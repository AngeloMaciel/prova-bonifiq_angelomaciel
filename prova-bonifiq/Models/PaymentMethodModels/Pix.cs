using ProvaPub.Models.PaymentMethodModels.ResultModels;

namespace ProvaPub.Models.PaymentMethodModels
{
    public class Pix : PaymentMethodBase
    {
        public override async Task<PaymentResult> Pay(decimal paymentValue, int customerId)
        {
            return new PaymentResult { IsSuccessful = true };
        }
    }
}
