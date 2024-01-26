using ProvaPub.Models.PaymentMethodModels.ResultModels;

namespace ProvaPub.Models.PaymentMethodModels
{
    public abstract class PaymentMethodBase
    {
        /* Propriedades comuns aos métodos de pagamento aqui. Pode ser que o uso de interfaces seja mais adequado para alguns casos.*/
        public abstract Task<PaymentResult> Pay(decimal paymentValue, int customerId);
    }
}
