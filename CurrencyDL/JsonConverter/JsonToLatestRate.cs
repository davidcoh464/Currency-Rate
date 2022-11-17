using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace CurrencyDL.JsonConverter
{
    public class JsonToLatestRate
    {
        [JsonProperty("motd")]
        public JsonMotd Motd { get; set; }

        [JsonProperty("success")]
        public bool Success { get; set; }

        [JsonProperty("base")]
        public string Base { get; set; }

        [JsonProperty("date")]
        public DateTimeOffset Date { get; set; }

        [JsonProperty("rates")]
        public Dictionary<string, double> Rates { get; set; }
    }
}
