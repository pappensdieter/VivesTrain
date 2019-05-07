using System;
using System.Collections.Generic;
using System.Text;
using VivesTrein.Domain.Entities;
using VivesTrein.Domain;
using VivesTrein.Storage;
using VivesTrein.Service.Interfaces;

namespace VivesTrein.Service
{
    public class BoekingService : IService<Boeking>
    {
        private BoekingDAO boekingDAO;

        public BoekingService()
        {
            boekingDAO = new BoekingDAO();
        }

        public void Create(Boeking entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Boeking entity)
        {
            throw new NotImplementedException();
        }

        public Boeking FindById(int? Id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Boeking> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Update(Boeking entity)
        {
            throw new NotImplementedException();
        }
    }
}
