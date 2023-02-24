using Microsoft.EntityFrameworkCore;
using Moq;
using SkynetzMVC.Interfaces;
using SkynetzMVC.Models;
using SkynetzMVC.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using Xunit;

namespace SkynetzMVC.Test
{
    public class RepositoryTest
    {
        //public Mock<SkynetzDbContext> mockContext = new Mock<SkynetzDbContext>();

        //public RepositoryTest(Mock<SkynetzDbContext> db = null)
        //{
        //    mockContext = db ?? new Mock<SkynetzDbContext>();
        //}
        //public readonly SkynetzDbContext _db;

        //[Fact]
        //public void Should_Return_Success_GetAllPlans()
        //{
        //    //Arrange
        //    var data = new List<Plan> { new Plan { Id = 1, Name = "FaleMais 30", FreeMinutes = 30 }, new Plan {Id = 2, Name = "FaleMais 60", FreeMinutes = 60 }, new Plan { Id = 3, Name = "FaleMais 120", FreeMinutes = 120 } }.AsQueryable();
        //    var mockContext = new Mock<SkynetzDbContext>();
        //    var mockSet = new Mock<DbSet<Plan>>();

        //    mockSet.As<IDbAsyncEnumerable<Plan>>().Setup(m => m.GetAsyncEnumerator()).Returns(new TestDbAsyncEnumerator<Plan>(data.GetEnumerator()));
        //    mockSet.As<IQueryable<Plan>>().Setup(m => m.Provider).Returns(new TestDbAsyncQueryProvider<Plan>(data.Provider));

        //    mockSet.As<IQueryable<Plan>>().Setup(m => m.Expression).Returns(data.Expression);
        //    mockSet.As<IQueryable<Plan>>().Setup(m => m.ElementType).Returns(data.ElementType);
        //    mockSet.As<IQueryable<Plan>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator);
        //    mockContext.Setup(m => m.Set<Plan>()).Returns(mockSet.Object);

        //    //Act
        //    var repository = new Mock<PlanRepository>(mockContext.Object);
        //    var plans = repository.Object.GetAll();

        //    //Assert
        //    Assert.NotNull(plans);
        //    Assert.Equal(data.Count(), plans.Count());
        //}

        //[Fact]
        //public void Should_Return_Success_GetAllPlans2()
        //{
        //    //Arrange
        //    var data = new List<Plan> { new Plan { Id = 1, Name = "FaleMais 30", FreeMinutes = 30 }, new Plan { Id = 2, Name = "FaleMais 60", FreeMinutes = 60 }, new Plan { Id = 3, Name = "FaleMais 120", FreeMinutes = 120 } };
        //    Mock<IPlanRepository> mock = new Mock<IPlanRepository>();
        //    //mock.Setup(m => m.GetAll()).Returns(data);
        //    mock.Setup(p => p.GetAll()).Returns(data);
        //    PlanRepository planRepository = new PlanRepository(mockContext.Object);

        //    //Act
        //    var expected = mock.Object.GetAll();
        //    var result = planRepository.GetAll();

        //    //Assert
        //    Assert.NotNull(expected);
        //    Assert.Equal(expected.Count(), result.Count());
        //}

        #region Teste PlanRepository

        [Fact]
        public void Should_Return_Success_GetAllPlans3()
        {
            var options = new DbContextOptionsBuilder<SkynetzDbContext>()
                .UseInMemoryDatabase(databaseName: "GetAllPlanDatabase")
                .Options;

            // Insert seed data into the database using one instance of the context
            using (var context = new SkynetzDbContext(options))
            {
                context.Plans.Add(new Plan { Id = 1, Name = "FaleMais 30", FreeMinutes = 30 });
                context.Plans.Add(new Plan { Id = 2, Name = "FaleMais 60", FreeMinutes = 60 });
                context.Plans.Add(new Plan { Id = 3, Name = "FaleMais 120", FreeMinutes = 120 });
                context.SaveChanges();
            }

            // Use a clean instance of the context to run the test
            using (var context = new SkynetzDbContext(options))
            {
                PlanRepository planRepository = new PlanRepository(context);
                List<Plan> plans = planRepository.GetAll();

                Assert.Equal(3, plans.Count);
            }
        }

        [Fact]
        public void Should_Return_Success_GetPlanById()
        {
            var options = new DbContextOptionsBuilder<SkynetzDbContext>()
                .UseInMemoryDatabase(databaseName: "PlanByIdDatabase")
                .Options;

            // Insert seed data into the database using one instance of the context
            using (var context = new SkynetzDbContext(options))
            {
                context.Plans.Add(new Plan { Id = 1, Name = "FaleMais 30", FreeMinutes = 30 });
                context.Plans.Add(new Plan { Id = 2, Name = "FaleMais 60", FreeMinutes = 60 });
                context.Plans.Add(new Plan { Id = 3, Name = "FaleMais 120", FreeMinutes = 120 });
                context.SaveChanges();
            }

            using (var context = new SkynetzDbContext(options))
            {
                PlanRepository planRepository = new PlanRepository(context);
                Plan plan = planRepository.GetPlanById(2);

                Assert.Equal(2, plan.Id);
                Assert.Equal("FaleMais 60", plan.Name);
                Assert.Equal(60, plan.FreeMinutes);
            }

        }

        [Fact]
        public void Should_Return_Success_GetPlanByParameter()
        {
            var options = new DbContextOptionsBuilder<SkynetzDbContext>()
                .UseInMemoryDatabase(databaseName: "PlanByParameterDatabase")
                .Options;

            // Insert seed data into the database using one instance of the context
            using (var context = new SkynetzDbContext(options))
            {
                context.Plans.Add(new Plan { Id = 1, Name = "FaleMais 30", FreeMinutes = 30 });
                context.Plans.Add(new Plan { Id = 2, Name = "FaleMais 60", FreeMinutes = 60 });
                context.Plans.Add(new Plan { Id = 3, Name = "FaleMais 120", FreeMinutes = 120 });
                context.SaveChanges();
            }

            // Use a clean instance of the context to run the test
            using (var context = new SkynetzDbContext(options))
            {
                PlanRepository planRepository = new PlanRepository(context);
                FilterPlan filterPlan = new FilterPlan { Name = "FaleMais 60" };
                List<Plan> plans = planRepository.GetByParameters(filterPlan);

                Assert.Single(plans);
            }
        }

        [Fact]
        public void Should_Return_Success_InsertPlan()
        {
            var options = new DbContextOptionsBuilder<SkynetzDbContext>()
                .UseInMemoryDatabase(databaseName: "InsertPlanDatabase")
                .Options;

            // Insert seed data into the database using one instance of the context
            using (var context = new SkynetzDbContext(options))
            {
                context.Plans.Add(new Plan { Id = 1, Name = "FaleMais 30", FreeMinutes = 30 });
                context.Plans.Add(new Plan { Id = 2, Name = "FaleMais 60", FreeMinutes = 60 });
                context.Plans.Add(new Plan { Id = 3, Name = "FaleMais 120", FreeMinutes = 120 });
                context.SaveChanges();
            }

            // Use a clean instance of the context to run the test
            using (var context = new SkynetzDbContext(options))
            {
                PlanRepository planRepository = new PlanRepository(context);
                Plan InsertPlan = new Plan { };
                InsertPlan.Name = "FaleMais 100";
                InsertPlan.FreeMinutes = 100;
                planRepository.InsertPlan(InsertPlan);
                List<Plan> plans = planRepository.GetAll();

                Assert.Equal(4, plans.Count);
            }
        }

        [Fact]
        public void Should_Return_Success_UpdatePlan()
        {
            var options = new DbContextOptionsBuilder<SkynetzDbContext>()
                .UseInMemoryDatabase(databaseName: "UpdatePlanDatabase")
                .Options;

            // Insert seed data into the database using one instance of the context
            using (var context = new SkynetzDbContext(options))
            {
                context.Plans.Add(new Plan { Id = 1, Name = "FaleMais 30", FreeMinutes = 30 });
                context.Plans.Add(new Plan { Id = 2, Name = "FaleMais 60", FreeMinutes = 60 });
                context.Plans.Add(new Plan { Id = 3, Name = "FaleMais 120", FreeMinutes = 120 });
                context.Plans.Add(new Plan { Id = 4, Name = "FaleMais 100", FreeMinutes = 100 });
                context.SaveChanges();
            }

            // Use a clean instance of the context to run the test
            using (var context = new SkynetzDbContext(options))
            {
                PlanRepository planRepository = new PlanRepository(context);
                Plan updatePlan = new Plan { Id = 4, Name = "FaleMais 110", FreeMinutes = 110 };
                planRepository.UpdatePlan(updatePlan);
                Plan plan = planRepository.GetPlanById(4);

                Assert.Equal(4, plan.Id);
                Assert.Equal("FaleMais 110", plan.Name);
                Assert.Equal(110, plan.FreeMinutes);
            }
        }

        #endregion

        #region Teste TariffRepository

        [Fact]
        public void Should_Return_Success_GetAllTariffs()
        {
            var options = new DbContextOptionsBuilder<SkynetzDbContext>()
                .UseInMemoryDatabase(databaseName: "GetAllTariffDatabase")
                .Options;

            // Insert seed data into the database using one instance of the context
            using (var context = new SkynetzDbContext(options))
            {
                context.Tariffs.Add(new Tariff { Id = 1, Source = "011", Destination = "016", MinuteValue = 1.9});
                context.Tariffs.Add(new Tariff { Id = 2, Source = "016", Destination = "011", MinuteValue = 2.9});
                context.Tariffs.Add(new Tariff { Id = 3, Source = "011", Destination = "017", MinuteValue = 1.7});
                context.Tariffs.Add(new Tariff { Id = 4, Source = "017", Destination = "011", MinuteValue = 2.7});
                context.Tariffs.Add(new Tariff { Id = 5, Source = "011", Destination = "018", MinuteValue = 0.9});
                context.Tariffs.Add(new Tariff { Id = 6, Source = "018", Destination = "011", MinuteValue = 1.9});
                context.SaveChanges();
            }

            // Use a clean instance of the context to run the test
            using (var context = new SkynetzDbContext(options))
            {
                TariffRepository tariffRepository = new TariffRepository(context);
                List<Tariff> tariffs = tariffRepository.GetAll();

                Assert.Equal(6, tariffs.Count);
            }
        }

        #endregion
    }
}
