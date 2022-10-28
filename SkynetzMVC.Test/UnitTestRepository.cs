using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using Microsoft.VisualBasic;
using SkynetzMVC.Controllers.DTO;
using SkynetzMVC.Models;
using SkynetzMVC.Repositories;
using SkynetzMVC.Services;
using Xunit;

namespace SkynetzMVC.Test
{
    public class UnitTestRepository
    {
        /*[Theory]
        [InlineData(1, "012", "011", 2.00)]
        public void TestUpdateTariff(int id, string source, string destination, double minuteValue)
        {
            TariffRepository tariffRepository = new TariffRepository();

            var newTariff = new Tariff() { Id = id , Source = source, Destination = destination, MinuteValue = minuteValue }; 

            Tariff Retorno = tariffRepository.UpdateTarifa(newTariff);

            Assert.Equal(newTariff.Id, Retorno.Id);
            Assert.Equal(newTariff.Source, Retorno.Source);
            Assert.Equal(newTariff.Destination, Retorno.Destination);
            Assert.Equal(newTariff.MinuteValue, Retorno.MinuteValue);
        }

        [Theory]
        [InlineData(2)]
        public void TestSelectTariff(int id)
        {
            TariffRepository tariffRepository = new TariffRepository();

            var comparedTariff = new Tariff() { Id = 2, Source = "016", Destination = "011", MinuteValue = 2.90 };

            Tariff Return = tariffRepository.GetTariffById(id);

            Assert.Equal(comparedTariff.Id, Return.Id);
            Assert.Equal(comparedTariff.Source, Return.Source);
            Assert.Equal(comparedTariff.Destination, Return.Destination);
            Assert.Equal(comparedTariff.MinuteValue, Return.MinuteValue);
        }

        [Theory]
        [InlineData(7, "013", "022", 1.50)]
        public void TestInsertTariff(int id, string source, string destination, double minuteValue)
        {
            TariffRepository tariffRepository = new TariffRepository();
            
            var insertTariff = new Tariff() { Id = id, Source = source, Destination = destination, MinuteValue = minuteValue };

            Tariff Return = tariffRepository.InsertTariff(insertTariff);

            Assert.Equal(insertTariff.Id, Return.Id);
            Assert.Equal(insertTariff.Source, Return.Source);
            Assert.Equal(insertTariff.Destination, Return.Destination);
            Assert.Equal(insertTariff.MinuteValue, Return.MinuteValue);
        }


        
        [Theory]
        [InlineData()]
        public void TestInsertRangeTariff()
        {
            TariffRepository tariffRepository = new TariffRepository();

            List<Tariff> newsTariffs = new List<Tariff>()
            {
                new Tariff() { Id = 10, Source = "013", Destination = "022", MinuteValue = 1.50},
                new Tariff() { Id = 11, Source = "022", Destination = "011", MinuteValue = 2.50}
            };
        

            List<Tariff> comparedList = tariffRepository.InsertRangeTariff(newsTariffs);
            foreach(Tariff tariff in newsTariffs)
            {
                Assert.Contains(tariff, comparedList);
            }
        }

        [Theory]
        [InlineData(3)]
        public void TestDeleteTariff(int id)
        {
            TariffRepository tariffRepository = new TariffRepository();

            bool Return = tariffRepository.DeleteTarifa(id);

            Assert.False(Return);
        }

        [Theory]
        [InlineData()]
        public void TestDeleteRangeTariff()
        {
            TariffRepository tariffRepository = new TariffRepository();

            List<Tariff> removedTariffs = new List<Tariff>()
            {
                new Tariff() { Id = 4, Source = "017", Destination = "011", MinuteValue = 2.70},
                new Tariff() { Id = 5, Source = "011", Destination = "018", MinuteValue = 0.90}
            };


            List<Tariff> comparedList = tariffRepository.DeleteRangeTariff(removedTariffs);
            foreach (Tariff tariff in removedTariffs)
            {
                Assert.DoesNotContain(tariff, comparedList);
            }
        }

        // =================== Testes Plan ===================

        [Theory]
        [InlineData(1, "FaleMais 40", 40)]
        public void TestUpdatePlan(int id, string name, int freeMinutes)
        {
            PlanRepository planRepository = new PlanRepository();

            var newPlan = new Plan() { Id = id, Name = name, FreeMinutes = freeMinutes };

            Plan Return = planRepository.UpdatePlan(newPlan);

            Assert.Equal(newPlan.Id, Return.Id);
            Assert.Equal(newPlan.Name, Return.Name);
            Assert.Equal(newPlan.FreeMinutes, Return.FreeMinutes);
        }

        [Theory]
        [InlineData(2)]
        public void TestSelectPlan(int id)
        {
            PlanRepository planRepository = new PlanRepository();

            var comparedPlan = new Plan() { Id = 2, Name = "FaleMais 60", FreeMinutes = 60 };

            Plan Return = planRepository.GetPlanById(id);

            Assert.Equal(comparedPlan.Id, Return.Id);
            Assert.Equal(comparedPlan.Name, Return.Name);
            Assert.Equal(comparedPlan.FreeMinutes, Return.FreeMinutes);
        }

        [Theory]
        [InlineData(4, "FaleMais 240", 240)]
        public void TestInsertPlan(int id, string name, int freeMinutes)
        {
            PlanRepository planRepository = new PlanRepository();

            Plan insertPlan = new Plan() { Id = id, Name = name, FreeMinutes = freeMinutes };

            Plan Return = planRepository.InsertPlan(insertPlan);

            Assert.Equal(insertPlan.Id, Return.Id);
            Assert.Equal(insertPlan.Name, Return.Name);
            Assert.Equal(insertPlan.FreeMinutes, Return.FreeMinutes);
        }

        [Theory]
        [InlineData(3)]
        public void TestDeletePlan(int id)
        {
            PlanRepository planRepository = new PlanRepository();

            bool Return = planRepository.DeletePlan(id);

            Assert.False(Return);
        }

        [Theory]
        [InlineData()]
        public void TestInsertRangePlan()
        {
            PlanRepository planRepository = new PlanRepository();

            List<Plan> newPlans = new List<Plan>()
            {
                new Plan() { Id = 10, Name = "FaleMais 80", FreeMinutes = 80},
                new Plan() { Id = 11, Name = "FaleMais 100", FreeMinutes = 100},
            };


            List<Plan> comparedList = planRepository.InsertRangePlan(newPlans);
            foreach (Plan plan in newPlans)
            {
                Assert.Contains(plan, comparedList);
            }
        }

        [Theory]
        [InlineData()]
        public void TestDeleteRangePlan()
        {
            PlanRepository planRepository = new PlanRepository();

            List<Plan> removedPlans = new List<Plan>()
            {
                new Plan(){ Id = 2, Name = "FaleMais 60", FreeMinutes = 60},
                new Plan(){ Id = 3, Name = "FaleMais 120", FreeMinutes = 120}
            };


            List<Plan> comparedList = planRepository.DeleteRangePlan(removedPlans);
            foreach (Plan plan in removedPlans)
            {
                Assert.DoesNotContain(plan, comparedList);
            }
        }

        // ================ Teste Service ================

        //[Theory]
        //[InlineData("011", "016", 31, "FaleMais 30")]
        //public void TestResult(string source, string destination, int usedMinutes, string usedPlan)
        //{
        //    HomeService homeService = new HomeService();

        //    var Result = homeService.Result(source, destination, usedMinutes, usedPlan);

        //    ResultDTO compareDTO = new ResultDTO()
        //    {
        //        Source = "011",
        //        Destination = "016",
        //        UsedMinutes = 31,
        //        UsedPlan = "FaleMais 30",
        //        PriceWithPlan = "2,09",
        //        PriceWithoutPlan = "58,90"
        //    };

        //    Assert.Equal(compareDTO.Source, Result.Source);
        //    Assert.Equal(compareDTO.Destination, Result.Destination);
        //    Assert.Equal(compareDTO.UsedMinutes, Result.UsedMinutes);
        //    Assert.Equal(compareDTO.UsedPlan, Result.UsedPlan);
        //    Assert.Equal(compareDTO.PriceWithPlan, Result.PriceWithPlan);
        //    Assert.Equal(compareDTO.PriceWithoutPlan, Result.PriceWithoutPlan);
        //}
        */
    }
}
