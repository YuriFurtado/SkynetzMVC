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
                PlanDTO planDTO = ModelToDTO(plan);

                plansDTOS.Add(planDTO);
            }

            return plansDTOS;
        }

        public PlanDTO GetPlanById(int id)
        {
            Plan plan = planRepository.GetPlanById(id);

            PlanDTO planDTO = ModelToDTO(plan);

            return planDTO;
        }

        public List<PlanDTO> GetPlanByParameter(PlanDTO filterDTO) // Criar filtro com base na DTO
        {
            FilterPlan filterPlan = new FilterPlan
            {
                Id = filterDTO.Id,
                Name = filterDTO.Name,
                FreeMinutes = filterDTO.FreeMinutes
            };
            
            List<Plan> plans = planRepository.GetByParameters(filterPlan);

            List<PlanDTO> plansDTOS = new List<PlanDTO>();
            
            foreach(Plan plan in plans)
            {
                PlanDTO planDTO = ModelToDTO(plan);

                plansDTOS.Add(planDTO);
            }

            return plansDTOS;
        }

        public bool InsertPlan(PlanDTO planDTO)
        {
            Plan insertPlan = DTOToModel(planDTO);

            Plan returnPlan = planRepository.InsertPlan(insertPlan);

            return returnPlan != null;
        }

        public bool UpdatePlan(PlanDTO planDTO)
        {
            Plan updatePlan = DTOToModel(planDTO);

            Plan returnPlan = planRepository.UpdatePlan(updatePlan);

            return returnPlan != null;
        }

        public bool DeletePlan(int id)
        {
            return(planRepository.DeletePlan(id));
        }

        // TODO - Criar Mapeamento DTO to Model

        public PlanDTO ModelToDTO(Plan plan)
        {
            return new PlanDTO
            {
                Id = plan.Id,
                Name = plan.Name,
                FreeMinutes = plan.FreeMinutes
            };
        }

        public Plan DTOToModel(PlanDTO planDTO)
        {
            return new Plan
            {
                Id = (int)planDTO.Id,
                Name = planDTO.Name,
                FreeMinutes = (int)planDTO.FreeMinutes
            };
        }
    }
}
