using SkynetzMVC.Models;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace SkynetzMVC.Repositories
{
    public class PlanRepository
    {
        public PlanRepository() 
        {
            
        }

        public List<Plan> Plans = new List<Plan>() 
        { 
            new Plan(){ Id = 1, Name = "FaleMais 30", FreeMinutes = 30},
            new Plan(){ Id = 2, Name = "FaleMais 60", FreeMinutes = 60},
            new Plan(){ Id = 3, Name = "FaleMais 120", FreeMinutes = 120}
        };

        public List<Plan> GetAll()
        {
            return Plans.ToList();
        }

        public Plan GetPlanById(int id) 
        {
            return Plans.AsQueryable().Where(x => x.Id == id).FirstOrDefault();
        }

        public List<Plan> GetByParameters(FilterPlan filters)
        {
            var query = Plans.AsQueryable();

            if (!string.IsNullOrEmpty(filters.Name))
            {
                query = query.Where(x => x.Name == filters.Name);
            }

            if(filters.FreeMinutes != null)
            {
                query = query.Where(x => x.FreeMinutes == filters.FreeMinutes);
            }

            return query.ToList();
        }

        public Plan InsertPlan(Plan plan) 
        {
            Plans.Add(plan);
            return GetPlanById(plan.Id);
        }

        public List<Plan> InsertRangePlan(List<Plan> newPlans)
        {
            Plans.AddRange(newPlans);
            return Plans;
        }

        public Plan UpdatePlan(Plan plan)
        {
            var update = GetPlanById(plan.Id);
            update.Name = plan.Name;
            update.FreeMinutes = plan.FreeMinutes;
            return GetPlanById(update.Id);
        }

        public bool DeletePlan(int id)
        {
            var delete = GetPlanById(id);
            Plans.Remove(delete);
            bool HasPlano = Plans.Contains(delete);
            return HasPlano;
        }

        public List<Plan> DeleteRangePlan(List<Plan> plansRemoved)
        {
            foreach (Plan plan in plansRemoved)
            {
                Plans.Remove(plan);
            }
            return Plans;
        }
    }

    public class FilterPlan
    {
        public string? Name { get; set; }
        public int? FreeMinutes { get; set; }
    }
}
