using Payment.WxPay.Sdk.Lib;
using Payment.WxPay.Sdk.Model;

namespace Payment.WxPay.Sdk.Business
{
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
        public static WxPayData Run(WxRefundQueryModel refundQueryModel)
        {
            //Log.Info("RefundQuery", "RefundQuery is processing...");

            WxPayData data = new WxPayData();
            if (!string.IsNullOrEmpty(refundQueryModel.RefundId))
            {
                data.SetValue("refund_id", refundQueryModel.RefundId);//微信退款单号，优先级最高
            }
            else if (!string.IsNullOrEmpty(refundQueryModel.OutRefundNo))
            {
                data.SetValue("out_refund_no", refundQueryModel.OutRefundNo);//商户退款单号，优先级第二
            }
            else if (!string.IsNullOrEmpty(refundQueryModel.TransactionId))
            {
                data.SetValue("transaction_id", refundQueryModel.TransactionId);//微信订单号，优先级第三
            }
            else
            {
                data.SetValue("out_trade_no", refundQueryModel.OutTradeNo);//商户订单号，优先级最低
            }

            WxPayData result = WxPayApi.RefundQuery(data);//提交退款查询给API，接收返回数据

            //Log.Info("RefundQuery", "RefundQuery process complete, result : " + result.ToXml());
            return result;
        }
    }
}
