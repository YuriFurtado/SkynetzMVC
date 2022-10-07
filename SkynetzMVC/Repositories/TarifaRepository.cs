using Microsoft.Win32;
using SkynetzMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace SkynetzMVC.Repositories
{
    public class TarifaRepository
    {
        public TarifaRepository()
        {

        }

        public List<Tarifa> Tarifas = new List<Tarifa>()
        {
            new Tarifa() { Id = 1, Origem = "011", Destino = "016", ValorMinuto = 1.90},
            new Tarifa() { Id = 2, Origem = "016", Destino = "011", ValorMinuto = 2.90},
            new Tarifa() { Id = 3, Origem = "011", Destino = "017", ValorMinuto = 1.70},
            new Tarifa() { Id = 4, Origem = "017", Destino = "011", ValorMinuto = 2.70},
            new Tarifa() { Id = 5, Origem = "011", Destino = "018", ValorMinuto = 0.90},
            new Tarifa() { Id = 6, Origem = "018", Destino = "011", ValorMinuto = 1.90}
        };

        public List<Tarifa> GetAll() 
        { 
            return Tarifas.ToList();
        }


        public Tarifa GetTarifaById(int Id) 
        {
            return Tarifas.AsQueryable().Where(x => x.Id == Id).FirstOrDefault();
        }

        public List<Tarifa> GetByParameters(FiltroTarifa filtros)
        {
            var query = Tarifas.AsQueryable();

            if (!string.IsNullOrEmpty(filtros.Origem))
            {
                query = query.Where(x => x.Origem == filtros.Origem);
            }

            if (!string.IsNullOrEmpty(filtros.Destino))
            {
                query = query.Where(x => x.Destino == filtros.Destino);
            }

            if (filtros.ValorMinuto != null)
            {
                query = query.Where(x => x.ValorMinuto == filtros.ValorMinuto);
            }

            return query.ToList();
        }

        public Tarifa InsertTarifa(Tarifa tarifa)
        {
            Tarifas.Add(tarifa);
            return GetTarifaById(tarifa.Id);
        }


        public List<Tarifa> InsertRangeTarifa(List<Tarifa> novasTarifas)
        {
            Tarifas.AddRange(novasTarifas);
            return Tarifas;
        }


        public Tarifa UpdateTarifa(Tarifa tarifa) 
        {
            var update = GetTarifaById(tarifa.Id);
            update.Origem = tarifa.Origem;
            update.Destino = tarifa.Destino;
            update.ValorMinuto = tarifa.ValorMinuto;
            return GetTarifaById(update.Id);
        }

        public bool DeleteTarifa(int id)
        {
            var delete = GetTarifaById(id);
            Tarifas.Remove(delete);
            bool HasTarifa = Tarifas.Contains(delete);
            return HasTarifa;
        }
        public List<Tarifa> DeleteRangeTarifa(List<Tarifa> tarifasRemovidas)
        {
            foreach(Tarifa tarifa in tarifasRemovidas)
            {
                Tarifas.Remove(tarifa);
            }
            return Tarifas;
        }


    }

    public class FiltroTarifa
    {
        public string? Origem { get; set; }
        public string? Destino { get; set; }
        public double? ValorMinuto { get; set; }
    }
}
