using Payment.WxPay.Sdk.Lib;
using System;
using Payment.WxPay.Sdk.Model;

namespace Payment.WxPay.Sdk.Business
{
    internal class AppPay
    {
        public WxPayData Run(AppPayModel wxPayModel)
        {
            WxPayData data = new WxPayData();
            data.SetValue("body", wxPayModel.Body);//商品描述
            //data.SetValue("attach", wxPayModel.Body);//附加数据
            data.SetValue("out_trade_no", wxPayModel.OutTradeNo);//随机字符串
            data.SetValue("total_fee", wxPayModel.TotalFee);//总金额
            data.SetValue("time_start", DateTime.Now.ToString("yyyyMMddHHmmss"));//交易起始时间
            data.SetValue("time_expire", DateTime.Now.AddMinutes(10).ToString("yyyyMMddHHmmss"));//交易结束时间
            //data.SetValue("goods_tag", "jjj");//商品标记
            data.SetValue("trade_type", "APP");//交易类型

            WxPayData result = WxPayApi.UnifiedOrder(data);//调用统一下单接口

            return result;
        }
    }
}
