using Payment.WxPay.Sdk.Lib;
using System;
using System.Threading.Tasks;

namespace Payment.WxPay.Sdk.Business
{
    /// <summary>
    /// 微信H5支付
    /// </summary>
    internal class MwebPay
    {
        public async Task<WxPayData> GetPayUrl(string body, string outTradeNo, int totalFee, string ip)
        {
            var data = new WxPayData();
            data.SetValue("body", body);//商品描述
            //data.SetValue("attach", outTradeIds);//附加数据
            data.SetValue("out_trade_no", outTradeNo);//随机字符串
            data.SetValue("total_fee", totalFee);//总金额
            data.SetValue("time_start", DateTime.Now.ToString("yyyyMMddHHmmss"));//交易起始时间
            data.SetValue("time_expire", DateTime.Now.AddMinutes(10).ToString("yyyyMMddHHmmss"));//交易结束时间
            //data.SetValue("goods_tag", "jjj");//商品标记
            data.SetValue("trade_type", "MWEB");//交易类型
            data.SetValue("spbill_create_ip", ip);//终端ip	 

            var result = await WxPayApi.UnifiedOrder(data);//调用统一下单接口
            return result;
        }

        public string GetJsApiParameters(WxPayData unifiedOrderResult)
        {
            WxPayData jsApiParam = new WxPayData();
            jsApiParam.SetValue("appId", unifiedOrderResult.GetValue("appid"));
            jsApiParam.SetValue("timeStamp", WxPayApi.GenerateTimeStamp());
            jsApiParam.SetValue("nonceStr", WxPayApi.GenerateNonceStr());
            jsApiParam.SetValue("package", "prepay_id=" + unifiedOrderResult.GetValue("prepay_id"));
            jsApiParam.SetValue("signType", "MD5");
            jsApiParam.SetValue("paySign", jsApiParam.MakeSign());

            string parameters = jsApiParam.ToJson();
            return parameters;
        }
    }
}
