using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace CurrencyDL.JsonConverter
{
    public class JsonToHistoryInfo
    {
        [JsonProperty("motd")]
        public JsonMotd Motd { get; set; }

        [JsonProperty("success")]
        public bool Success { get; set; }

        [JsonProperty("timeseries")]
        public bool Timeseries { get; set; }

        [JsonProperty("base")]
        public string Base { get; set; }

        [JsonProperty("start_date")]
        public DateTimeOffset StartDate { get; set; }

        [JsonProperty("end_date")]
        public DateTimeOffset EndDate { get; set; }

        [JsonProperty("rates")]
        public Dictionary<string, Dictionary<string, double>> Rates { get; set; }
    }
}
