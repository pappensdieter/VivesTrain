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

        public (Reis reis, ICollection<TreinritReis>, Boolean vrijeplaats) MakeReis(String naam, Boolean klasse, Stad vertrekstad, Stad aankomststad, DateTime date, int aantalZitp)
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
                Vertrekstad = vertrekstad,
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

                //Zoek de laatst aangemaakte treinritreis van treinrit om de plaats te vinden
                TreinritReis foundTreinritReis = treinritReisService.FindTreinritReis(treinrit);
                if (foundTreinritReis != null)
                {   
                    if(foundTreinritReis.Plaats + aantalZitp <= treinrit.AtlZitplaatsen)
                    {
                        for(int i = 0; i < aantalZitp; i++)
                        {
                            //Treinritreis aanmaken voor elke plaats
                            TreinritReis treinritreis = new TreinritReis
                            {
                                Treinrit = treinrit,
                                Klasse = klasse,
                                Reis = reis,
                                ReisId = reis.Id,
                                Plaats = foundTreinritReis.Plaats + i
                            };
                            colTreinritreis.Add(treinritreis);
                        }
                    }
                    else
                    {
                        vrijeplaats = false;
                    }
                }
                else
                {
                    for(int i = 0; i < aantalZitp; i++)
                    {
                        TreinritReis treinritreis = new TreinritReis
                        {
                            Treinrit = treinrit,
                            Klasse = klasse,
                            Reis = reis,
                            Plaats = i + 1
                        };
                        colTreinritreis.Add(treinritreis);
                    }
                    
                }
            }

            reis.TreinritReis = colTreinritreis;

            reis.Prijs = reis.Prijs * aantalZitp;

            //Create(reis);

            return (reis, colTreinritreis, vrijeplaats);
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
