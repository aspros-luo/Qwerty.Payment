using Microsoft.Extensions.DependencyInjection;
using Payment.AliApy.Sdk.Interfaces;
using Payment.AliApy.Sdk.Model;
using Xunit;
using Xunit.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Payment.AliApy.Sdk.Services;

namespace PayTest
{
    public class UnitTest1:BaseTest
    {
        private readonly IAliPayService _aliPayService;
        public UnitTest1(ITestOutputHelper output) : base(output)   
        {
            _aliPayService = Provider.GetService<IAliPayService>();
        }

        [Fact]
        public async void Test1()
        {
            var payModel = new PagePayModel
            {
                out_trade_no = $"{DateTime.Now:yyyyMMddHHmmss}",
                subject = $"÷ß∏∂≤‚ ‘{DateTime.Now:yyyyMMddHHmmss}",
                total_amount = "0.01"
            };
            AliPayService a=new AliPayService();
            var s= await a.Pay(payModel);
            Assert.NotNull(s);
        }
    }
}
