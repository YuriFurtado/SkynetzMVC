using System;
using System.ComponentModel.DataAnnotations;

namespace SkynetzMVC.Models
{
    public class Plan
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public int? FreeMinutes { get; set; }

        public Plan()
        {

        }

        public Plan(int id, string name, int freeMinutes)
        {
            if (id < 0)
                throw new InvalidOperationException();
            if (string.IsNullOrEmpty(name))
                throw new InvalidOperationException();
            if (freeMinutes <= 0)
                throw new ArgumentNullException(nameof(freeMinutes),
                          "A quantidade de minutos grátis deve ser maior que 0");
            Id = id;
            Name = name;
            FreeMinutes = freeMinutes;
        }

        double percentageExceeded = 1.10;

        public bool HaveTimeExceeded (int usedMinutes)
        {
            if (usedMinutes < 0)
            {
                throw new ArgumentException("Valores Negativos não são válidos para a operação");
            }

            return usedMinutes > FreeMinutes ? true : false;
        }

        public int MinutesExceeded (int usedMinutes)
        {
            if (usedMinutes < 0)
            {
                throw new ArgumentException("Valores Negativos não são válidos para a operação");
            }

            return (int)(usedMinutes - FreeMinutes);
        }

        public double PriceWithPlan (int usedMinutes, double valueMinute)
        {
            if (usedMinutes < 0 || valueMinute < 0.0)
            {
                throw new ArgumentException("Valores Negativos não são válidos para a operação");
            }


            if (HaveTimeExceeded(usedMinutes))
            {
                return MinutesExceeded(usedMinutes) * (valueMinute * percentageExceeded);
            }
            else
            {
                return 0.0;
            }
        }

        public double PriceWithouPlan(Tariff tariff, int usedMinutes)
        {
            return tariff.MinuteValue * usedMinutes;
        }

    }
}
