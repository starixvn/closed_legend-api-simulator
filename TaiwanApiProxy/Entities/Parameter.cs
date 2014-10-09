namespace TaiwanApiProxy.Entities
{
    public class Parameter
    {
        public string Name { get; set; }

        public string Type { get; set; }

        public object Value { get; set; }

        public bool IsOutput { get; set; }
    }
}