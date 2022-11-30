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

namespace SkynetzAPI.Controllers.Plan
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlansController : ControllerBase
    {
        private readonly PlanService planService;


        [HttpGet]
        [Route("GetAll")]
        public ActionResult GetAllPlan()
        {
            return Ok(planService.GetAllPlans());
        }

        [HttpGet]
        [Route("GetPlanById")]
        public ActionResult GetPlanById(int id)
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
        [Route("GetPlansByParameter")]
        public ActionResult GetPlansByParameter(string name, int freeMinutes)
        {
            FilterPlan filterPlan = new FilterPlan { Name = name, FreeMinutes = freeMinutes };

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

        [HttpPut]
        [Route("InsertPlan")]
        public ActionResult InsertPlan(string name, int freeMinutes)
        {
            var plan = planService.InsertPlan(name, freeMinutes);

            return Ok(plan);
        }
    }
}
