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
        public void TestFiltroTarifa(string? origem, string? destino, double? valorMinuto)
        {
            TarifaRepository tarifaRepository = new TarifaRepository();

            List<Tarifa> tarifasEsperadas = new List<Tarifa>()
            {
                new Tarifa() { Id = 1, Origem = "011", Destino = "016", ValorMinuto = 1.90},
                new Tarifa() { Id = 3, Origem = "011", Destino = "017", ValorMinuto = 1.70},
                new Tarifa() { Id = 5, Origem = "011", Destino = "018", ValorMinuto = 0.90}
            };

            FiltroTarifa filtroTarifa = new FiltroTarifa() { Origem = origem, Destino = destino, ValorMinuto = valorMinuto };

            List<Tarifa> tarifas = tarifaRepository.GetByParameters(filtroTarifa);

            CollectionAssert.Equals(tarifasEsperadas, tarifas);
        }

        [Theory]
        [InlineData(null, 60)]
        public void TestFiltroPlano(string? nome, int? minutosGratis)
        {
            PlanoRepository planoRepository = new PlanoRepository();

            List<Plano> planosEsperados = new List<Plano>()
            {
                new Plano(){ Id = 2, Nome = "FaleMais 60", MinutosGratis = 60}
            };

            FiltroPlano filtroPlano = new FiltroPlano() { Nome = nome, MinutosGratis = minutosGratis };

            List<Plano> planos = planoRepository.GetByParameters(filtroPlano);

            CollectionAssert.Equals(planosEsperados, planos);
        }
    }
}
