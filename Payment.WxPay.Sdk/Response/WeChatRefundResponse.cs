using System;
using System.Collections.Generic;
using System.Text;

namespace Payment.WxPay.Sdk.Response
{
    public class WeChatRefundResponse
    {
        public bool IsSuccess => return_code == "SUCCESS";
        public string appid { get; set; }
        public string cash_fee { get; set; }
        public string cash_refund_fee { get; set; }
        public string coupon_refund_count { get; set; }
        public string coupon_refund_fee { get; set; }
        public string err_code { get; set; }
        public string err_code_des { get; set; }
        public string mch_id { get; set; }
        public string nonce_str { get; set; }
        public string out_refund_no { get; set; }
        public string out_trade_no { get; set; }
        public string refund_channel { get; set; }
        public string refund_fee { get; set; }
        public string refund_id { get; set; }
        public string result_code { get; set; }
        public string return_code { get; set; }
        public string return_msg { get; set; }
        public string sign { get; set; }
        public string total_fee { get; set; }
        public string transaction_id { get; set; }
    }
}
