using System;
using System.Collections.Generic;
using System.Text;
using VivesTrein.Domain.Entities;
using VivesTrein.Domain;
using VivesTrein.Storage;

namespace VivesTrein.Service
{
    public class StadService
    {
        private StadDAO stadDAO;

        public StadService()
        {
            stadDAO = new StadDAO();
        }

        public IEnumerable<Stad> GetAll()
        {
            return stadDAO.GetAll();
        }
    }
}
