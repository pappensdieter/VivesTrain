using System;
using System.Collections.Generic;
using System.Text;
using VivesTrein.Domain.Entities;
using VivesTrein.Domain;
using VivesTrein.Storage;
using VivesTrein.Utilities;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Linq;

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

        public (ICollection<TreinritReis>, Boolean vrijeplaats) MakeReis(String naam, Boolean klasse, Stad vertrekstad, Stad aankomststad, DateTime date, int aantalZitp)
        {
            //Tussenstoppen opvragen
            IList<Stad> steden = stadService.GetTussenstoppen(vertrekstad, aankomststad);
            double prijs = 0;

            if(steden.Count == 2)
            {
                Treinrit treinrit = treinritService.GetClosestTreinrit(vertrekstad, aankomststad, date);
                treinritten.Add(treinrit);
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
                    depDate = treinrit.Aankomst;
                }
            }


            Reis reis = new Reis
            {
                VertrekstadId = vertrekstad.Id,
                BestemmingsstadId = aankomststad.Id,
                Naam = naam,
                Prijs = prijs
            };

            Boolean vrijeplaats = true;

            ICollection<TreinritReis> colTreinritreis = new Collection<TreinritReis>();
            foreach(Treinrit treinrit in treinritten)
            {
                treinrit.Vertrekstad = stadService.FindById(treinrit.VertrekstadId);
                treinrit.Bestemmingsstad = stadService.FindById(treinrit.BestemmingsstadId);

                //Treinritreis aanmaken
                TreinritReis treinritreis = new TreinritReis
                {
                    Treinrit = treinrit,
                    Klasse = klasse,
                    Reis = reis,
                    ReisId = reis.Id,
                };

                //Zoek de laatst aangemaakte treinritreis van treinrit om de plaats te vinden
                TreinritReis foundTreinritReis = treinritReisService.FindTreinritReis(treinrit);
                if (foundTreinritReis != null)
                {   
                    if(foundTreinritReis.Plaats + aantalZitp <= treinrit.AtlZitplaatsen)
                    {
                        treinritreis.Plaats = foundTreinritReis.Plaats + aantalZitp;
                    }
                    else
                    {
                        vrijeplaats = false;
                    }
                }
                else
                {
                    treinritreis.Plaats = aantalZitp;
                }
                colTreinritreis.Add(treinritreis);
            }

            reis.TreinritReis = colTreinritreis;

            Create(reis);

            return (colTreinritreis, vrijeplaats);
        }

        public Reis Get(int id)
        {
            return reisDAO.Get(id);
        }

        public void Update(Reis entity)
        {
            reisDAO.Update(entity);
        }

        public void Create(Reis entity)
        {
            reisDAO.Create(entity);
        }

        public void Delete(Reis entity)
        {
            reisDAO.Delete(entity);
        }
    }
}
