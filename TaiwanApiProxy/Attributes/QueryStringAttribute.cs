namespace TaiwanApiProxy.Attributes
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    public class QueryStringAttribute : Attribute
    {
        public int Help { get; set; }
    }
}