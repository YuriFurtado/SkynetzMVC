using SkynetzMVC.Models;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace SkynetzMVC.Repositories
{
    public class PlanoRepository
    {
        public PlanoRepository() 
        {
            
        }

        public List<Plano> Planos = new List<Plano>() 
        { 
            new Plano(){ Id = 1, Nome = "FaleMais 30", MinutosGratis = 30},
            new Plano(){ Id = 2, Nome = "FaleMais 60", MinutosGratis = 60},
            new Plano(){ Id = 3, Nome = "FaleMais 120", MinutosGratis = 120}
        };

        public List<Plano> GetAll()
        {
            return Planos.ToList();
        }

        public Plano GetPlanoById(int id) 
        {
            return Planos.AsQueryable().Where(x => x.Id == id).FirstOrDefault();
        }

        public List<Plano> GetByParameters(FiltroPlano filtros)
        {
            var query = Planos.AsQueryable();

            if (!string.IsNullOrEmpty(filtros.Nome))
            {
                query = query.Where(x => x.Nome == filtros.Nome);
            }

            if(filtros.MinutosGratis != null)
            {
                query = query.Where(x => x.MinutosGratis == filtros.MinutosGratis);
            }

            return query.ToList();
        }

        public Plano InsertPlano(Plano plano) 
        { 
            Planos.Add(plano);
            return GetPlanoById(plano.Id);
        }

        public List<Plano> InsertRangePlano(List<Plano> novosPlanos)
        {
            Planos.AddRange(novosPlanos);
            return Planos;
        }

        public Plano UpdatePlano(Plano plano)
        {
            var update = GetPlanoById(plano.Id);
            update.Nome = plano.Nome;
            update.MinutosGratis = plano.MinutosGratis;
            return GetPlanoById(update.Id);
        }

        public bool DeletePlano(int id)
        {
            var delete = GetPlanoById(id);
            Planos.Remove(delete);
            bool HasPlano = Planos.Contains(delete);
            return HasPlano;
        }

        public List<Plano> DeleteRangePlano(List<Plano> planosRemovidos)
        {
            foreach (Plano plano in planosRemovidos)
            {
                Planos.Remove(plano);
            }
            return Planos;
        }
    }

    public class FiltroPlano
    {
        public string? Nome { get; set; }
        public int? MinutosGratis { get; set; }
    }
}
