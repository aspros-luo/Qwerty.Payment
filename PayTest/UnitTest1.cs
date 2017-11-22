using Microsoft.Extensions.DependencyInjection;
using Payment.AliPay.Sdk.Interfaces;
using Payment.AliPay.Sdk.Model;
using Payment.AliPay.Sdk.Services;
using Payment.WxPay.Sdk.Services;
using System;
using System.Text;
using Payment.AliPay.Sdk.Configs;
using Xunit;
using Xunit.Abstractions;

namespace PayTest
{
    public class UnitTest1 : BaseTest
    {
        private readonly IAliPayService _aliPayService;
        private const string AppId = @"XX";

        private const string PrivateKey = @"XX";
        private const string AliPublicKey = @"XX";

        public UnitTest1(ITestOutputHelper output) : base(output)
        {
            _aliPayService = Provider.GetService<IAliPayService>();
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            AliPayConfig.Init(AppId, PrivateKey, AliPublicKey, "", "www.aqsea.com");
        }

        [Fact]
        public async void Test1()
        {
            var payModel = new AliPayModel
            {
                out_trade_no = $"{DateTime.Now:yyyyMMddHHmmss}",
                subject = $"÷ß∏∂≤‚ ‘",
                total_amount = "0.01",
            };
            
            AliPayService a = new AliPayService();
            var s = a.NativePay(payModel);

            Assert.NotNull(s);
        }

        [Fact]
        public async void Test2()
        {
            var payModel = new AliPayModel
            {
                out_trade_no = $"{DateTime.Now:yyyyMMddHHmmss}",
                subject = $"÷ß∏∂≤‚ ‘{DateTime.Now:yyyyMMddHHmmss}",
                total_amount = "0.01"
            };
            AliPayService a = new AliPayService();
            var s = a.AppPay("ssss");
            Assert.NotNull(s);
        }
        [Fact]
        public async void Test3()
        {
            var payModel = new AliPayModel
            {
                out_trade_no = $"{DateTime.Now:yyyyMMddHHmmss}",
                subject = $"JSAPI Test Pay",
                total_amount = "0.01",
            };
            AliPayService a = new AliPayService();
            var s = a.JsApiPay(payModel);
            Assert.NotNull(s);
        }

        [Fact]
        public async void Test4()
        {
            var refundModel = new AliRefundModel()
            {
                out_trade_no = "10005",
                trade_no = "2017102321001004720230769658",
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

            var s = await a.PagePay("", "", 0, "");
            Assert.NotNull(s);
        }

        [Fact]
        public async void Test7()
        {
            WxPayService a = new WxPayService();
            var s = await a.JsApiPay("", "", 0, "");
            Assert.NotNull(s);
        }

        [Fact]
        public async void Test8()
        {
            WxPayService a = new WxPayService();

            var s = await a.MwebPay("", "", 0, "");
            Assert.NotNull(s);
        }

        [Fact]
        public async void Test9()
        {
            WxPayService a = new WxPayService();

            var s = await a.AppPay("test1", "10003", 1);
            Assert.NotNull(s);
        }

        [Fact]
        public async void Test10()
        {
            WxPayService a = new WxPayService();
            var s = await a.WeChatRefund("", "", 0, 0, "");
            Assert.NotNull(s);
        }

        [Fact]
        public async void Test11()
        {
            WxPayService a = new WxPayService();
            var s = await a.WeChatRefundQuery("", "", "", "");
            Assert.NotNull(s);
        }
    }
}
