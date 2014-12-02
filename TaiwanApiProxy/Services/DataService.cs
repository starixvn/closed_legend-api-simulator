namespace TaiwanApiProxy.Services
{
    using System.Collections.Generic;
    using System.IO;
    using System.Web;
    using System.Xml.Linq;
    using TaiwanApiProxy.Utilities;

    public class DataService
    {
        public DataService(string directory)
        {
            Directory = directory;
        }

        public string Directory { get; set; }

        public IList<T> Read<T>()
        {
            var path = GetFilePath(typeof(T).Name);
            if (!File.Exists(path))
            {
                return new List<T>();
            }

            return ObjectSerializer.DeserializeList<T>(XDocument.Load(path).ToString());
        }

        public void Write<T>(IList<T> rules)
        {
            var path = GetFilePath(typeof(T).Name);

            XDocument.Parse(ObjectSerializer.SerializeList<T>(rules)).Save(path);
        }

        private string GetFilePath(string fileName)
        {
            return HttpContext.Current.Server.MapPath(string.Format(Directory, fileName));
        }
    }
}