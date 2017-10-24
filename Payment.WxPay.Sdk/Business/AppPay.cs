using Payment.WxPay.Sdk.Lib;
using System;
using System.Threading.Tasks;

namespace Payment.WxPay.Sdk.Business
{
    internal class AppPay
    {
        public async Task<WxPayData> Run(string body, string outTradeNo, int totalFee)
        {
            var data = new WxPayData();
            data.SetValue("body", body);//商品描述
            //data.SetValue("attach", outTradeIds);//附加数据
            data.SetValue("out_trade_no", outTradeNo);//随机字符串
            data.SetValue("total_fee", totalFee);//总金额
            data.SetValue("time_start", DateTime.Now.ToString("yyyyMMddHHmmss"));//交易起始时间
            data.SetValue("time_expire", DateTime.Now.AddMinutes(10).ToString("yyyyMMddHHmmss"));//交易结束时间
            //data.SetValue("goods_tag", "jjj");//商品标记
            data.SetValue("trade_type", "APP");//交易类型
            var result = await WxPayApi.UnifiedOrder(data);//调用统一下单接口
            return result;
        }
    }
}
