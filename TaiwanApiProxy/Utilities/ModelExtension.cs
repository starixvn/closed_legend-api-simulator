namespace TaiwanApiProxy.Utilities
{
    using System;

    public static class ModelExtension
    {
        public static Type GetModelType(string modelName)
        {
            return Type.GetType(string.Format("TaiwanApiProxy.ViewModels.{0}", modelName), true);
        }
    }
}