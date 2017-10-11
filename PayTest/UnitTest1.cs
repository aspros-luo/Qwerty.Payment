using Microsoft.Extensions.DependencyInjection;
using Payment.AliPay.Sdk.Interfaces;
using Payment.AliPay.Sdk.Model;
using Payment.AliPay.Sdk.Services;
using System;
using System.Text;
using Payment.WxPay.Sdk.Model;
using Payment.WxPay.Sdk.Services;
using Xunit;
using Xunit.Abstractions;

namespace PayTest
{
    public class UnitTest1:BaseTest
    {
        private readonly IAliPayService _aliPayService;
        public UnitTest1(ITestOutputHelper output) : base(output)   
        {
            _aliPayService = Provider.GetService<IAliPayService>();
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        }

        [Fact]
        public async void Test1()
        {
           
            var payModel = new AliPayModel
            {
                out_trade_no = $"{DateTime.Now:yyyyMMddHHmmss}",
                subject = $"PC Test Pay",
                total_amount = "0.01"
            };
            AliPayService a=new AliPayService();
            var s= await a.PagePay(payModel);
            Assert.NotNull(s);
        }

        [Fact]
        public async void Test2()
        {
            var payModel = new AliPayModel
            {
                out_trade_no = $"{DateTime.Now:yyyyMMddHHmmss}",
                subject = $"支付测试{DateTime.Now:yyyyMMddHHmmss}",
                total_amount = "0.01"
            };
            AliPayService a = new AliPayService();
            var s = await a.AppPay(payModel);
            Assert.NotNull(s);
        }
        [Fact]
        public async void Test3()
        {
            var payModel = new AliPayModel
            {
                out_trade_no = $"{DateTime.Now:yyyyMMddHHmmss}",
                subject = $"JSAPI Test Pay",
                total_amount = "0.01"
            };
            AliPayService a = new AliPayService();
            var s = await a.JsApiPay(payModel);
            Assert.NotNull(s);
        }

        [Fact]
        public async void Test4()
        {
            var refundModel = new AliRefundModel()
            {
                out_trade_no = "20171010165857",
                refund_amount = 0.01M
            };
            AliPayService a = new AliPayService();
            var s = await a.AliRefund(refundModel);
            Assert.NotNull(s);
        }

        [Fact]
        public async void Test5()
        {
            var refundModel = new AliRefundQueryModel()
            {
                out_trade_no = "20171010165857",
                out_request_no = "20171010165857"
            };
            AliPayService a = new AliPayService();
            var s = await a.AliRefundQuery(refundModel);
            Assert.NotNull(s);
        }

        [Fact]
        public async void Test6()
        {
            WxPayService a = new WxPayService();
            var wxPayModel = new NativeWxPayModel
            {
                Body = "Ferragamo小手包",
                OutTradeNo = $"{DateTime.Now:yyyyMMddHHmmss}",
                TotalFee = 10,
                ProductId = $"Ferragamo小手包{DateTime.Now:yyyyMMddHHmmss}"
            };
            var s = await a.PagePay(wxPayModel);
            Assert.NotNull(s);
        }
    }
}
