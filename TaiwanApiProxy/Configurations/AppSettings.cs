namespace TaiwanApiProxy.Configurations
{
    using System;
    using System.Configuration;
    using System.Web;

    public static class AppSettings
    {
        static AppSettings()
        {
            TaiwanAPIUrl = new Uri(GetValue("TaiwanAPIUrl"));
            NumberLog = Convert.ToInt32(GetValue("NumberLog"));
        }

        public static Uri TaiwanAPIUrl { get; private set; }

        public static int NumberLog { get; private set; }

        private static string GetValue(string key)
        {
            try
            {
                return ConfigurationManager.AppSettings[key].ToLower();
            }
            catch
            {
                var exception = new InvalidOperationException(string.Format("Key '{0}' not found in appSettings.", key));
                throw exception;
            }
        }
    }
}