using System;
using System.Collections.Generic;
using System.Text;
using VivesTrein.Domain.Entities;
using VivesTrein.Domain;
using VivesTrein.Storage;
using VivesTrein.Service.Interfaces;
using VivesTrein.Service.Utilities;

namespace VivesTrein.Service
{
    public class TreinritService : IService<Treinrit>
    {
        private TreinritDAO treinritDAO;

        public TreinritService()
        {
            treinritDAO = new TreinritDAO();
        }

        public void Create(Treinrit entity)
        {
            treinritDAO.Create(entity);
        }

        public void Delete(Treinrit entity)
        {
            throw new NotImplementedException();
        }

        public Treinrit FindById(int? Id)
        {
            return treinritDAO.FindById(Id);
        }

        public IEnumerable<Treinrit> GetAll()
        {
            return treinritDAO.GetAll();
        }

        public Treinrit GetClosestTreinrit(Stad vertrekstad, Stad aankomststad, DateTime date)
        {
            return treinritDAO.FindTreinrit(vertrekstad, aankomststad, date);
        }

        public void Update(Treinrit entity)
        {
            throw new NotImplementedException();
        }
    }
}
