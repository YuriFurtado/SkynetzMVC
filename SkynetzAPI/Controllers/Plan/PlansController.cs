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

        // TODO - Criar DTO para exception - Success Bool - Message String

        [HttpGet]
        [Route("Plan")]
        public ActionResult GetAllPlan()
        {
            return Ok(planService.GetAllPlans());
        }

        [HttpGet]
        [Route("Plan/{id}")]
        public ActionResult GetPlanById([FromHeader]int id)
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

        [HttpGet]
        [Route("Plan/Filter")]
        public ActionResult GetPlansByParameter([FromBody]PlanDTO filterPlan) // TODO - Utilizar DTO para o Filtro
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

        [HttpPost]
        [Route("Plan")]
        public ActionResult InsertPlan([FromBody]PlanDTO planDTO)
        {
            var plan = planService.InsertPlan(planDTO);

            return Ok(plan);
        }

        [HttpPut]
        [Route("Plan")]
        public ActionResult UpdatePlan([FromBody] PlanDTO planDTO)
        {
            var plan = planService.UpdatePlan(planDTO);

            return Ok(plan);
        }

    }
}
