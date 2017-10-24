namespace Payment.AliPay.Sdk.Model
{
    public class AliRefundModel
    {
        /// <summary>
        /// 平台交易单号
        /// </summary>
        public string out_trade_no { get; set; }
        /// <summary>
        /// 支付宝交易单号
        /// </summary>
        public string trade_no { get; set; }
        public decimal refund_amount { get; set; }
    }
}
