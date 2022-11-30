using SkynetzAPI.Controllers.Plan.Response;
using SkynetzMVC.Models;
using SkynetzMVC.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SkynetzAPI.Services
{
    public class PlanService
    {
        public readonly PlanRepository planRepository;

        public PlanService(SkynetzDbContext db)
        {
            planRepository = new PlanRepository(db);
        }

        public List<PlanDTO> GetAllPlans()
        {
            List<Plan> plans = planRepository.GetAll();

            List<PlanDTO> plansDTOS = new List<PlanDTO>();

            foreach(Plan plan in plans)
            {
                PlanDTO planDTO = new PlanDTO
                {
                    Name = plan.Name,
                    FreeMinutes = plan.FreeMinutes
                };

                plansDTOS.Add(planDTO);
            }

            return plansDTOS;
        }

        public PlanDTO GetPlanById(int id)
        {
            Plan plan = planRepository.GetPlanById(id);

            PlanDTO planDTO = new PlanDTO
            {
                Name = plan.Name,
                FreeMinutes = plan.FreeMinutes
            };

            return planDTO;
        }

        public List<PlanDTO> GetPlanByParameter(FilterPlan filterPlan)
        {
            List<Plan> plans = planRepository.GetByParameters(filterPlan);

            List<PlanDTO> plansDTOS = new List<PlanDTO>();
            
            foreach(Plan plan in plans)
            {
                PlanDTO planDTO = new PlanDTO
                {
                    Name = plan.Name,
                    FreeMinutes = plan.FreeMinutes
                };

                plansDTOS.Add(planDTO);
            }

            return plansDTOS;
        }

        public PlanDTO InsertPlan(string name, int freeMinutes)
        {
            Plan insertPlan = new Plan
            {
                Name = name,
                FreeMinutes = freeMinutes
            };

            Plan returnPlan = planRepository.InsertPlan(insertPlan);

            PlanDTO planDTO = new PlanDTO
            {
                Name = insertPlan.Name,
                FreeMinutes = insertPlan.FreeMinutes
            };

            return planDTO;
        }

    }
}
