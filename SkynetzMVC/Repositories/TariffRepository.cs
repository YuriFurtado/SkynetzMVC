using Microsoft.EntityFrameworkCore;
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
        public readonly SkynetzDbContext _db;

        public TariffRepository(SkynetzDbContext db)
        {
            _db = db;
        }

        public List<Tariff> GetAll() 
        { 
            return _db.Tariffs.ToList();
        }


        public Tariff GetTariffById(int Id) 
        {
            return _db.Tariffs.AsQueryable().Where(x => x.Id == Id).FirstOrDefault();
        }

        public List<Tariff> GetByParameters(FilterTariff filters)
        {
            var query = _db.Tariffs.AsQueryable();

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
            _db.Tariffs.Add(tariff);
            _db.SaveChanges();
            return GetTariffById(tariff.Id);
        }


        public List<Tariff> InsertRangeTariff(List<Tariff> newTariffs)
        {
            _db.Tariffs.AddRange(newTariffs);
            _db.SaveChanges();
            return _db.Tariffs.ToList();
        }


        public Tariff UpdateTariff(Tariff tariff) 
        {
            var update = GetTariffById(tariff.Id);
            update.Source = tariff.Source;
            update.Destination = tariff.Destination;
            update.MinuteValue = tariff.MinuteValue;
            _db.Entry(update).State = EntityState.Modified;
            _db.SaveChanges();
            return GetTariffById(update.Id);
        }

        public bool DeleteTariff(int id)
        {
            var delete = GetTariffById(id);
            _db.Tariffs.Remove(delete);
            _db.SaveChanges();
            var HasTariff = GetTariffById(delete.Id);
            if (HasTariff == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public List<Tariff> DeleteRangeTariff(List<Tariff> tariffsRemoved)
        {
            foreach(Tariff tariff in tariffsRemoved)
            {
                _db.Tariffs.Remove(tariff);
            }
            return _db.Tariffs.ToList();
        }


    }

    public class FilterTariff
    {
        public int? Id { get; set; }
        public string? Source { get; set; }
        public string? Destination { get; set; }
        public double? MinuteValue { get; set; }
    }
}
