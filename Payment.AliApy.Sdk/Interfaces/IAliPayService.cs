using Payment.AliPay.Sdk.Model;
using Payment.AliPay.Sdk.Request;
using Payment.AliPay.Sdk.Response;
using System.IO;
using System.Threading.Tasks;

namespace Payment.AliPay.Sdk.Interfaces
{
    public interface IAliPayService
    {
        /// <summary>
        /// page支付，支付信息组成页面表单数据，用于pc支付
        /// </summary>
        /// <returns></returns>
        AliPayRequest NativePay(AliPayModel payModel);
        /// <summary>
        /// app支付，app端发送加签字段，返回签名数据
        /// </summary>
        /// <returns></returns>
        AliPayRequest AppPay(string preSign);
        /// <summary>
        /// jsapi支付，用于网页支付
        /// </summary>
        /// <returns></returns>
        AliPayRequest JsApiPay(AliPayModel payModel);
        /// <summary>
        /// 退款申请接口，用户发起退款申请
        /// </summary>
        /// <returns></returns>
        Task<AliRefundResponse> AliRefund(AliRefundModel refundModel);
        /// <summary>
        /// 退款查询接口，用于确认退款是否成功
        /// </summary>
        /// <returns></returns>
        Task<AliRefundQueryResponse> AliRefundQuery(AliRefundQueryModel refundQueryModel);
        /// <summary>
        /// 支付回掉接口
        /// </summary>
        /// <returns></returns>
        AliNotifyRequest AliNotify(Stream aliReturnData);
        /// <summary>
        /// 统一收单线下交易预创建，用于预先生成交易订单，生成二维码，用户扫码后完成支付
        /// </summary>
        /// <returns></returns>
        Task<AliPayRequest> Precreate(AliPrePayModel prePayModel);
    }
}
