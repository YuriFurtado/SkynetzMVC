using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace SkynetzAPI.Controllers
{
    public class ReturnDTO
    {
        [JsonProperty("statusCode")]
        public HttpStatusCode StatusCode { get; set; }

        [JsonProperty("success")]
        public bool Success { get; set; }
        
        [JsonProperty("message")]
        public string? Message { get; set; }

        [JsonProperty("content")]
        public object Content { get; set; }
    }
}
