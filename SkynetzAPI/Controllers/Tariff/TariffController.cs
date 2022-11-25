using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SkynetzAPI.Controllers.Tariff
{
    [Route("api/[controller]")]
    [ApiController]
    public class TariffController : ControllerBase
    {
        [HttpGet]
        [Route("GetAll")]
        public ActionResult GetAll()
        {
            return Ok("Teste Ok");
        }
    }
}
