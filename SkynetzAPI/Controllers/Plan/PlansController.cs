using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SkynetzAPI.Services;
using SkynetzMVC.Repositories;
using SkynetzMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using SkynetzAPI.Controllers.Plan.Response;

namespace SkynetzAPI.Controllers.Plan
{
    [Route("api/")]
    [ApiController]
    public class PlansController : ControllerBase
    {
        private readonly PlanService planService;

        public PlansController(SkynetzDbContext db)
        {
            planService = new PlanService(db);
        }

        [HttpGet]
        [Route("Plan")]
        public ActionResult GetAllPlan()
        {
            try
            {
                var plans = planService.GetAllPlans();
                ReturnDTO successDTO = new ReturnDTO 
                { 
                    StatusCode = HttpStatusCode.OK,
                    Success = true,
                    Content = plans
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
        [Route("Plan/{id}")]
        public ActionResult GetPlanById(int id)
        {
            try
            {
                var plan = planService.GetPlanById(id);
                if (plan != null)
                {
                    ReturnDTO successDTO = new ReturnDTO
                    {
                        StatusCode = HttpStatusCode.OK,
                        Success = true,
                        Content = plan
                    };
                    return Ok(successDTO);
                }
                else
                {
                    return NotFound();
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

        [HttpGet]
        [Route("Plan/Filter")]
        public ActionResult GetPlansByParameter(string? name, int? freeMinutes)
        {
            try
            {
                PlanDTO filterPlan = new PlanDTO {Name = name, FreeMinutes = freeMinutes };
                var plans = planService.GetPlanByParameter(filterPlan);
                ReturnDTO successDTO = new ReturnDTO { };
                if (plans.Any())
                {
                    successDTO.StatusCode = HttpStatusCode.OK;
                    successDTO.Success = true;
                    successDTO.Content = plans;
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
        [Route("Plan")]
        public ActionResult InsertPlan(string name, int freeMinutes)
        {
            try
            {
                PlanDTO planDTO = new PlanDTO { Name = name, FreeMinutes = freeMinutes };
                var plan = planService.InsertPlan(planDTO);
                ReturnDTO successDTO = new ReturnDTO
                {
                    StatusCode = HttpStatusCode.Created,
                    Success = true,
                    Message = "Plano inserido com sucesso",
                };
                return Ok(successDTO);
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

        [HttpPut]
        [Route("Plan")]
        public ActionResult UpdatePlan(int id, string name, int freeMinutes)
        {
            try
            {
                PlanDTO planDTO = new PlanDTO { Id = id, Name = name, FreeMinutes = freeMinutes };
                var plan = planService.UpdatePlan(planDTO);
                ReturnDTO successDTO = new ReturnDTO
                {
                    StatusCode = HttpStatusCode.OK,
                    Success = true,
                    Message = "Plano alterado com sucesso",
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
        [Route("Plan/{id}")]
        public ActionResult DeletePlan(int id)
        {
            try
            {
                var returnDelete = planService.DeletePlan(id);
                ReturnDTO successDTO = new ReturnDTO
                {
                    StatusCode = HttpStatusCode.OK,
                    Success = true,
                    Message = "Plano removido com sucesso",
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
