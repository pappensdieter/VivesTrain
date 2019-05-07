using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VivesTrein.Domain;
using VivesTrein.Domain.Entities;
using VivesTrein.Storage.Interfaces;

namespace VivesTrein.Storage
{
    public class ReisDAO : IDAO<Reis>
    {
        private readonly vivestrainContext _db;

        public ReisDAO()
        {
            _db = new vivestrainContext();
        }

        public void Create(Reis entity)
        {
            _db.Entry(entity).State = EntityState.Added;
            _db.SaveChanges();
        }

        public void Delete(Reis entity)
        {
            _db.Remove(entity).State = EntityState.Deleted;
            _db.SaveChanges();
        }

        public Reis FindById(int? id)
        {
            return _db.Reis
                .Where(r => r.Id == id)
                .First();
        }

        public IEnumerable<Reis> GetAll()
        {
            return _db.Reis.ToList();
        }

        public void Update(Reis entity)
        {
            _db.Entry(entity).State = EntityState.Modified;
            _db.SaveChanges();
        }
   }
}
