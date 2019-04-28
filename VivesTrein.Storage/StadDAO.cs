using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VivesTrein.Domain;
using VivesTrein.Domain.Entities;

namespace VivesTrein.Storage
{
    public class StadDAO
    {
        private readonly vivestrainContext _db;

        public StadDAO()
        {
            _db = new vivestrainContext();
        }

        public IEnumerable<Stad> GetAll()
        {
            return _db.Stad.ToList();
        }
    }
}
