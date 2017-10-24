using Payment.WxPay.Sdk.Lib;
using System.Threading.Tasks;

namespace Payment.WxPay.Sdk.Business
{
    /// <summary>
    /// 微信退款申请
    /// </summary>
    internal class WxRefund
    {
        /***
        * 申请退款完整业务流程逻辑
        * @param transaction_id 微信订单号（优先使用）
        * @param out_trade_no 商户订单号
        * @param total_fee 订单总金额
        * @param refund_fee 退款金额
        * @return 退款结果（xml格式）
        */
        public static async Task<string> Run(string transactionId, string outTradeNo, int totalFee, int refundFee, string outRefundNo)
        {
            var data = new WxPayData();
            if (!string.IsNullOrEmpty(transactionId)) //微信订单号存在的条件下，则已微信订单号为准
            {
                data.SetValue("transaction_id", transactionId);
            }
            else //微信订单号不存在，才根据商户订单号去退款
            {
                data.SetValue("out_trade_no", outTradeNo);
            }
            data.SetValue("total_fee", totalFee); //订单总金额
            data.SetValue("refund_fee", refundFee); //退款金额
            data.SetValue("out_refund_no", outRefundNo); //随机生成商户退款单号
            data.SetValue("op_user_id", WxPayConfig.MCHID); //操作员，默认为商户号
            var result = await WxPayApi.Refund(data); //提交退款申请给API，接收返回数据
            return result.ToJson();
        }
    }
}
