using Microsoft.VisualStudio.TestTools.UnitTesting;
using SkynetzMVC.Models;
using SkynetzMVC.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace SkynetzMVC.Test
{
    public class ListTest
    {

        [Theory]
        [InlineData("011", null, null)]
        public void TestFilterTariff(string? source, string? destination, double? minuteValue)
        {
            TariffRepository tariffRepository = new TariffRepository();

            List<Tariff> expectedTariffs = new List<Tariff>()
            {
                new Tariff() { Id = 1, Source = "011", Destination = "016", MinuteValue = 1.90},
                new Tariff() { Id = 3, Source = "011", Destination = "017", MinuteValue = 1.70},
                new Tariff() { Id = 5, Source = "011", Destination = "018", MinuteValue = 0.90}
            };

            FilterTariff filterTariff = new FilterTariff() { Source = source, Destination = destination, MinuteValue = minuteValue };

            List<Tariff> tariffs = tariffRepository.GetByParameters(filterTariff);

            CollectionAssert.Equals(expectedTariffs, tariffs);
        }

        [Theory]
        [InlineData(null, 60)]
        public void TestFilterPlan(string? name, int? freeMinutes)
        {
            PlanRepository planRepository = new PlanRepository();

            List<Plan> expectedPlans = new List<Plan>()
            {
                new Plan(){ Id = 2, Name = "FaleMais 60", FreeMinutes = 60}
            };

            FilterPlan filterPlan = new FilterPlan() { Name = name, FreeMinutes = freeMinutes };

            List<Plan> plans = planRepository.GetByParameters(filterPlan);

            CollectionAssert.Equals(expectedPlans, plans);
        }
    }
}
