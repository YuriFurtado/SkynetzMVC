using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SkynetzAPI.Controllers.Home.Response
{
    public class HomeDTO
    {
        [JsonProperty("source")]
        public string Source { get; set; }

        [JsonProperty("destination")]
        public string Destination { get; set; }

        [JsonProperty("usedMinutes")]
        public int UsedMinutes { get; set; }

        [JsonProperty("usedPlan")]
        public string UsedPlan { get; set; }
    }
}
