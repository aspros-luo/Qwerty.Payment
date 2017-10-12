using Payment.WxPay.Sdk.Lib;
using Payment.WxPay.Sdk.Model;

namespace Payment.WxPay.Sdk.Business
{
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
        public static string Run(WxRefundModel refundModel)
        {
            //Log.Info("Refund", "Refund is processing...");

            WxPayData data = new WxPayData();
            if (!string.IsNullOrEmpty(refundModel.TransactionId)) //微信订单号存在的条件下，则已微信订单号为准
            {
                data.SetValue("transaction_id", refundModel.TransactionId);
            }
            else //微信订单号不存在，才根据商户订单号去退款
            {
                data.SetValue("out_trade_no", refundModel.OutTradeNo);
            }

            data.SetValue("total_fee", refundModel.TotalFee); //订单总金额
            data.SetValue("refund_fee", refundModel.RefundFee); //退款金额
            data.SetValue("out_refund_no", refundModel.OutRefundNo); //随机生成商户退款单号
            data.SetValue("op_user_id", WxPayConfig.MCHID); //操作员，默认为商户号

            WxPayData result = WxPayApi.Refund(data); //提交退款申请给API，接收返回数据

            //Log.Info("Refund", "Refund process complete, result : " + result.ToXml());
            return result.ToPrintStr();
        }
    }
}
