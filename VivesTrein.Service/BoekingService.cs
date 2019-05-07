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
            boekingDAO.Create(entity);
        }

        public void Delete(Boeking entity)
        {
            boekingDAO.Delete(entity);
        }

        public Boeking FindById(int? Id)
        {
            return boekingDAO.FindById(Id);
        }

        public IEnumerable<Boeking> GetAll()
        {
            return boekingDAO.GetAll();
        }

        public void Update(Boeking entity)
        {
            boekingDAO.Update(entity);
        }
    }
}
