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

            double priceWhitoutPlan = tariff.MinuteValue * usedMinutes;
            double priceWithPlan;

            if (plan.FreeMinutes >= usedMinutes)
            {
                priceWithPlan = 0;
            }
            else
            {
                priceWithPlan = (usedMinutes - plan.FreeMinutes) * (tariff.MinuteValue * 1.10);
            }

            ResultDTO resultDTO = new ResultDTO 
            {   
                Source = tariff.Source, 
                Destination = tariff.Destination, 
                UsedMinutes = usedMinutes, 
                UsedPlan = plan.Name, 
                PriceWithPlan = priceWithPlan.ToString("N2"), 
                PriceWithoutPlan = priceWhitoutPlan.ToString("N2")
            };

            return resultDTO;
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
                        priceWithPlan = (usedMinutes - plan.FreeMinutes) * (tariff.MinuteValue * 1.10);
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

        // Métodos Antigos
    //    public string Results(string origem, string destino, int minutosUsados, string planoUsado)
    //    {
    //        int? minutosPlano = MinutosPorPlano(planoUsado);
    //        double? precoMinuto = PrecoPorMinuto(origem, destino);
    //        double precoComPlano;

    //        if (minutosPlano == null || precoMinuto == null)
    //        {
    //            return "Erro ao gerar os valores";
    //        }

    //        double precoSemPlano = (double)precoMinuto * minutosUsados;

    //        if (minutosPlano >= minutosUsados)
    //        {
    //            precoComPlano = 0;
    //        }
    //        else
    //        {
    //            precoComPlano = (minutosUsados - (int)minutosPlano) * ((double)precoMinuto * 1.10);
    //        }

    //        return ("Valor com Plano: " + precoComPlano.ToString("N2") + " - Valor sem Plano: " + precoSemPlano.ToString("N2"));
    //    }

    //    public int? MinutosPorPlano(string plano)
    //    {
    //        if (plano.Equals("FaleMais 30")) return 30;
    //        if (plano.Equals("FaleMais 60")) return 60;
    //        if (plano.Equals("FaleMais 120")) return 120;

    //        return null;
    //    }

    //    public double? PrecoPorMinuto(string origem, string destino)
    //    {
    //        if (origem.Equals("011"))
    //        {
    //            if (destino.Equals("016")) return 1.90;
    //            if (destino.Equals("017")) return 1.70;
    //            if (destino.Equals("018")) return 0.90;
    //        }

    //        if (origem.Equals("016"))
    //        {
    //            if (destino.Equals("011")) return 2.90;
    //        }

    //        if (origem.Equals("017"))
    //        {
    //            if (destino.Equals("011")) return 2.70;
    //        }

    //        if (origem.Equals("018"))
    //        {
    //            if (destino.Equals("011")) return 1.90;
    //        }

    //        return null;
    //    }
    //}
}
