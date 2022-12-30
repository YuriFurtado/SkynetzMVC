using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SkynetzAPI.Controllers.Tariff.Response;
using SkynetzAPI.Services;
using SkynetzMVC.Models;
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

        public TariffController(SkynetzDbContext db)
        {
            tariffService = new TariffService(db);
        }

        [HttpGet]
        [Route("Tariff")]
        public ActionResult GetAllTariffs()
        {
            try
            {
                var tariffs = tariffService.GetAllTariffs();
                return Ok(tariffs);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error - {(ex.InnerException ?? ex).Message}");
            }
        }

        [HttpGet]
        [Route("Tariff/{id}")]
        public ActionResult GetTariffById([FromHeader] int id)
        {
            try
            {
                var tariff = tariffService.GetTariffById(id);
                return Ok(tariff);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error - {(ex.InnerException ?? ex).Message}");
            }
        }

        [HttpGet]
        [Route("Tariff/Filter")]
        public ActionResult GetTariffsByParameter([FromBody] TariffDTO tariffDTO)
        {
            try
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
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error - {(ex.InnerException ?? ex).Message}");
            }
        }

        [HttpPost]
        [Route("Tariff")]
        public ActionResult InsertTariff([FromBody] TariffDTO tariffDTO)
        {
            try
            {
                var tariff = tariffService.InsertTariff(tariffDTO);

                return Ok(tariff);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error - {(ex.InnerException ?? ex).Message}");
            }
        }

        [HttpPut]
        [Route("Tariff")]
        public ActionResult UpdateTariff([FromBody] TariffDTO tariffDTO)
        {
            try
            {
                var tariff = tariffService.UpdateTariff(tariffDTO);

                return Ok(tariff);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error - {(ex.InnerException ?? ex).Message}");
            }
        }

        [HttpDelete]
        [Route("Tariff/{id}")]
        public ActionResult DeleteTariff([FromHeader] int id)
        {
            try
            {
                var returnDelete = tariffService.DeleteTariff(id);

                return Ok(returnDelete);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error - {(ex.InnerException ?? ex).Message}");
            }
        }

    }


}
