﻿using Microsoft.AspNetCore.Mvc;
using ProvaPub.Models;
using ProvaPub.Models.PaymentMethodModels;
using ProvaPub.Repository;
using ProvaPub.Services;
using Swashbuckle.AspNetCore.Annotations;

namespace ProvaPub.Controllers
{
	
	/// <summary>
	/// Esse teste simula um pagamento de uma compra.
	/// O método PayOrder aceita diversas formas de pagamento. Dentro desse método é feita uma estrutura de diversos "if" para cada um deles.
	/// Sabemos, no entanto, que esse formato não é adequado, em especial para futuras inclusões de formas de pagamento.
	/// Como você reestruturaria o método PayOrder para que ele ficasse mais aderente com as boas práticas de arquitetura de sistemas?
	/// </summary>
	[ApiController]
	[Route("[controller]")]
	public class Parte3Controller :  ControllerBase
	{
		[HttpGet("orders")]
        [ProducesResponseType(typeof(Order), 200)]
        [ProducesResponseType(400)]
        [SwaggerOperation("PlaceOrder")]
        [SwaggerResponse(200, "Order placed successfully", typeof(Order))]
        [SwaggerResponse(400, "Bad request")]
        public async Task<Order> PlaceOrder([SwaggerParameter("Payment method: pix, creditcard, paypal")]
        string paymentMethod, decimal paymentValue, int customerId)
		{
			return await new OrderService().PayOrder(paymentMethod, paymentValue, customerId);
		}
	}
}