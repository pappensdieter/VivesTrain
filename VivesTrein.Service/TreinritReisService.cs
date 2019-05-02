using System;
using System.Collections.Generic;
using System.Text;
using VivesTrein.Domain.Entities;
using VivesTrein.Storage;

namespace VivesTrein.Service
{
    public class TreinritReisService
    {
        private TreinritReisDAO treinritReisDAO;

        public TreinritReisService()
        {
            treinritReisDAO = new TreinritReisDAO();
        }

        public TreinritReis FindTreinritReis(Treinrit treinrit)
        {
            return treinritReisDAO.FindTreinritReis(treinrit);
        }
    }
}
