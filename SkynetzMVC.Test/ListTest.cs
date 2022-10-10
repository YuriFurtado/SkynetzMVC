using Microsoft.VisualStudio.TestTools.UnitTesting;
using SkynetzMVC.Controllers.DTO;
using SkynetzMVC.Models;
using SkynetzMVC.Repositories;
using SkynetzMVC.Services;
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

        [Theory]
        [InlineData("011", null, 30, "FaleMais 30")]
        public void TestResultDinamic(string? source, string? destination, int usedMinutes, string usedPlan)
        {
            HomeService homeService = new HomeService();

            FilterTariff filterTariff = new FilterTariff() { Source = source, Destination = destination };

            FilterPlan filterPlan = new FilterPlan() { Name = usedPlan };

            var Results = homeService.ResultsDinamic(filterPlan, filterTariff, usedMinutes);

            List<ResultDTO> expectedDTOs = new List<ResultDTO>()
            {
                new ResultDTO()
                {
                    Source = "011",
                    Destination = "016",
                    UsedMinutes = 30,
                    UsedPlan = "FaleMais 30",
                    PriceWithPlan = "0,00",
                    PriceWithoutPlan = "57,00"
                },
                new ResultDTO()
                {
                    Source = "011",
                    Destination = "017",
                    UsedMinutes = 30,
                    UsedPlan = "FaleMais 30",
                    PriceWithPlan = "0,00",
                    PriceWithoutPlan = "51,00"
                },
                new ResultDTO()
                {
                    Source = "011",
                    Destination = "018",
                    UsedMinutes = 30,
                    UsedPlan = "FaleMais 30",
                    PriceWithPlan = "0,00",
                    PriceWithoutPlan = "27,00"
                }
            };

            CollectionAssert.Equals(expectedDTOs, Results);
        }
    }
}
