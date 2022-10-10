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

        public HomeService()
        {
            tariffRepository = new TariffRepository();
            planRepository = new PlanRepository();
        }

        public string Result(string source, string destination, int usedMinutes, string usedPlan)
        {
            FilterPlan filterPlan = new FilterPlan() { Name = usedPlan };
            FilterTariff filterTariff = new FilterTariff() { Source = source, Destination = destination };

            Tariff tariff = tariffRepository.GetByParameters(filterTariff).FirstOrDefault();
            Plan plan = planRepository.GetByParameters(filterPlan).FirstOrDefault();

            double priceWhitoutPlan = tariff.MinuteValue * usedMinutes;
            double priceWithPlan;
            string Results = string.Empty;

            if (plan.FreeMinutes >= usedMinutes)
            {
                priceWithPlan = 0;
            }
            else
            {
                priceWithPlan = (usedMinutes - plan.FreeMinutes) * (tariff.MinuteValue * 1.10);
            }

            Results += "Origem: " + tariff.Source + " - Destino: " + tariff.Destination + " - Tempo: " + usedMinutes;
            Results += " - Plano: " + plan.Name + " - Com FaleMais: " + priceWithPlan.ToString("N2") + " - Sem FaleMais: " + priceWhitoutPlan.ToString("N2");

            return Results;
        }

        public string ResultsDinamic(FilterPlan filterPlan, FilterTariff filterTariff, int usedMinutes)
        {
            List<Tariff> tariffs = tariffRepository.GetByParameters(filterTariff);
            List<Plan> plans = planRepository.GetByParameters(filterPlan);

            double priceWhitoutPlan;
            double priceWhitPlan;
            string Results = string.Empty;

            foreach (Tariff tariff in tariffs)
            {
                foreach (Plan plan in plans)
                {
                    priceWhitoutPlan = tariff.MinuteValue * usedMinutes;

                    if (plan.FreeMinutes >= usedMinutes)
                    {
                        priceWhitPlan = 0;
                    }
                    else
                    {
                        priceWhitPlan = (usedMinutes - plan.FreeMinutes) * (tariff.MinuteValue * 1.10);
                    }

                    Results += "Origem: " + tariff.Source + " - Destino: " + tariff.Destination + " - Tempo: " + usedMinutes;
                    Results += " - Plano: " + plan.Name + " - Com FaleMais: " + priceWhitPlan.ToString("N2") + " - Sem FaleMais: " + priceWhitoutPlan.ToString("N2") + Environment.NewLine;
                }
            }

            return Results;
        }

        // Métodos Antigos
        public string Results(string origem, string destino, int minutosUsados, string planoUsado)
        {
            int? minutosPlano = MinutosPorPlano(planoUsado);
            double? precoMinuto = PrecoPorMinuto(origem, destino);
            double precoComPlano;

            if (minutosPlano == null || precoMinuto == null)
            {
                return "Erro ao gerar os valores";
            }

            double precoSemPlano = (double)precoMinuto * minutosUsados;

            if (minutosPlano >= minutosUsados)
            {
                precoComPlano = 0;
            }
            else
            {
                precoComPlano = (minutosUsados - (int)minutosPlano) * ((double)precoMinuto * 1.10);
            }

            return ("Valor com Plano: " + precoComPlano.ToString("N2") + " - Valor sem Plano: " + precoSemPlano.ToString("N2"));
        }

        public int? MinutosPorPlano(string plano)
        {
            if (plano.Equals("FaleMais 30")) return 30;
            if (plano.Equals("FaleMais 60")) return 60;
            if (plano.Equals("FaleMais 120")) return 120;

            return null;
        }

        public double? PrecoPorMinuto(string origem, string destino)
        {
            if (origem.Equals("011"))
            {
                if (destino.Equals("016")) return 1.90;
                if (destino.Equals("017")) return 1.70;
                if (destino.Equals("018")) return 0.90;
            }

            if (origem.Equals("016"))
            {
                if (destino.Equals("011")) return 2.90;
            }

            if (origem.Equals("017"))
            {
                if (destino.Equals("011")) return 2.70;
            }

            if (origem.Equals("018"))
            {
                if (destino.Equals("011")) return 1.90;
            }

            return null;
        }
    }
}
