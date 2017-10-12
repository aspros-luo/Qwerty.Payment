namespace Payment.WxPay.Sdk.Model
{
    public class WxRefundQueryModel
    {
        /// <summary>
        /// 微信退款单号
        /// </summary>
        public string RefundId { get; set; }
        /// <summary>
        /// 商户退款单号
        /// </summary>
        public string OutRefundNo { get; set; }
        /// <summary>
        /// 微信订单号
        /// </summary>
        public string TransactionId { get; set; }
        /// <summary>
        /// 商户订单号m
        /// </summary>
        public string OutTradeNo { get; set; }
    }
}
