using Microsoft.EntityFrameworkCore;
using ProvaPub.Models;
using ProvaPub.Repository;

namespace ProvaPub.Services
{
    public class CustomerService
    {
        TestDbContext _ctx;

        public CustomerService(TestDbContext ctx)
        {
            _ctx = ctx;
        }

        public CustomerList ListCustomers(int page)
        {
            //Solução Parte2: o parâmetro referente a página ("page") não era utilizado nem existia lógica implementada.
            var elementosPorPagina = 10;
            var numeroAPular = (page - 1) * elementosPorPagina;
            var listaClientes = _ctx.Customers
                .Skip(numeroAPular)
                .Take(elementosPorPagina)
                .ToList();
            var totalCount = listaClientes.Count;
            var hasNext = ((numeroAPular) + totalCount) < _ctx.Customers.Count();

            return new CustomerList() { HasNext = hasNext, TotalCount = totalCount, Customers = listaClientes };
            /* O ideal é também adicionar um tratamento de exceção para caso não tenhamos elementos suficiente para uma página informada,
			 * bem como o tratamento para parametros inválidos de "page".
			 * Ex: página 10, mas não existem mais de 100 elementos ou página 0. */
        }

        public async Task<bool> CanPurchase(int customerId, decimal purchaseValue)
        {
            if (customerId <= 0) throw new ArgumentOutOfRangeException(nameof(customerId));

            if (purchaseValue <= 0) throw new ArgumentOutOfRangeException(nameof(purchaseValue));

            //Business Rule: Non registered Customers cannot purchase
            var customer = await _ctx.Customers.FindAsync(customerId);
            if (customer == null) throw new InvalidOperationException($"Customer Id {customerId} does not exists");

            //Business Rule: A customer can purchase only a single time per month
            var baseDate = DateTime.UtcNow.AddMonths(-1);
            var ordersInThisMonth = await _ctx.Orders.CountAsync(s => s.CustomerId == customerId && s.OrderDate >= baseDate);
            if (ordersInThisMonth > 0)
                return false;

            //Business Rule: A customer that never bought before can make a first purchase of maximum 100,00
            var haveBoughtBefore = await _ctx.Customers.CountAsync(s => s.Id == customerId && s.Orders.Any());
            if (haveBoughtBefore == 0 && purchaseValue > 100)
                return false;

            return true;
        }

    }
}
