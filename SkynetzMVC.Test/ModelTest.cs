using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using Microsoft.VisualBasic;
using Moq;
using SkynetzMVC.Controllers.DTO;
using SkynetzMVC.Interfaces;
using SkynetzMVC.Models;
using SkynetzMVC.Repositories;
using SkynetzMVC.Services;
using Xunit;


namespace SkynetzMVC.Test
{
    public class ModelTest
    {
        [Theory(DisplayName = "Deve retornar com sucesso o calculo do preço da ligação sem o uso do plano")]
        [InlineData(5, "012", "011", 2.00)]
        public void Should_Return_Success_PriceWithoutPlan(int id, string source, string destination, double minuteValue)
        {
            //Arrange
            var newTariff = new Tariff();

            newTariff.Id = id;
            newTariff.Source = source;
            newTariff.Destination = destination;
            newTariff.MinuteValue = minuteValue;


            int usedMinutes = 30;
            
            //Act
            var price = newTariff.PriceWithoutPlan(usedMinutes);

            //Assert
            Assert.Equal(60.00, price);
        }

        [Theory(DisplayName = "Deve retornar o erro tratado ao calcular o preço da ligação sem o uso do plano")]
        [InlineData(5, "012", "011", 2.00)]
        public void Should_Return_Erro_PriceWithoutPlan(int id, string source, string destination, double minuteValue)
        {
            //Arrange
            var newTariff = new Tariff();

            newTariff.Id = id;
            newTariff.Source = source;
            newTariff.Destination = destination;
            newTariff.MinuteValue = minuteValue;


            int usedMinutes = -30;

            //Act
            var exception = Assert.Throws<ArgumentException>(() => newTariff.PriceWithoutPlan(usedMinutes));

            //Assert
            Assert.Equal("Valores Negativos não são válidos para a operação", exception.Message);
        }

        [Theory(DisplayName = "Deve retornar o tempo excedente com base nos minutos gratuitos")]
        [InlineData(1, "FaleMais 30", 30)]
        public void Should_Return_Success_HaveTimeExceeded(int id, string name, int freeMinutes )
        {
            //Arrange
            var newPlan = new Plan();

            newPlan.Id = id;
            newPlan.Name = name;
            newPlan.FreeMinutes = freeMinutes;

            int usedMinutes = 40;

            //Act
            var timeExceeded = newPlan.HaveTimeExceeded(usedMinutes);

            //Assert
            Assert.True(timeExceeded);
        }

        [Theory(DisplayName = "Deve retornar o erro tratado do tempo excedente com base nos minutos gratuitos")]
        [InlineData(1, "FaleMais 120", 120)]
        public void Should_Return_Erro_HaveTimeExceeded(int id, string name, int freeMinutes)
        {
            //Arrange
            var newPlan = new Plan();

            newPlan.Id = id;
            newPlan.Name = name;
            newPlan.FreeMinutes = freeMinutes;

            int usedMinutes = -40;

            //Act
            var exception = Assert.Throws<ArgumentException>(() => newPlan.HaveTimeExceeded(usedMinutes));

            //Assert
            Assert.Equal("Valores Negativos não são válidos para a operação", exception.Message);
        }

        [Theory(DisplayName = "Deve retornar a quantidade de minutos excedidos")]
        [InlineData(1, "FaleMais 30", 30)]
        public void Should_Return_Success_MinutesExceeded(int id, string name, int freeMinutes )
        {
            //Arrange
            var newPlan = new Plan();

            newPlan.Id = id;
            newPlan.Name = name;
            newPlan.FreeMinutes = freeMinutes;

            int usedMinutes = 40;

            //Act
            var minutesExceeded = newPlan.MinutesExceeded(usedMinutes);

            //Assert
            Assert.Equal(10, minutesExceeded);
        }

        [Theory(DisplayName = "Deve retornar o erro tratado sobre a quantidade de minutos excedidos")]
        [InlineData(1, "FaleMais 30", 30)]
        public void Should_Return_Erro_MinutesExceeded(int id, string name, int freeMinutes)
        {
            //Arrange
            var newPlan = new Plan();

            newPlan.Id = id;
            newPlan.Name = name;
            newPlan.FreeMinutes = freeMinutes;

            int usedMinutes = -41;

            //Act
            var exception = Assert.Throws<ArgumentException>(() => newPlan.MinutesExceeded(usedMinutes));

            //Assert
            Assert.Equal("Valores Negativos não são válidos para a operação", exception.Message);
        }

        [Theory(DisplayName = "Deve retornar o calculo do preço da ligação com base no plano informado")]
        [InlineData(1, "FaleMais 30", 30)]
        public void Should_Return_Success_PriceWithPlan(int id, string name, int freeMinutes )
        {
            //Arrange
            var newPlan = new Plan();

            newPlan.Id = id;
            newPlan.Name = name;
            newPlan.FreeMinutes = freeMinutes;

            int usedMinutes = 40;
            double valueMinute = 1.0;

            //Act
            var priceWithPlan = newPlan.PriceWithPlan(usedMinutes, valueMinute);

            //Assert
            Assert.Equal(11.0, priceWithPlan);
        }

        [Theory(DisplayName = "Deve retornar o erro tratado do preço da ligação com base no plano informado")]
        [InlineData(1, "FaleMais 30", 30)]
        public void Should_Return_Erro_PriceWithPlan(int id, string name, int freeMinutes)
        {
            //Arrange
            var newPlan = new Plan();

            newPlan.Id = id;
            newPlan.Name = name;
            newPlan.FreeMinutes = freeMinutes;

            int usedMinutes = 41;
            double valueMinute = 1.0;
            int negativeUsedMinutes = -41;
            double negativeValueMinute = -1.0;

            //Act
            var exceptionUsedMinutes = Assert.Throws<ArgumentException>(() => newPlan.PriceWithPlan(negativeUsedMinutes, valueMinute));
            var exceptionValueMinutes = Assert.Throws<ArgumentException>(() => newPlan.PriceWithPlan(usedMinutes, negativeValueMinute));
            var exceptionBouthValues = Assert.Throws<ArgumentException>(() => newPlan.PriceWithPlan(negativeUsedMinutes, negativeValueMinute));

            //Assert
            Assert.Equal("Valores Negativos não são válidos para a operação", exceptionUsedMinutes.Message);
            Assert.Equal("Valores Negativos não são válidos para a operação", exceptionValueMinutes.Message);
            Assert.Equal("Valores Negativos não são válidos para a operação", exceptionBouthValues.Message);
        }
    }
}
