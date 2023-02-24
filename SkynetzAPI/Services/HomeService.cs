using SkynetzAPI.Controllers.Home.Response;
using SkynetzMVC.Controllers.DTO;
using SkynetzMVC.Models;
using SkynetzMVC.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SkynetzAPI.Services
{
    public class HomeService
    {
        public readonly PlanRepository planRepository;
        public readonly TariffRepository tariffRepository;

        public HomeService(SkynetzDbContext db)
        {
            planRepository = new PlanRepository(db);
            tariffRepository = new TariffRepository(db);
        }

        public ResultDTO Result(HomeDTO homeDTO)
        {
            try
            {
                FilterPlan filterPlan = new FilterPlan() { Name = homeDTO.UsedPlan };
                FilterTariff filterTariff = new FilterTariff { Source = homeDTO.Source, Destination = homeDTO.Destination };

                Tariff tariff = tariffRepository.GetByParameters(filterTariff).FirstOrDefault();
                Plan plan = planRepository.GetByParameters(filterPlan).FirstOrDefault();

                if (tariff == null)
                {
                    throw new KeyNotFoundException("Tarifa não encontrada");
                }
                if (plan == null)
                {
                    throw new KeyNotFoundException("Plano não encontrado");
                }

                ResultDTO resultDTO = new ResultDTO
                {
                    Source = tariff.Source,
                    Destination = tariff.Destination,
                    UsedMinutes = homeDTO.UsedMinutes,
                    UsedPlan = plan.Name,
                    PriceWithPlan = plan.PriceWithPlan(homeDTO.UsedMinutes, tariff.MinuteValue).ToString("N2"),
                    PriceWithoutPlan = plan.PriceWithouPlan(tariff, homeDTO.UsedMinutes).ToString("N2")
                };

                return resultDTO;
            }
            catch (KeyNotFoundException keyEx)
            {
                throw keyEx;
            }
            catch (Exception ex)
            {
                throw new Exception($"Falha ao gerar calculo - {(ex.InnerException ?? ex).Message}");
            }
        }
        
    }
}
