using System;
using System.Collections.Generic;
using System.Text;
using VivesTrein.Domain.Entities;
using VivesTrein.Domain;
using VivesTrein.Storage;

namespace VivesTrein.Service
{
    public class TreinritService
    {
        private TreinritDAO treinritDAO;

        public TreinritService()
        {
            treinritDAO = new TreinritDAO();
        }

        public IEnumerable<Treinrit> GetAll()
        {
            return treinritDAO.GetAll();
        }
    }
}
