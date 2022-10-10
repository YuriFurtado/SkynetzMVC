using Microsoft.Win32;
using SkynetzMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace SkynetzMVC.Repositories
{
    public class TariffRepository
    {
        public TariffRepository()
        {

        }

        public List<Tariff> Tariffs = new List<Tariff>()
        {
            new Tariff() { Id = 1, Source = "011", Destination = "016", MinuteValue = 1.90},
            new Tariff() { Id = 2, Source = "016", Destination = "011", MinuteValue = 2.90},
            new Tariff() { Id = 3, Source = "011", Destination = "017", MinuteValue = 1.70},
            new Tariff() { Id = 4, Source = "017", Destination = "011", MinuteValue = 2.70},
            new Tariff() { Id = 5, Source = "011", Destination = "018", MinuteValue = 0.90},
            new Tariff() { Id = 6, Source = "018", Destination = "011", MinuteValue = 1.90}
        };

        public List<Tariff> GetAll() 
        { 
            return Tariffs.ToList();
        }


        public Tariff GetTariffById(int Id) 
        {
            return Tariffs.AsQueryable().Where(x => x.Id == Id).FirstOrDefault();
        }

        public List<Tariff> GetByParameters(FilterTariff filters)
        {
            var query = Tariffs.AsQueryable();

            if (!string.IsNullOrEmpty(filters.Source))
            {
                query = query.Where(x => x.Source == filters.Source);
            }

            if (!string.IsNullOrEmpty(filters.Destination))
            {
                query = query.Where(x => x.Destination == filters.Destination);
            }

            if (filters.MinuteValue != null)
            {
                query = query.Where(x => x.MinuteValue == filters.MinuteValue);
            }

            return query.ToList();
        }

        public Tariff InsertTariff(Tariff tariff)
        {
            Tariffs.Add(tariff);
            return GetTariffById(tariff.Id);
        }


        public List<Tariff> InsertRangeTariff(List<Tariff> newTariffs)
        {
            Tariffs.AddRange(newTariffs);
            return Tariffs;
        }


        public Tariff UpdateTarifa(Tariff tariff) 
        {
            var update = GetTariffById(tariff.Id);
            update.Source = tariff.Source;
            update.Destination = tariff.Destination;
            update.MinuteValue = tariff.MinuteValue;
            return GetTariffById(update.Id);
        }

        public bool DeleteTarifa(int id)
        {
            var delete = GetTariffById(id);
            Tariffs.Remove(delete);
            bool HasTarifa = Tariffs.Contains(delete);
            return HasTarifa;
        }
        public List<Tariff> DeleteRangeTariff(List<Tariff> tariffsRemoved)
        {
            foreach(Tariff tariff in tariffsRemoved)
            {
                Tariffs.Remove(tariff);
            }
            return Tariffs;
        }


    }

    public class FilterTariff
    {
        public string? Source { get; set; }
        public string? Destination { get; set; }
        public double? MinuteValue { get; set; }
    }
}
