using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SkynetzAPI.Controllers.Tariff.Response
{
    public class TariffDTO
    {
        [JsonProperty("id")]
        public int? Id { get; set; }

        [JsonProperty("source")]
        public string? Source { get; set; }

        [JsonProperty("destination")]
        public string? Destination { get; set; }

        [JsonProperty("MinuteValue")]
        public double? MinuteValue { get; set; }
    }
}
