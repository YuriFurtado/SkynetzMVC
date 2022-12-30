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

            foreach (Tariff tariff in tariffs)
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

            foreach (Tariff tariff in tariffs)
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

        public bool DeleteTariff(int id)
        {
            return tariffRepository.DeleteTariff(id);
        }

        public TariffDTO ModelToDTO(Tariff tariff)
        {
            try
            {
                TariffDTO tariffDTO = new TariffDTO { };

                tariffDTO.Id = tariff.Id;
                tariffDTO.Source = tariff.Source;
                tariffDTO.Destination = tariff.Destination;
                tariffDTO.MinuteValue = tariff.MinuteValue;

                return tariffDTO;
            }
            catch
            {
                throw new Exception("Erro ao converter dados");
            }
        }

        public Tariff DTOToModel(TariffDTO tariffDTO)
        {
            try
            {
                Tariff tariff = new Tariff { };

                if (tariffDTO.Id != null)
                {
                    tariff.Id = (int)tariffDTO.Id;
                }
                tariff.Source = tariffDTO.Source;
                tariff.Destination = tariffDTO.Destination;
                tariff.MinuteValue = (double)tariffDTO.MinuteValue;

                return tariff;
            }
            catch
            {
                throw new Exception("Erro ao converter dados");
            }
        }
    }
}
