using System;
using System.Collections.Generic;
using System.Text;

namespace Payment.WxPay.Sdk.Response
{
    public class WeChatRefundQueryResponse
    {
        public bool IsSuccess=> refund_status_0== "SUCCESS";
        public string appid { get; set; }
        public string cash_fee { get; set; }
        public string mch_id { get; set; }
        public string nonce_str { get; set; }
        public string out_refund_no_0 { get; set; }
        public string out_trade_no { get; set; }
        public string refund_account_0 { get; set; }
        public string refund_channel_0 { get; set; }
        public string refund_count { get; set; }
        public string refund_fee { get; set; }
        public string refund_fee_0 { get; set; }
        public string refund_id_0 { get; set; }
        public string refund_recv_accout_0 { get; set; }
        public string refund_status_0 { get; set; }
        public string refund_success_time_0 { get; set; }
        public string result_code { get; set; }
        public string return_code { get; set; }
        public string return_msg { get; set; }
        public string sign { get; set; }
        public string total_fee { get; set; }
        public string transaction_id { get; set; }
    }
}
