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
        private TreinritReisDAO treinritReisDAO;
        private TreinritReisService treinritReisService;
        private TreinritHelper treinritHelper;
        private IList<Treinrit> treinritten;

        public ReisService()
        {
            reisDAO = new ReisDAO();
            treinritDAO = new TreinritDAO();
            treinritReisDAO = new TreinritReisDAO();
            stadService = new StadService();
            treinritService = new TreinritService();
            treinritten = new List<Treinrit>();
            treinritReisService = new TreinritReisService();
        }

        public IEnumerable<Reis> GetAll()
        {
            return reisDAO.GetAll();
        }

        public (Reis, ICollection<TreinritReis>, Boolean vrijeplaats) MakeReis(String naam, Boolean klasse, Stad vertrekstad, Stad aankomststad, DateTime date)
        {
            //Tussenstoppen opvragen
            IList<Stad> steden = stadService.GetTussenstoppen(vertrekstad, aankomststad);
            double? prijs = 0;

            if(steden.Count == 2)
            {
                Treinrit treinrit = treinritService.GetClosestTreinrit(vertrekstad, aankomststad, date);
                prijs = treinrit.Prijs;
            }
            else if(steden.Count > 2)
            {
                DateTime depDate = date;
                for (int i = 0; i < steden.Count - 1; i++)
                {
                    Treinrit treinrit = treinritService.GetClosestTreinrit(steden[i], steden[i + 1], depDate);
                    prijs += treinrit.Prijs;
                    treinritten.Add(treinrit);
                    depDate = treinrit.Aankomst.Value;
                }
            }

            Reis reis = new Reis
            {
                VertrekstadId = vertrekstad.Id,
                Vertrekstad = vertrekstad,
                BestemmingsstadId = aankomststad.Id,
                Bestemmingsstad = aankomststad,
                Naam = naam,
                Prijs = prijs
            };

            Boolean vrijeplaats = true;

            ICollection<TreinritReis> colTreinritreis = new Collection<TreinritReis>();
            foreach(Treinrit treinrit in treinritten)
            {
                treinrit.Vertrekstad = stadService.FindById(treinrit.VertrekstadId);
                treinrit.Bestemmingsstad = stadService.FindById(treinrit.BestemmingsstadId);

                TreinritReis treinritreis = new TreinritReis
                {
                    Treinrit = treinrit,
                    Klasse = klasse,
                    Reis = reis
                };


                TreinritReis foundTreinritReis = treinritReisService.FindTreinritReis(treinrit);
                if (foundTreinritReis != null)
                {   
                    if(foundTreinritReis.Plaats < treinrit.AtlZitplaatsen)
                    {
                        treinritreis.Plaats = foundTreinritReis.Plaats + 1;
                    }
                    else
                    {
                        vrijeplaats = false;
                    }
                }
                else
                {
                    treinritreis.Plaats = 1;
                }

                colTreinritreis.Add(treinritreis);
            }

            reis.TreinritReis = colTreinritreis;

            return (reis, colTreinritreis, vrijeplaats);
        }
    }
}
