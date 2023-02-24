using Microsoft.AspNetCore.Mvc;
using SkynetzAPI.Controllers.Home.Response;
using SkynetzAPI.Services;
using SkynetzMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace SkynetzAPI.Controllers.Home
{
    [Route("api/")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly HomeService homeService;

        public HomeController(SkynetzDbContext db)
        {
            homeService = new HomeService(db);
        }

        [HttpGet]
        [Route("Price")]
        public ActionResult GetPrices(string source, string destination, int usedMinutes, string usedPlan)
        {
            try
            {
                HomeDTO homeDTO = new HomeDTO
                {
                    Source = source,
                    Destination = destination,
                    UsedMinutes = usedMinutes,
                    UsedPlan = usedPlan
                };
                var prices = homeService.Result(homeDTO);
                ReturnDTO successDTO = new ReturnDTO
                {
                    StatusCode = HttpStatusCode.OK,
                    Success = true,
                    Content = prices
                };
                return Ok(successDTO);
            }
            catch (KeyNotFoundException ex)
            {
                ReturnDTO erroDTO = new ReturnDTO
                {
                    StatusCode = HttpStatusCode.NotFound,
                    Success = false,
                    Message = ex.Message
                };
                return StatusCode(404, erroDTO);
            }
            catch(Exception ex)
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
