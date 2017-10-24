namespace Payment.AliPay.Sdk.Request
{
    public class AliPayRequest
    {
        public bool IsSuccess { get; set; }
        public string PreSign { get; set; }
        public string Sign { get; set; }
        public string Result { get; set; }
    }
}
