namespace TaiwanApiProxy.Entities
{
    using System;

    public class LogInformation
    {
        public int Id { get; set; }

        public string ActionName { get; set; }

        public string ActionModel { get; set; }

        public string Input { get; set; }

        public DateTime LogTime { get; set; }

        public string Ip { get; set; }
    }
}