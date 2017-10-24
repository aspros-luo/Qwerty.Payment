using Payment.WxPay.Sdk.Request;
using Payment.WxPay.Sdk.Response;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Payment.WxPay.Sdk.Interfaces
{
    public interface IWxPayService
    {
        /// <summary>
        /// 微信扫码支付，用户发送数据到服务器，返回支付二维码字段
        /// </summary>
        /// <returns></returns>
        Task<Tuple<bool, string>> PagePay(string body, string outTradeNo, int totalFee, string productId);
        /// <summary>
        /// 微信app支付，移动端发送数据，服务器统一下单，返回数据用于app支付
        /// </summary>
        /// <returns></returns>
        Task<Tuple<bool, string>> AppPay(string body, string outTradeNo, int totalFee);
        /// <summary>
        /// 微信公众号支付，服务器返回H5调起参数
        /// </summary>
        /// <returns></returns>
        Task<Tuple<bool, string>> JsApiPay(string body, string outTradeNo, int totalFee, string openId);
        /// <summary>
        /// 微信H5支付
        /// </summary>
        /// <returns></returns>
        Task<Tuple<bool, string>> MwebPay(string body, string outTradeNo, int totalFee, string ip);
        /// <summary>
        /// 微信申请退款
        /// </summary>
        /// <returns></returns>
        Task<WeChatRefundResponse> WeChatRefund(string transactionId, string outTradeNo, int totalFee, int refundFee, string outRefundNo);
        /// <summary>
        /// 微信退款查询，用于确认退款是否成功
        /// </summary>
        /// <returns></returns>
        Task<WeChatRefundQueryResponse> WeChatRefundQuery(string refundId, string outRefundNo, string transactionId, string outTradeNo);
        /// <summary>
        /// 微信回调
        /// </summary>
        /// <returns></returns>
        Task<WeChatNotifyRequest> WeChatNotify(Stream weChatReturnData);
    }
}
