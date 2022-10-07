using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using Microsoft.VisualBasic;
using SkynetzMVC.Models;
using SkynetzMVC.Repositories;
using SkynetzMVC.Services;
using Xunit;
//using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SkynetzMVC.Test
{
    public class UnitTestRepository
    {
        [Theory]
        [InlineData(1, "012", "011", 2.00)]
        public void TestUpdateTarifa(int id, string origem, string destino, double valorMinuto)
        {
            TarifaRepository tarifaRepository = new TarifaRepository();

            var novaTarifa = new Tarifa() { Id = id , Origem = origem, Destino = destino, ValorMinuto = valorMinuto}; 

            Tarifa Retorno = tarifaRepository.UpdateTarifa(novaTarifa);

            Assert.Equal(novaTarifa.Id, Retorno.Id);
            Assert.Equal(novaTarifa.Origem, Retorno.Origem);
            Assert.Equal(novaTarifa.Destino, Retorno.Destino);
            Assert.Equal(novaTarifa.ValorMinuto, Retorno.ValorMinuto);
        }

        [Theory]
        [InlineData(2)]
        public void TestSelectTarifa(int id)
        {
            TarifaRepository tarifaRepository = new TarifaRepository();

            var tarifaComparada = new Tarifa() { Id = 2, Origem = "016", Destino = "011", ValorMinuto = 2.90 };

            Tarifa Retorno = tarifaRepository.GetTarifaById(id);

            Assert.Equal(tarifaComparada.Id, Retorno.Id);
            Assert.Equal(tarifaComparada.Origem, Retorno.Origem);
            Assert.Equal(tarifaComparada.Destino, Retorno.Destino);
            Assert.Equal(tarifaComparada.ValorMinuto, Retorno.ValorMinuto);
        }

        [Theory]
        [InlineData(7, "013", "022", 1.50)]
        public void TestInsertTarifa(int id, string origem, string destino, double valorMinuto)
        {
            TarifaRepository tarifaRepository = new TarifaRepository();
            
            var tarifaInserida = new Tarifa() { Id = id, Origem = origem, Destino = destino, ValorMinuto = valorMinuto };

            Tarifa Retorno = tarifaRepository.InsertTarifa(tarifaInserida);

            Assert.Equal(tarifaInserida.Id, Retorno.Id);
            Assert.Equal(tarifaInserida.Origem, Retorno.Origem);
            Assert.Equal(tarifaInserida.Destino, Retorno.Destino);
            Assert.Equal(tarifaInserida.ValorMinuto, Retorno.ValorMinuto);
        }


        
        [Theory]
        [InlineData()]
        public void TestInsertRangeTarifa()
        {
            TarifaRepository tarifaRepository = new TarifaRepository();

            List<Tarifa> novasTarifas = new List<Tarifa>()
            {
                new Tarifa() { Id = 10, Origem = "013", Destino = "022", ValorMinuto = 1.50},
                new Tarifa() { Id = 11, Origem = "022", Destino = "011", ValorMinuto = 2.50}
            };
        

            List<Tarifa> listaComparacao = tarifaRepository.InsertRangeTarifa(novasTarifas);
            foreach(Tarifa tarifa in novasTarifas)
            {
                Assert.Contains(tarifa, listaComparacao);
            }
        }

            [Theory]
        [InlineData(3)]
        public void TestDeleteTarifa(int id)
        {
            TarifaRepository tarifaRepository = new TarifaRepository();

            bool Retorno = tarifaRepository.DeleteTarifa(id);

            Assert.False(Retorno);
        }

        [Theory]
        [InlineData()]
        public void TestDeleteRangeTarifa()
        {
            TarifaRepository tarifaRepository = new TarifaRepository();

            List<Tarifa> tarifasRemovidas = new List<Tarifa>()
            {
                new Tarifa() { Id = 4, Origem = "017", Destino = "011", ValorMinuto = 2.70},
                new Tarifa() { Id = 5, Origem = "011", Destino = "018", ValorMinuto = 0.90}
            };


            List<Tarifa> listaComparacao = tarifaRepository.DeleteRangeTarifa(tarifasRemovidas);
            foreach (Tarifa tarifa in tarifasRemovidas)
            {
                Assert.DoesNotContain(tarifa, listaComparacao);
            }
        }

        // =================== Testes Plano ===================

        [Theory]
        [InlineData(1, "FaleMais 40", 40)]
        public void TestUpdatePlano(int id, string nome, int minutosGratis)
        {
            PlanoRepository planoRepository = new PlanoRepository();

            var novoPlano = new Plano() { Id = id, Nome = nome, MinutosGratis = minutosGratis };

            Plano Retorno = planoRepository.UpdatePlano(novoPlano);

            Assert.Equal(novoPlano.Id, Retorno.Id);
            Assert.Equal(novoPlano.Nome, Retorno.Nome);
            Assert.Equal(novoPlano.MinutosGratis, Retorno.MinutosGratis);
        }

        [Theory]
        [InlineData(2)]
        public void TestSelectPlano(int id)
        {
            PlanoRepository planoRepository = new PlanoRepository();

            var planoComparado = new Plano() { Id = 2, Nome = "FaleMais 60", MinutosGratis = 60 };

            Plano Retorno = planoRepository.GetPlanoById(id);

            Assert.Equal(planoComparado.Id, Retorno.Id);
            Assert.Equal(planoComparado.Nome, Retorno.Nome);
            Assert.Equal(planoComparado.MinutosGratis, Retorno.MinutosGratis);
        }

        [Theory]
        [InlineData(4, "FaleMais 240", 240)]
        public void TestInsertPlano(int id, string nome, int minutosGratis)
        {
            PlanoRepository planoRepository = new PlanoRepository();

            Plano planoInserido = new Plano() { Id = id, Nome = nome, MinutosGratis = minutosGratis };

            Plano Retorno = planoRepository.InsertPlano(planoInserido);

            Assert.Equal(planoInserido.Id, Retorno.Id);
            Assert.Equal(planoInserido.Nome, Retorno.Nome);
            Assert.Equal(planoInserido.MinutosGratis, Retorno.MinutosGratis);
        }

        [Theory]
        [InlineData(3)]
        public void TestDeletePlano(int id)
        {
            PlanoRepository planoRepository = new PlanoRepository();

            bool Retorno = planoRepository.DeletePlano(id);

            Assert.False(Retorno);
        }

        [Theory]
        [InlineData()]
        public void TestInsertRangePlano()
        {
            PlanoRepository planoRepository = new PlanoRepository();

            List<Plano> novosPlanos = new List<Plano>()
            {
                new Plano() { Id = 10, Nome = "FaleMais 80", MinutosGratis = 80},
                new Plano() { Id = 11, Nome = "FaleMais 100", MinutosGratis = 100},
            };


            List<Plano> listaComparacao = planoRepository.InsertRangePlano(novosPlanos);
            foreach (Plano plano in novosPlanos)
            {
                Assert.Contains(plano, listaComparacao);
            }
        }

        [Theory]
        [InlineData()]
        public void TestDeleteRangePlano()
        {
            PlanoRepository planoRepository = new PlanoRepository();

            List<Plano> planosRemovidos = new List<Plano>()
            {
                new Plano(){ Id = 2, Nome = "FaleMais 60", MinutosGratis = 60},
                new Plano(){ Id = 3, Nome = "FaleMais 120", MinutosGratis = 120}
            };


            List<Plano> listaComparacao = planoRepository.DeleteRangePlano(planosRemovidos);
            foreach (Plano plano in planosRemovidos)
            {
                Assert.DoesNotContain(plano, listaComparacao);
            }
        }

        // ================ Teste Service ================

        [Theory]
        [InlineData("011", "016", 31, "FaleMais 30")]
        public void TestResult(string origem, string destino, int minutosUsados, string planoUsado)
        {
            HomeService homeService = new HomeService();

            var Result = homeService.Result(origem, destino, minutosUsados, planoUsado);

            string expectedResult = "Origem: 011 - Destino: 016 - Tempo: 31 - Plano: FaleMais 30 - Com FaleMais: 2,09 - Sem FaleMais: 58,90";

            Assert.Equal(expectedResult, Result);
        }

        [Theory]
        [InlineData("011", null, 30, "FaleMais 30")]
        public void TestResultDinamic(string? origem, string? destino, int minutosUsados, string planoUsado)
        {
            HomeService homeService = new HomeService();

            FiltroTarifa filtroTarifa = new FiltroTarifa() { Origem = origem, Destino = destino };

            FiltroPlano filtroPlano = new FiltroPlano() { Nome = planoUsado };

            var Results = homeService.ResultsDinamic(filtroPlano, filtroTarifa, minutosUsados);

            string expectedResult = "Origem: 011 - Destino: 016 - Tempo: 30 - Plano: FaleMais 30 - Com FaleMais: 0,00 - Sem FaleMais: 57,00" + Environment.NewLine;
            expectedResult += "Origem: 011 - Destino: 017 - Tempo: 30 - Plano: FaleMais 30 - Com FaleMais: 0,00 - Sem FaleMais: 51,00" + Environment.NewLine;
            expectedResult += "Origem: 011 - Destino: 018 - Tempo: 30 - Plano: FaleMais 30 - Com FaleMais: 0,00 - Sem FaleMais: 27,00" + Environment.NewLine;

            Assert.Equal(expectedResult, Results);
        }

    }
}
