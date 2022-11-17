using SkynetzMVC.Models;
using SkynetzMVC.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SkynetzMVC.Interfaces
{
    public interface ITariffRepository
    {
        List<Tariff> GetAll();
        Tariff GetTariffById(int Id);
        List<Tariff> GetByParameters(FilterTariff filters);
        Tariff InsertTariff(Tariff tariff);
        List<Tariff> InsertRangeTariff(List<Tariff> newTariffs);
        Tariff UpdateTarifa(Tariff tariff);
        bool DeleteTarifa(int id);
        List<Tariff> DeleteRangeTariff(List<Tariff> tariffsRemoved);
        
    }
}
