using System;
using System.Collections.Generic;
using System.Text;
using VivesTrein.Domain.Entities;
using VivesTrein.Domain;
using VivesTrein.Storage;
using VivesTrein.Utilities;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace VivesTrein.Service
{
    public class ReisService
    {
        private StadService stadService;
        private TreinritService treinritService;
        private ReisDAO reisDAO;
        private TreinritDAO treinritDAO;
        private TreinritHelper treinritHelper;
        private IList<Stad> reisSteden;

        public ReisService()
        {
            reisDAO = new ReisDAO();
            treinritDAO = new TreinritDAO();
            stadService = new StadService();
            treinritService = new TreinritService();
        }

        public IEnumerable<Reis> GetAll()
        {
            return reisDAO.GetAll();
        }

        public Reis MakeReis(Stad vertrekstad, Stad aankomststad, DateTime date)
        {
            //Tussenstoppen opvragen
            var route = stadService.GetTussenstoppen(vertrekstad, aankomststad);

            //Treinritten opvragen
            Treinrit treinrit = treinritService.GetClosestTreinrit(vertrekstad, aankomststad, date);

            Reis reis = new Reis
            {
                VertrekstadId = vertrekstad.Id,
                BestemmingsstadId = aankomststad.Id,
                Naam = "Test"
            };

            TreinritReis treinritreis = new TreinritReis
            {
                Treinrit = treinrit,
                Plaats = 123,
                Klasse = true,
                Reis = reis
            };

            ICollection<TreinritReis> colTreinritreis = new Collection<TreinritReis>();
            colTreinritreis.Add(treinritreis);

            reis.TreinritReis = colTreinritreis;

            return reis;
        }
    }
}
