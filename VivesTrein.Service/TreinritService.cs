﻿using System;
using System.Collections.Generic;
using System.Text;
using VivesTrein.Domain.Entities;
using VivesTrein.Domain;
using VivesTrein.Storage;
using VivesTrein.Service.Interfaces;

namespace VivesTrein.Service
{
    public class TreinritService : IService<Treinrit>
    {
        private TreinritDAO treinritDAO;

        public TreinritService()
        {
            treinritDAO = new TreinritDAO();
        }

        public void Create(Treinrit entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Treinrit entity)
        {
            throw new NotImplementedException();
        }

        public Treinrit FindById(int? Id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Treinrit> GetAll()
        {
            return treinritDAO.GetAll();
        }

        public Treinrit GetClosestTreinrit(Stad vertrekstad, Stad aankomststad, DateTime date)
        {
            return treinritDAO.FindTreinrit(vertrekstad, aankomststad, date);
        }

        public void Update(Treinrit entity)
        {
            throw new NotImplementedException();
        }
    }
}
