namespace Payment.AliPay.Sdk.Configs
{
    public static class AliPayConfig
    {
        public static string Gateway { get; private set; } = "https://openapi.alipay.com/gateway.do";
        public static string PrivateKey { get; private set; } = @"";
        public static string AliPublicKey { get; private set; } = @"";
        public static string NotifyUrl { get; private set; } = "";
    }
}
