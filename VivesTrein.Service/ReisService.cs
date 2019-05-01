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
        private IList<Treinrit> treinritten;

        public ReisService()
        {
            reisDAO = new ReisDAO();
            treinritDAO = new TreinritDAO();
            stadService = new StadService();
            treinritService = new TreinritService();
            treinritten = new List<Treinrit>();
        }

        public IEnumerable<Reis> GetAll()
        {
            return reisDAO.GetAll();
        }

        public Reis MakeReis(Stad vertrekstad, Stad aankomststad, DateTime date)
        {
            //Tussenstoppen opvragen
            IList<Stad> steden = stadService.GetTussenstoppen(vertrekstad, aankomststad);


            if(steden.Count == 2)
            {
                Treinrit treinrit = treinritService.GetClosestTreinrit(vertrekstad, aankomststad, date);
            }
            else if(steden.Count > 2)
            {
                DateTime depDate = date;
                for (int i = 0; i < steden.Count - 1; i++)
                {
                    Treinrit treinrit = treinritService.GetClosestTreinrit(steden[i], steden[i + 1], depDate);
                    treinritten.Add(treinrit);
                    depDate = treinrit.Aankomst.Value;
                }
            }

            Reis reis = new Reis
            {
                VertrekstadId = vertrekstad.Id,
                BestemmingsstadId = aankomststad.Id,
                Naam = "Test"
                
            };

            ICollection<TreinritReis> colTreinritreis = new Collection<TreinritReis>();
            foreach(Treinrit treinrit in treinritten)
            {
                TreinritReis treinritreis = new TreinritReis
                {
                    Treinrit = treinrit,
                    Plaats = 100,
                    Klasse = false,
                    Reis = reis
                };
                colTreinritreis.Add(treinritreis);
            }

            reis.TreinritReis = colTreinritreis;

            return reis;
        }
    }
}
