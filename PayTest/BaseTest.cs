using Microsoft.Extensions.DependencyInjection;
using System;
using Xunit.Abstractions;

namespace PayTest
{
    public class BaseTest
    {
        protected readonly ITestOutputHelper Output;
        protected IServiceProvider Provider;
        public BaseTest(ITestOutputHelper output)
        {
            var service = new ServiceCollection();
            Output = output;
            Provider = service.BuildServiceProvider();
        }
    }
}
