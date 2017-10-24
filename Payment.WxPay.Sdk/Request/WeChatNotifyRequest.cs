namespace Payment.WxPay.Sdk.Request
{
    public class WeChatNotifyRequest
    {
        public bool IsVerify { get; set; }
        public string PayNo { get; set; }
        public string TradeIds { get; set; }
        public string PayTime { get; set; }
        public string Sign { get; set; }
        public string Content { get; set; }
    }
}
