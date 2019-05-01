using System;
using System.Collections.Generic;
using System.Text;
using VivesTrein.Domain.Entities;
using VivesTrein.Domain;
using VivesTrein.Storage;
using VivesTrein.Utilities;

namespace VivesTrein.Service
{
    public class StadService
    {
        private StadDAO stadDAO;
        private TreinritHelper treinritHelper;
        private IList<Stad> route;

        public StadService()
        {
            stadDAO = new StadDAO();
            treinritHelper = new TreinritHelper();
            route = new List<Stad>();
        }

        public Stad FindById(int id)
        {
            return stadDAO.FindById(id);
        }

        public IEnumerable<Stad> GetAll()
        {
            return stadDAO.GetAll();
        }

        public IList<Stad> GetTussenstoppen(Stad vertrekstad, Stad aankomststad){
            var tussenstoppen = treinritHelper.FindRoute(vertrekstad, aankomststad);

            route.Add(vertrekstad);
            if(tussenstoppen != null)
            {
                foreach(Stad stad in tussenstoppen)
                {
                    route.Add(stad);
                }
            }
            route.Add(aankomststad);

            return route;
        }

    }
}
