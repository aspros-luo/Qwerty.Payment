namespace Payment.AliPay.Sdk.Model
{
    public class AliPayModel
    {
        public string out_trade_no { get; set; }
        public string product_code { get; private set; } = "FAST_INSTANT_TRADE_PAY";
        public string total_amount { get; set; }
        public string subject { get; set; }

        public void SetProductCode(string code)
        {
            product_code = code;
        }
    }
}
