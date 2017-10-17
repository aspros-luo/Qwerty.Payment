using System;
using System.Collections.Generic;
using System.Text;
using Payment.WxPay.Sdk.Lib;
using Payment.WxPay.Sdk.Model;

namespace Payment.WxPay.Sdk.Business
{
    internal class MwebPay
    {
        public WxPayData GetPayUrl(NativeWxPayModel wxPayModel)
        {
            //Log.Info(this.GetType().ToString(), "Native pay mode 2 url is producing...");

            WxPayData data = new WxPayData();
            data.SetValue("body", wxPayModel.Body);//商品描述
            //data.SetValue("attach", wxPayModel.Body);//附加数据
            data.SetValue("out_trade_no", wxPayModel.OutTradeNo);//随机字符串
            data.SetValue("total_fee", wxPayModel.TotalFee);//总金额
            data.SetValue("time_start", DateTime.Now.ToString("yyyyMMddHHmmss"));//交易起始时间
            data.SetValue("time_expire", DateTime.Now.AddMinutes(10).ToString("yyyyMMddHHmmss"));//交易结束时间
            //data.SetValue("goods_tag", "jjj");//商品标记
            data.SetValue("trade_type", "MWEB");//交易类型
            data.SetValue("spbill_create_ip", "115.236.186.130");//终端ip	  	    
            WxPayData result = WxPayApi.UnifiedOrder(data);//调用统一下单接口
            //string url = result.GetValue("code_url").ToString();//获得统一下单接口返回的二维码链接

            //Log.Info(this.GetType().ToString(), "Get native pay mode 2 url : " + url);
            return result;
        }

        public string GetJsApiParameters(WxPayData unifiedOrderResult)
        {
            //Log.Debug(this.GetType().ToString(), "JsApiPay::GetJsApiParam is processing...");

            WxPayData jsApiParam = new WxPayData();
            jsApiParam.SetValue("appId", unifiedOrderResult.GetValue("appid"));
            jsApiParam.SetValue("timeStamp", WxPayApi.GenerateTimeStamp());
            jsApiParam.SetValue("nonceStr", WxPayApi.GenerateNonceStr());
            jsApiParam.SetValue("package", "prepay_id=" + unifiedOrderResult.GetValue("prepay_id"));
            jsApiParam.SetValue("signType", "MD5");
            jsApiParam.SetValue("paySign", jsApiParam.MakeSign());

            string parameters = jsApiParam.ToJson();

            //Log.Debug(this.GetType().ToString(), "Get jsApiParam : " + parameters);
            return parameters;
        }
    }
}
