using ProvaPub.Models;
using ProvaPub.Models.PaymentMethodModels;

namespace ProvaPub.Services
{
	public class OrderService
	{
		public async Task<Order> PayOrder(string paymentMethod, decimal paymentValue, int customerId)
		{			
			//Faz pagamento...
			var _paymentMethod = PaymentMethodFactory.CreatePaymentMethod(paymentMethod);
			var resultado = await _paymentMethod.Pay(paymentValue, customerId);
			/*
			 * cada método de pagamento terá sua própria lógica e seu retorno sobre sucesso ou falha a ser tratado.
			 * */
			if (! resultado.IsSuccessful)
			{ 
				// faz algum tratamento.
			}

			return await Task.FromResult( new Order()
			{
				Value = paymentValue,
				CustomerId = customerId //outro tratamento pode buscar o objeto Customer.
			});
		}
	}
}
