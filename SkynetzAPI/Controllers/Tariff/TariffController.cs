using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SkynetzAPI.Services;
using SkynetzMVC.Repositories;
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

        private readonly TariffService tariffService;

        [HttpGet]
        [Route("GetAllPlans")]
        public ActionResult GetAllTariffs()
        {
            return Ok(tariffService.GetAllTariffs());
        }

        [HttpGet]
        [Route("GetTariffById")]
        public ActionResult GetTariffById(int id)
        {
            return Ok(tariffService.GetTariffById(id));
        }

        [HttpGet]
        [Route("GetTariffsByParameter")]
        public ActionResult GetTariffsByParameter(string source, string destination, double minuteValue)
        {
            FilterTariff filterTariff = new FilterTariff { Source = source, Destination = destination, MinuteValue = minuteValue };

            var tariffs = tariffService.GetTariffsByParameter(filterTariff);

            if (tariffs.Any())
            {
                return Ok(tariffs);
            }
            else
            {
                return NotFound();

            }
        }
    }


}
