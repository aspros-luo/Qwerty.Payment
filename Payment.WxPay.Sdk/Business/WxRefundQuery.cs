using Payment.WxPay.Sdk.Lib;
using System.Threading.Tasks;

namespace Payment.WxPay.Sdk.Business
{
    /// <summary>
    /// 微信退款查询
    /// </summary>
    internal class WxRefundQuery
    {
        /***
        * 退款查询完整业务流程逻辑
        * @param refund_id 微信退款单号（优先使用）
        * @param out_refund_no 商户退款单号
        * @param transaction_id 微信订单号
        * @param out_trade_no 商户订单号
        * @return 退款查询结果（xml格式）
        */
        public static async Task<string> Run(string refundId, string outRefundNo, string transactionId, string outTradeNo)
        {
            var data = new WxPayData();
            if (!string.IsNullOrEmpty(refundId))
            {
                data.SetValue("refund_id", refundId);//微信退款单号，优先级最高
            }
            else if (!string.IsNullOrEmpty(outRefundNo))
            {
                data.SetValue("out_refund_no", outRefundNo);//商户退款单号，优先级第二
            }
            else if (!string.IsNullOrEmpty(transactionId))
            {
                data.SetValue("transaction_id", transactionId);//微信订单号，优先级第三
            }
            else
            {
                data.SetValue("out_trade_no", outTradeNo);//商户订单号，优先级最低
            }
            var result = await WxPayApi.RefundQuery(data);//提交退款查询给API，接收返回数据
            return result.ToJson();
        }
    }
}
