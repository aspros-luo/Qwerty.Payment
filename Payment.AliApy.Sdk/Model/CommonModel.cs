namespace Payment.AliApy.Sdk.Model
{
    public class CommonModel
    {
        public string app_id { get; set; }
        public string method { get; set; }
        public string format { get; set; }
        public string return_url { get; set; }
        public string charset { get; set; }
        public string sign_type { get; set; }
        public string sign { get; set; }
        public string timestamp { get; set; }
        public string version { get; set; }
        public string notify_url { get; set; }
        public string biz_content { get; set; }
        
    }
}
