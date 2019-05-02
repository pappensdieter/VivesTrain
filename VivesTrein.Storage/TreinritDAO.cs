using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VivesTrein.Domain;
using VivesTrein.Domain.Entities;

namespace VivesTrein.Storage
{
    public class TreinritDAO
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
            using (vivestrainContext db = new vivestrainContext())
            {
                return db.Treinrit
                          .Where(x => x.Vertrekstad == vertrekstad && x.Bestemmingsstad == aankomststad && x.Vertrek >= vertrekuur).FirstOrDefault();
            }
        }
    }
}
