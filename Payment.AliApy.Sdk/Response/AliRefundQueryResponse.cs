namespace Payment.AliPay.Sdk.Response
{
    public class AliRefundQueryResponse
    {
       public AlipayRefundQueryResponse alipay_trade_fastpay_refund_query_response { get; set; }
       public string sign { get; set; }
        
    }
    public class AlipayRefundQueryResponse
    {
        /// <summary>
        /// 本笔退款对应的退款请求号
        /// </summary>
        public string code { get; set; }
        /// <summary>
        /// 本笔退款对应的退款请求号
        /// </summary>
        public string msg { get; set; }
        /// <summary>
        /// 本笔退款对应的退款请求号
        /// </summary>
        public string out_request_no { get; set; }
        /// <summary>
        /// 创建交易传入的商户订单号
        /// </summary>
        public string out_trade_no { get; set; }
        /// <summary>
        /// 本次退款请求，对应的退款金额
        /// </summary>
        public string refund_amount { get; set; }
        /// <summary>
        /// 该笔退款所对应的交易的订单金额
        /// </summary>
        public string total_amount { get; set; }
        /// <summary>
        /// 支付宝交易号
        /// </summary>
        public string trade_no { get; set; }
    }
}
