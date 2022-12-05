using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SkynetzAPI.Controllers.Tariff.Response;
using SkynetzAPI.Services;
using SkynetzMVC.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SkynetzAPI.Controllers.Tariff
{
    [Route("api/")]
    [ApiController]
    public class TariffController : ControllerBase
    {

        private readonly TariffService tariffService;

        [HttpGet]
        [Route("Tariff")]
        public ActionResult GetAllTariffs()
        {
            return Ok(tariffService.GetAllTariffs());
        }

        [HttpGet]
        [Route("Tariff/{id}")]
        public ActionResult GetTariffById([FromHeader]int id)
        {
            return Ok(tariffService.GetTariffById(id));
        }

        [HttpGet]
        [Route("Tariff/Filter")]
        public ActionResult GetTariffsByParameter([FromBody] TariffDTO tariffDTO)
        {
            var tariffs = tariffService.GetTariffsByParameter(tariffDTO);

            if (tariffs.Any())
            {
                return Ok(tariffs);
            }
            else
            {
                return NotFound();

            }
        }

        [HttpPost]
        [Route("Tariff")]
        public ActionResult InsertTariff([FromBody]TariffDTO tariffDTO)
        {
            var tariff = tariffService.InsertTariff(tariffDTO);

            return Ok(tariff);
        }

        [HttpPut]
        [Route("Tariff")]
        public ActionResult UpdateTariff([FromBody]TariffDTO tariffDTO)
        {
            var tariff = tariffService.UpdateTariff(tariffDTO);

            return Ok(tariff);
        }

    }


}
