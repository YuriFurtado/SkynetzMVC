using SkynetzMVC.Models;
using SkynetzMVC.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SkynetzMVC.Interfaces
{
    public interface IPlanRepository
    {
        List<Plan> GetAll();
        Plan GetPlanById(int id);
        List<Plan> GetByParameters(FilterPlan filters);
        Plan InsertPlan(Plan plan);
        List<Plan> InsertRangePlan(List<Plan> newPlans);
        bool DeletePlan(int id);
        List<Plan> DeleteRangePlan(List<Plan> plansRemoved);

    }
}
