using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VivesTrein.Domain;
using VivesTrein.Domain.Entities;
using VivesTrein.Storage.Interfaces;

namespace VivesTrein.Storage
{
    public class TreinritDAO : IDAO<Treinrit>
    {
        private readonly vivestrainContext _db;

        public TreinritDAO()
        {
            _db = new vivestrainContext();
        }

        public IEnumerable<Treinrit> GetAll()
        {
            return _db.Treinrit.ToList();
        }

        public Treinrit FindTreinrit(Stad vertrekstad, Stad aankomststad, DateTime vertrekuur)
        {
            return _db.Treinrit
                          .Where(x => x.Vertrekstad == vertrekstad && x.Bestemmingsstad == aankomststad && x.Vertrek >= vertrekuur).FirstOrDefault();
        }

        public Treinrit FindById(int? Id)
        {
            return _db.Treinrit
                .Where(r => r.Id == Id)
                .First();
        }

        public void Create(Treinrit entity)
        {
            _db.Entry(entity).State = EntityState.Added;
            _db.SaveChanges();
        }

        public void Delete(Treinrit entity)
        {
            throw new NotImplementedException();
        }

        public void UpdateVrijeplaatsen(int treinritId)
        {
            Treinrit treinrit = _db.Treinrit
                .Where(r => r.Id == treinritId)
                .First();
            treinrit.Vrijeplaatsen = treinrit.Vrijeplaatsen - 1;
            _db.Entry(treinrit).State = EntityState.Modified;
            _db.SaveChanges();
        }

        public void Update(Treinrit entity)
        {
            _db.Entry(entity).State = EntityState.Modified;
            _db.SaveChanges();
        }
    }
}
