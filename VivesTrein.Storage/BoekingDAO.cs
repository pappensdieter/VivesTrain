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
            throw new NotImplementedException();
        }

        public void Delete(Boeking entity)
        {
            throw new NotImplementedException();
        }

        public Boeking FindById(int? Id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Boeking> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Update(Boeking entity)
        {
            throw new NotImplementedException();
        }
    }
}
