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

        // TODO - Realocar DTO para pasta Response

        [HttpGet]
        [Route("Plan")]
        public ActionResult GetAllPlan()
        {
            try
            {
                var plans = planService.GetAllPlans();
                return Ok(plans);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error - {(ex.InnerException ?? ex).Message}");
            }
        }

        [HttpGet]
        [Route("Plan/{id}")]
        public ActionResult GetPlanById([FromHeader] int id)
        {
            try
            {
                var plan = planService.GetPlanById(id);
                if (plan != null)
                {
                    return Ok(plan);
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

        [HttpGet]
        [Route("Plan/Filter")]
        public ActionResult GetPlansByParameter([FromBody] PlanDTO filterPlan)
        {
            try
            {
                var plans = planService.GetPlanByParameter(filterPlan);

                if (plans.Any())
                {
                    return Ok(plans);
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
        [Route("Plan")]
        public ActionResult InsertPlan([FromBody] PlanDTO planDTO)
        {
            try
            {
                var plan = planService.InsertPlan(planDTO);

                return Ok(plan);
            }
            catch(Exception ex)
            {
                return StatusCode(500, $"Internal server error - {(ex.InnerException ?? ex).Message}");
            }
        }

        [HttpPut]
        [Route("Plan")]
        public ActionResult UpdatePlan([FromBody] PlanDTO planDTO)
        {
            try
            {
                var plan = planService.UpdatePlan(planDTO);

                return Ok(plan);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error - {(ex.InnerException ?? ex).Message}");
            }
        }

        [HttpDelete]
        [Route("Plan/{id}")]
        public ActionResult DeletePlan([FromHeader] int id)
        {
            try
            {
                var returnDelete = planService.DeletePlan(id);

                return Ok(returnDelete);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error - {(ex.InnerException ?? ex).Message}");
            }
        }

    }
}
