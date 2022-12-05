using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SkynetzAPI.Controllers.Plan.Response
{
    public class PlanDTO
    {
        [JsonProperty("id")]
        public int? Id { get; set; }
        
        [JsonProperty("name")]
        public string? Name { get; set; }

        [JsonProperty("freeMinutes")]
        public int? FreeMinutes { get; set; }
    }
}
