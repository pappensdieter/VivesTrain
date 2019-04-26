using System;
using System.Collections.Generic;
using System.Text;
using VivesTrein.Domain.Entities;
using VivesTrein.Domain;
using VivesTrein.Storage;

namespace VivesTrein.Service
{
    public class ReisService
    {
        private ReisDAO reisDAO;

        public ReisService()
        {
            reisDAO = new ReisDAO();
        }

        public IEnumerable<Reis> GetAll()
        {
            return reisDAO.GetAll();
        }
    }
}
