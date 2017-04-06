using System.Web.Http;
using Newtonsoft.Json.Serialization;

namespace ItemList.Api
{
    public static class FormatterConfig
    {
        public static void Register(HttpConfiguration config)
        {
            var jsonFormatter = config.Formatters.JsonFormatter;
            jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            jsonFormatter.UseDataContractJsonSerializer = false;
        }
    }
}