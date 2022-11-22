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
        public readonly SkynetzDbContext _db;

        [Fact]
        public void Should_Return_Success_GetAllPlans()
        {
            //Arrange
            var data = new List<Plan> { new Plan { Id = 1, Name = "FaleMais 30", FreeMinutes = 30 }, new Plan {Id = 2, Name = "FaleMais 60", FreeMinutes = 60 }, new Plan { Id = 3, Name = "FaleMais 120", FreeMinutes = 120 } }.AsQueryable();
            var mockContext = new Mock<SkynetzDbContext>();
            var mockSet = new Mock<DbSet<Plan>>();

            mockSet.As<IDbAsyncEnumerable<Plan>>().Setup(m => m.GetAsyncEnumerator()).Returns(new TestDbAsyncEnumerator<Plan>(data.GetEnumerator()));
            mockSet.As<IQueryable<Plan>>().Setup(m => m.Provider).Returns(new TestDbAsyncQueryProvider<Plan>(data.Provider));

            mockSet.As<IQueryable<Plan>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Plan>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Plan>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator);
            mockContext.Setup(m => m.Set<Plan>()).Returns(mockSet.Object);

            //Act
            var repository = new Mock<PlanRepository>(mockContext.Object);
            var plans = repository.Object.GetAll();

            //Assert
            Assert.NotNull(plans);
            Assert.Equal(data.Count(), plans.Count());
        }

        [Fact]
        public void Should_Return_Success_GetAllPlans2()
        {
            //Arrange
            var data = new List<Plan> { new Plan { Id = 1, Name = "FaleMais 30", FreeMinutes = 30 }, new Plan { Id = 2, Name = "FaleMais 60", FreeMinutes = 60 }, new Plan { Id = 3, Name = "FaleMais 120", FreeMinutes = 120 } }.AsQueryable();
            Mock<IPlanRepository> mock = new Mock<IPlanRepository>();
            mock.Setup(m => m.GetAll()).Returns(data);
            PlanRepository planRepository = new PlanRepository(_db);

            //Act
            var expected = mock.Object.GetAll();
            var result = planRepository.GetAll();

            //Assert
            Assert.NotNull(expected);
            Assert.Equal(expected.Count(), result.Count());
        }
    }
}
