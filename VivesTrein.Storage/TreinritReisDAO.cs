using Microsoft.EntityFrameworkCore;
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

        public IEnumerable<TreinritReis> FindByReisId(int? Id)
        {
            return _db.TreinritReis
                .Where(r => r.Id == Id)
                .ToList();
        }

        public void Create(TreinritReis entity)
        {
            _db.Entry(entity).State = EntityState.Added;
            _db.SaveChanges();
        }

        public void Delete(TreinritReis entity)
        {
            _db.Remove(entity).State = EntityState.Deleted;
            _db.SaveChanges();
        }
    }
}
