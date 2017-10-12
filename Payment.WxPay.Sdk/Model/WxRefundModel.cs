namespace Payment.WxPay.Sdk.Model
{
    public class WxRefundModel
    {
        /// <summary>
        /// 微信订单号
        /// </summary>
        public string TransactionId { get; set; }
        /// <summary>
        /// 商户订单号
        /// </summary>
        public string OutTradeNo { get; set; }
        /// <summary>
        /// 订单总金额
        /// </summary>
        public int TotalFee { get; set; }
        /// <summary>
        /// 退款金额
        /// </summary>
        public int RefundFee { get; set; }
        /// <summary>
        /// 商户退款单号
        /// </summary>
        public string OutRefundNo { get; set; }
    }
}
