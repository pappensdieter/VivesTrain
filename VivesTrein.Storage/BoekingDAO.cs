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
    public class BoekingDAO : IDAO<Boeking>
    {
        private readonly vivestrainContext _db;

        public BoekingDAO()
        {
            _db = new vivestrainContext();
        }

        public void Create(Boeking entity)
        {
            _db.Entry(entity).State = EntityState.Added;
            _db.SaveChanges();
        }

        public void Delete(Boeking entity)
        {
            _db.Remove(entity).State = EntityState.Deleted;
            _db.SaveChanges();
        }

        public Boeking FindById(int? Id)
        {
            return _db.Boeking
                .Where(r => r.Id == Id)
                .First();
        }

        public IEnumerable<Boeking> GetAll()
        {
            return _db.Boeking.ToList();
        }

        public void Update(Boeking entity)
        {
            _db.Entry(entity).State = EntityState.Modified;
            _db.SaveChanges();
        }

        public IEnumerable<Boeking> GetXForUser (string userID, int x)
        {
            return _db.Boeking
                .Where(r => r.UserId == userID)
                .Take(x)
                .ToList();
        }
    }
}
