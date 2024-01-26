using ProvaPub.Models;
using ProvaPub.Repository;

namespace ProvaPub.Services
{
	public class ProductService
	{
		TestDbContext _ctx;

		public ProductService(TestDbContext ctx)
		{
			_ctx = ctx;
		}

		public ProductList  ListProducts(int page)
		{
			//Solução Parte2: o parâmetro referente a página ("page") não era utilizado nem existia lógica implementada.
			var elementosPorPagina = 10;
			var numeroAPular = (page - 1) * elementosPorPagina;
			var listaProdutos =  _ctx.Products
				.Skip(numeroAPular)
				.Take(elementosPorPagina)
				.ToList();
			var totalCount = listaProdutos.Count;
            var hasNext = ((numeroAPular) + totalCount) < _ctx.Products.Count();

            return new ProductList() {  HasNext= hasNext, TotalCount = totalCount,
                Products = listaProdutos
            };
			/* O ideal é também adicionar um tratamento de exceção para caso não tenhamos elementos suficiente para uma página informada,
			 * bem como o tratamento para parametros inválidos de "page".
			 * Ex: página 10, mas não existem mais de 100 elementos ou página 0. */
		}
	}
}
