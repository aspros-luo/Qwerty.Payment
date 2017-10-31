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
        private const string AppId = @"2016081900289736";

        private const string PrivateKey = @"MIIEvQIBADANBgkqhkiG9w0BAQEFAASCBKcwggSjAgEAAoIBAQCHSbxwF3tjByoraRtxGu7yr2nt+GMNApGUd92CQQKYkTa19hVYzv3McxnZtMP8fXEx9Uhs14yXyQHL8+GHOJ+gSWcxEJ3v4f3phUHMHq9C/5z4XhPdKdup611jUrkzxCVFQZnSUG7ErW8wmRw5wfoZ89oK0vFCXdUtcwwaVXDM4o+/r89/NMllb3EPGZci8Qw+L8pio5H1dLExdTf4L8ISRLlN2QUl2huCZahwpkKdGgGAQKYBn+PvwSw6ZDk+wDtMghRo4/jdMxcbjIWvOh5s7Tjx9StTNsgjr2uRSPsEqeJwSL9ojYnMVpuJ/xqQmIp512ULQvkZS4b/+U87w7ldAgMBAAECggEAQ1f6HFYkDnxvimJszZWZomadNV8ydzRzIVO1iPQxhZ6rfFJ999I51j7pfEyWTqZm5XZy0fNOQfRGF69T8YrHMvO3EV5zMAjv6wFxallP5ur0yVGTU8FVXjUSLLHuDQ2ze9EW7/En4nFu6uMcgMfFZovTWxX8EIxC5LfjK2yilOnr0H4AKiOYMm6WiyHMK69JV1A3kjnlRHorisrLysmcKMO3q9Yl+s2cVe7uZS2RUp4pvAVDwvL/FL0RcMvVNY4GFfrpYLgwH7NAkmakmzpyrPsj7IBlKE+WJ6/cJ2XyMQEXojyb98Y8je/YjfVvClw9DKPSSR1SaG9MxwACX0nFwQKBgQDTtto1F4wC6dueT2b02BWLUaJNYfV7OIjyuCsGXoZ3Sf5scYKn7gEd75eiJzmNc6iAym/abvgzGRUgzjEJJSxpPGuUT/VoQ6iN0wjE9KugSg8qaeB3iEOJk8TWqpbRszYpYBxqo18yhJ71qoEpMm2RQBMLB0XNKBpyyUEIKBLVbQKBgQCjllA+BLkuZTzyYhfqa5muTsOgkBOYvXF16SpAPxXh5rcHsN9869u6ZZ5NkfTyfozxFsCbq/bYchHIUHM0m+j/RUrGmYkd/e+DIDuxFRbw5gN9IBQ/9lLj+sGUxgj2d/LDs5DwVKJwI4fav8gLT/hVCgSE2qUfJzDVgxc34eItsQKBgEIG+eCq+lCCTKr/ynU72uQ7TmnhziRiylsgUtLGshsL6Zw6fmwPDywd5+V7ZDiYRIn+GIpAJ4oQHYXAqIxYmpQrcsWrdjbROwUYNtjuEYSI9Ffe81F0HtQOUMo+I5E82fxnbBVZ5DumHskxJt0JTCCLoiTDXOKRykXYPD7l+JG1AoGAWVVDmqqfqdt1TfQNlWGPOiYfJLapTPbfWAGtpgoNXCDPAO8xDJoMkxzdNwUm26oKM7o2Eoz1LzwTw+1TDH673XMso4nC3FMJEfVvQ4P91C3358O16zwMBh2wyxreWCImu4J6+xNs5YoxrV6f0rWoKmBfGWIBXCLdwytkaYr8JfECgYEAhQUoAc3C+mWOswPTpoYcLjxZU/bWZnxEF4P/KHf4RDMTg5w5kL1wNmmmejVT2OR0A/QvaVB9sv4oBXly6q0fKXSie6mH6KVYhJ3voVcm0Ukg/h1HqE7Q90G/5KmVvZsL26mm2l2p6Fi9OgzXSAn+Qbf4t4ksOd9I43azKGuhjwQ=";
        private const string AliPublicKey = @"MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAxM7oKRmjEgFibOmLYgRfLvf3lwlFA6XJyUGMfgvhx3sZp+Aa37HxHuuYQyXvt8iRrspKeKrZMbWeSkseOFIlspoKfPER9SeN5hXpW+eRJT1LH5j+7HnYj8D1ZxGq5NtCgGbExUoXaEsPOvOoOTpZTxjdNXzTIvdF4+Nx7Hh9rtitdNG/IllSvFEtTih7qRmE6FoJjb+YPSY1ElWTM3yM2JHumUSmz6+2HZ+2KIUqdVzQD/fURZscyNaATIyjxI/IEMaP1LGjnAGDZlIiHTuI8OHYI0jDai9DC3aAQDUH8POWxzXMBJ1s1UaDf8tGqbcMdBP0vPMvwuQHLsDli4BIhQIDAQAB";

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

            var s = await a.AppPay("", "", 0);
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
