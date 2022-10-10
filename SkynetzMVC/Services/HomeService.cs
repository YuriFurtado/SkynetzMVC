using SkynetzMVC.Models;
using SkynetzMVC.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SkynetzMVC.Services
{
    public class HomeService
    {
        public readonly TariffRepository tarifaRepository;
        public readonly PlanRepository planoRepository;

        public HomeService()
        {
            tarifaRepository = new TariffRepository();
            planoRepository = new PlanRepository();
        }

        public string Result(string origem, string destino, int minutosUsados, string planoUsado)
        {
            FilterPlan filtroPlano = new FilterPlan() { Name = planoUsado};
            FilterTariff filtroTarifa = new FilterTariff() { Source = origem, Destination = destino };

            Tariff tarifa = tarifaRepository.GetByParameters(filtroTarifa).FirstOrDefault();
            Plan plano = planoRepository.GetByParameters(filtroPlano).FirstOrDefault();

            double precoSemPlano = tarifa.MinuteValue * minutosUsados;
            double precoComPlano;
            string Results = string.Empty;

            if (plano.FreeMinutes >= minutosUsados)
            {
                precoComPlano = 0;
            }
            else
            {
                precoComPlano = (minutosUsados - plano.FreeMinutes) * (tarifa.MinuteValue * 1.10);
            }

            Results += "Origem: " + tarifa.Source + " - Destino: " + tarifa.Destination + " - Tempo: " + minutosUsados;
            Results += " - Plano: " + plano.Name + " - Com FaleMais: " + precoComPlano.ToString("N2") + " - Sem FaleMais: " + precoSemPlano.ToString("N2");

            return Results;
        }

        public string ResultsDinamic(FilterPlan filtroPlano, FilterTariff filtroTarifa, int minutosUsados)
        {
            List<Tariff> tarifas = tarifaRepository.GetByParameters(filtroTarifa);
            List<Plan> planos = planoRepository.GetByParameters(filtroPlano);

            double precoSemPlano;
            double precoComPlano;
            string Results = string.Empty;

            foreach (Tariff tarifa in tarifas)
            {
                foreach (Plan plano in planos)
                {
                    precoSemPlano = tarifa.MinuteValue * minutosUsados;

                    if (plano.FreeMinutes >= minutosUsados)
                    {
                        precoComPlano = 0;
                    }
                    else
                    {
                        precoComPlano = (minutosUsados - plano.FreeMinutes) * (tarifa.MinuteValue * 1.10);
                    }

                    Results += "Origem: " + tarifa.Source + " - Destino: " + tarifa.Destination + " - Tempo: " + minutosUsados;
                    Results += " - Plano: " + plano.Name + " - Com FaleMais: " + precoComPlano.ToString("N2") + " - Sem FaleMais: " + precoSemPlano.ToString("N2") + Environment.NewLine;
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
