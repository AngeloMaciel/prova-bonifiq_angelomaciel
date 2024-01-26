namespace ProvaPub.Models.PaymentMethodModels
{    public static class PaymentMethodFactory
    {
        public static PaymentMethodBase CreatePaymentMethod(string paymentMethod)
        {
            switch (paymentMethod.ToLower())
            {
                case "pix":
                    return new Pix();
                case "creditcard":
                    return new CreditCard();
                case "paypal":
                    return new PayPal();
                default:
                    throw new ArgumentException("Método de pagamento não suportado.");
            }
        }
    }
}
