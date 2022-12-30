using SkynetzMVC.Controllers.DTO;
using SkynetzMVC.Models;
using SkynetzMVC.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SkynetzMVC.Services
{
    public class HomeService
    {
        public readonly TariffRepository tariffRepository;
        public readonly PlanRepository planRepository;

        public HomeService(SkynetzDbContext db)
        {
            tariffRepository = new TariffRepository(db);
            planRepository = new PlanRepository(db);
        }


        public ResultDTO Result(string idTariff, int usedMinutes, string usedPlan)
        {
            FilterPlan filterPlan = new FilterPlan() { Name = usedPlan };

            Tariff tariff = tariffRepository.GetTariffById(Convert.ToInt32(idTariff));
            Plan plan = planRepository.GetByParameters(filterPlan).FirstOrDefault();

            double priceWithPlan;

            if (plan.FreeMinutes >= usedMinutes)
            {
                priceWithPlan = 0;
            }
            else
            {
                priceWithPlan = PriceExceeded(plan, tariff, usedMinutes);
            }

            ResultDTO resultDTO = new ResultDTO 
            {   
                Source = tariff.Source, 
                Destination = tariff.Destination, 
                UsedMinutes = usedMinutes, 
                UsedPlan = plan.Name, 
                PriceWithPlan = priceWithPlan.ToString("N2"), 
                PriceWithoutPlan = PriceWithouPlan(tariff, usedMinutes).ToString("N2")
            };

            return resultDTO;
        }

        double percentageExceeded = 1.10;
        
        public double PriceExceeded(Plan plan, Tariff tariff, int usedMinutes)
        {
            return (double)((usedMinutes - plan.FreeMinutes) * (tariff.MinuteValue * percentageExceeded));
        }

        public double PriceWithouPlan (Tariff tariff, int usedMinutes)
        {
            return tariff.MinuteValue * usedMinutes;
        }

        public List<ResultDTO> ResultsDinamic(FilterPlan filterPlan, FilterTariff filterTariff, int usedMinutes)
        {
            List<Tariff> tariffs = tariffRepository.GetByParameters(filterTariff);
            List<Plan> plans = planRepository.GetByParameters(filterPlan);

            double priceWithoutPlan;
            double priceWithPlan;
            List<ResultDTO> resultDTOs = new List<ResultDTO>();

            foreach (Tariff tariff in tariffs)
            {
                foreach (Plan plan in plans)
                {
                    priceWithoutPlan = tariff.MinuteValue * usedMinutes;

                    if (plan.FreeMinutes >= usedMinutes)
                    {
                        priceWithPlan = 0;
                    }
                    else
                    {
                        priceWithPlan = (double)((usedMinutes - plan.FreeMinutes) * (tariff.MinuteValue * 1.10));
                    }

                    ResultDTO resultDTO = new ResultDTO
                    {
                        Source = tariff.Source,
                        Destination = tariff.Destination,
                        UsedMinutes = usedMinutes,
                        UsedPlan = plan.Name,
                        PriceWithPlan = priceWithPlan.ToString("N2"),
                        PriceWithoutPlan = priceWithoutPlan.ToString("N2")
                    };

                    resultDTOs.Add(resultDTO);
                }
            }

            return resultDTOs;
        }

    }
}
