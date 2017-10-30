namespace Payment.AliPay.Sdk.Model
{
    public class AliRefundQueryModel
    {
        /// <summary>
        /// 商户平台订单号
        /// </summary>
        public string out_trade_no { get; set; }
        /// <summary>
        /// 支付宝订单号
        /// </summary>
        public string out_request_no { get; set; }
    }
}
