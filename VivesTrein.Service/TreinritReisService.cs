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

        public IEnumerable<TreinritReis> FindByReisId(int? Id)
        {
            return treinritReisDAO.FindByReisId(Id);
        }

        public void Create(TreinritReis entity)
        {
            treinritReisDAO.Create(entity);
        }

        public void Delete(TreinritReis entity)
        {
            treinritReisDAO.Delete(entity);
        }
    }
}
