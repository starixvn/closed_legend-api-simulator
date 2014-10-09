namespace TaiwanApiProxy.ViewResults
{
    using Newtonsoft.Json;

    public class JsonViewResult
    {
        public int ErrCode { get; set; }

        public string ErrMsg { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}