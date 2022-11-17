using SkynetzMVC.Repositories;
using System;
using System.ComponentModel.DataAnnotations;

namespace SkynetzMVC.Models
{
    public class Tariff
    {
        private TariffRepository _tariffRepos;

        [Key]
        public int Id { get; set; }
        public string Source { get; set; }
        public string Destination { get; set; }
        public double MinuteValue { get; set; }

        public Tariff()
        {

        }

        public Tariff(int id, string source, string destination, double minuteValue)
        {
            if (id < 0)
                throw new InvalidOperationException();
            if (string.IsNullOrEmpty(source) || string.IsNullOrEmpty(destination))
                throw new InvalidOperationException();
            if (double.IsNaN(minuteValue))
                throw new InvalidOperationException();
            Id = id;
            Source = source;
            Destination = destination;
            MinuteValue = minuteValue;
        }

        public double PriceWithoutPlan (int usedMinutes)
        {
            if(usedMinutes < 0)
            {
                throw new ArgumentException("Valores Negativos não são válidos para a operação");
            }

            return usedMinutes * MinuteValue;
        }
    }
}
