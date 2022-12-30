using Microsoft.EntityFrameworkCore;
using SkynetzMVC.Interfaces;
using SkynetzMVC.Models;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace SkynetzMVC.Repositories
{
    public class PlanRepository : IPlanRepository
    {
        public readonly SkynetzDbContext _db;

        public PlanRepository(SkynetzDbContext db)
        {
            _db = db;
        }
        

        public List<Plan> GetAll()
        {
            return _db.Plans.ToList();
        }

        public Plan GetPlanById(int id) 
        {
            return _db.Plans.AsQueryable().Where(x => x.Id == id).FirstOrDefault();
        }

        public List<Plan> GetByParameters(FilterPlan filters)
        {
            var query = _db.Plans.AsQueryable();

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
            _db.Plans.Add(plan);
            _db.SaveChanges();
            return GetPlanById((int)plan.Id);
        }

        public List<Plan> InsertRangePlan(List<Plan> newPlans)
        {
            _db.Plans.AddRange(newPlans);
            _db.SaveChanges();
            return _db.Plans.ToList();
        }

        public Plan UpdatePlan(Plan plan)
        {
            var update = GetPlanById((int)plan.Id);
            update.Name = plan.Name;
            update.FreeMinutes = plan.FreeMinutes;
            _db.Entry(update).State = EntityState.Modified;
            _db.SaveChanges();
            return GetPlanById((int)update.Id);
        }

        public bool DeletePlan(int id)
        {
            var delete = GetPlanById(id);
            _db.Plans.Remove(delete);
            _db.SaveChanges();
            var HasPlan = GetPlanById((int)delete.Id);
            if(HasPlan == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public List<Plan> DeleteRangePlan(List<Plan> plansRemoved)
        {
            foreach (Plan plan in plansRemoved)
            {
                _db.Plans.Remove(plan);
            }
            _db.SaveChanges();
            return _db.Plans.ToList();
        }
    }

    public class FilterPlan
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        public int? FreeMinutes { get; set; }
    }
}
