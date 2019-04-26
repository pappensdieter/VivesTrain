using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VivesTrein.Domain;
using VivesTrein.Domain.Entities;

namespace VivesTrein.Storage
{
    public class ReisDAO
    {
        private readonly vivestrainContext _db;

        public ReisDAO()
        {
            _db = new vivestrainContext();
        }

        public IEnumerable<Reis> GetAll()
        {
            return _db.Reis.ToList();
        }
    }
}
