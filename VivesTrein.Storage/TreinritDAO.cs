using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    }
}
