using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SkynetzAPI.Controllers.Tariff.Response;
using SkynetzAPI.Services;
using SkynetzMVC.Models;
using SkynetzMVC.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
                ReturnDTO successDTO = new ReturnDTO
                {
                    StatusCode = HttpStatusCode.OK,
                    Success = true,
                    Content = tariffs
                };
                return Ok(successDTO);
            }
            catch (Exception ex)
            {
                ReturnDTO erroDTO = new ReturnDTO
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Success = false,
                    Message = $"Internal server error - {(ex.InnerException ?? ex).Message}"
                };
                return StatusCode(500, erroDTO);
            }
        }

        [HttpGet]
        [Route("Tariff/{id}")]
        public ActionResult GetTariffById(int id)
        {
            try
            {
                var tariff = tariffService.GetTariffById(id);
                ReturnDTO successDTO = new ReturnDTO
                {
                    StatusCode = HttpStatusCode.OK,
                    Success = true,
                    Content = tariff
                };
                return Ok(successDTO);
            }
            catch (Exception ex)
            {
                ReturnDTO erroDTO = new ReturnDTO
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Success = false,
                    Message = $"Internal server error - {(ex.InnerException ?? ex).Message}"
                };
                return StatusCode(500, erroDTO);
            }
        }

        [HttpGet]
        [Route("Tariff/Filter")]
        public ActionResult GetTariffsByParameter(string? source, string? destination, float? minuteValue)
        {
            try
            {
                TariffDTO tariffDTO = new TariffDTO { Source = source, Destination = destination, MinuteValue = minuteValue };
                var tariffs = tariffService.GetTariffsByParameter(tariffDTO);
                ReturnDTO successDTO = new ReturnDTO { };
                if (tariffs.Any())
                {
                    successDTO.StatusCode = HttpStatusCode.OK;
                    successDTO.Success = true;
                    successDTO.Content = tariffs;
                    return Ok(successDTO);
                }
                else
                {
                    successDTO.StatusCode = HttpStatusCode.NotFound;
                    successDTO.Success = false;
                    return NotFound(successDTO);

                }
            }
            catch (Exception ex)
            {
                ReturnDTO erroDTO = new ReturnDTO
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Success = false,
                    Message = $"Internal server error - {(ex.InnerException ?? ex).Message}"
                };
                return StatusCode(500, erroDTO);
            }
        }

        [HttpPost]
        [Route("Tariff")]
        public ActionResult InsertTariff(string source, string destination, float minuteValue)
        {
            try
            {
                TariffDTO tariffDTO = new TariffDTO { Source = source, Destination = destination, MinuteValue = minuteValue };
                var tariff = tariffService.InsertTariff(tariffDTO);
                ReturnDTO successDTO = new ReturnDTO
                {
                    StatusCode = HttpStatusCode.Created,
                    Success = true,
                    Message = "Tarifa inserida com sucesso",
                };
                return Ok(successDTO);
            }
            catch (Exception ex)
            {
                ReturnDTO erroDTO = new ReturnDTO
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Success = false,
                    Message = $"Internal server error - {(ex.InnerException ?? ex).Message}"
                };
                return StatusCode(500, erroDTO);
            }
        }

        [HttpPut]
        [Route("Tariff")]
        public ActionResult UpdateTariff(int id, string source, string destination, float minuteValue)
        {
            try
            {
                TariffDTO tariffDTO = new TariffDTO { Id = id, Source = source, Destination = destination, MinuteValue = minuteValue };
                var tariff = tariffService.UpdateTariff(tariffDTO);
                ReturnDTO successDTO = new ReturnDTO
                {
                    StatusCode = HttpStatusCode.Created,
                    Success = true,
                    Message = "Tarifa alterada com sucesso",
                };
                return Ok(successDTO);
            }
            catch (Exception ex)
            {
                ReturnDTO erroDTO = new ReturnDTO
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Success = false,
                    Message = $"Internal server error - {(ex.InnerException ?? ex).Message}"
                };
                return StatusCode(500, erroDTO);
            }
        }

        [HttpDelete]
        [Route("Tariff/{id}")]
        public ActionResult DeleteTariff(int id)
        {
            try
            {
                var returnDelete = tariffService.DeleteTariff(id);
                ReturnDTO successDTO = new ReturnDTO
                {
                    StatusCode = HttpStatusCode.Created,
                    Success = true,
                    Message = "Tarifa removida com sucesso",
                };
                return Ok(successDTO);
            }
            catch (Exception ex)
            {
                ReturnDTO erroDTO = new ReturnDTO
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Success = false,
                    Message = $"Internal server error - {(ex.InnerException ?? ex).Message}"
                };
                return StatusCode(500, erroDTO);
            }
        }

    }


}
