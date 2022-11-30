using SkynetzAPI.Controllers.Tariff.Response;
using SkynetzMVC.Models;
using SkynetzMVC.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SkynetzAPI.Services
{
    public class TariffService
    {
        public readonly TariffRepository tariffRepository;

        public TariffService(SkynetzDbContext db)
        {
            tariffRepository = new TariffRepository(db);
        }

        public List<TariffDTO> GetAllTariffs()
        {
            List<Tariff> tariffs = tariffRepository.GetAll();

            List<TariffDTO> tariffsDTOS = new List<TariffDTO>();

            foreach(Tariff tariff in tariffs)
            {
                TariffDTO tariffDTO = new TariffDTO
                {
                    Source = tariff.Source,
                    Destination = tariff.Destination,
                    MinuteValue = tariff.MinuteValue
                };

                tariffsDTOS.Add(tariffDTO);
            }

            return tariffsDTOS;
        }

        public TariffDTO GetTariffById(int id)
        {
            Tariff tariff = tariffRepository.GetTariffById(id);

            TariffDTO tariffDTO = new TariffDTO
            {
                Source = tariff.Source,
                Destination = tariff.Destination,
                MinuteValue = tariff.MinuteValue
            };

            return tariffDTO;
        }

        public List<TariffDTO> GetTariffsByParameter(FilterTariff filterTariff)
        {
            List<Tariff> tariffs = tariffRepository.GetByParameters(filterTariff);

            List<TariffDTO> tariffsDTOS = new List<TariffDTO>();

            foreach(Tariff tariff in tariffs)
            {
                TariffDTO tariffDTO = new TariffDTO
                {
                    Source = tariff.Source,
                    Destination = tariff.Destination,
                    MinuteValue = tariff.MinuteValue
                };

                tariffsDTOS.Add(tariffDTO);
            }

            return tariffsDTOS;
        }
    }
}
