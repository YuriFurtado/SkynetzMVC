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
                var tariffDTO = ModelToDTO(tariff);

                tariffsDTOS.Add(tariffDTO);
            }

            return tariffsDTOS;
        }

        public TariffDTO GetTariffById(int id)
        {
            Tariff tariff = tariffRepository.GetTariffById(id);

            var tariffDTO = ModelToDTO(tariff);

            return tariffDTO;
        }

        public List<TariffDTO> GetTariffsByParameter(TariffDTO filterDTO)
        {
            FilterTariff filterTariff = new FilterTariff
            {
                Id = filterDTO.Id,
                Source = filterDTO.Source,
                Destination = filterDTO.Destination,
                MinuteValue = filterDTO.MinuteValue
            };

            List<Tariff> tariffs = tariffRepository.GetByParameters(filterTariff);

            List<TariffDTO> tariffsDTOS = new List<TariffDTO>();

            foreach(Tariff tariff in tariffs)
            {
                var tariffDTO = ModelToDTO(tariff);

                tariffsDTOS.Add(tariffDTO);
            }

            return tariffsDTOS;
        }

        public bool InsertTariff(TariffDTO tariffDTO)
        {
            Tariff insertTariff = DTOToModel(tariffDTO);

            Tariff returnTariff = tariffRepository.InsertTariff(insertTariff);

            return returnTariff != null;
        }

        public bool UpdateTariff(TariffDTO tariffDTO)
        {
            Tariff updateTariff = DTOToModel(tariffDTO);

            Tariff returnTariff = tariffRepository.UpdateTariff(updateTariff);

            return returnTariff != null;
        }

        public TariffDTO ModelToDTO(Tariff tariff)
        {
            return new TariffDTO
            {
                Id = tariff.Id,
                Source = tariff.Source,
                Destination = tariff.Destination,
                MinuteValue = tariff.MinuteValue
            };
        }

        public Tariff DTOToModel(TariffDTO tariffDTO)
        {
            return new Tariff
            {
                Id = (int)tariffDTO.Id,
                Source = tariffDTO.Source,
                Destination = tariffDTO.Destination,
                MinuteValue = (double)tariffDTO.MinuteValue
            };
        }
    }
}
