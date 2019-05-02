using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VivesTrein.Domain.Entities;

namespace VivesTrein.Storage
{
    public class TreinritReisDAO
    {
        private readonly vivestrainContext _db;

        public TreinritReisDAO()
        {
            _db = new vivestrainContext();
        }

        public TreinritReis FindTreinritReis(Treinrit treinrit)
        {
            return _db.TreinritReis.Where(x => x.Treinrit == treinrit).FirstOrDefault();
        }
    }
}
