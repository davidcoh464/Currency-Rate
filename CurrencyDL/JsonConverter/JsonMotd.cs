using Newtonsoft.Json;
using System;

namespace CurrencyDL.JsonConverter
{
    public class JsonMotd
    {
        [JsonProperty("msg")]
        public string Msg { get; set; }

        [JsonProperty("url")]
        public Uri Url { get; set; }
    }
}
