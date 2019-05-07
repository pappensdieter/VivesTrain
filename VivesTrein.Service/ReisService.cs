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
using VivesTrein.Service.Interfaces;

namespace VivesTrein.Service
{
    public class ReisService : IService<Reis>
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

        public Reis MakeReis(String naam, Boolean klasse, Stad vertrekstad, Stad aankomststad, DateTime date, int aantalZitp)
        {
            //TODO: depdate mag niet meer dan 14 dagen verder zijn
            //TODO: wat als alle treinen geen plaats hebben voor je reis

            //Tussenstoppen opvragen
            IList <Stad> steden = stadService.GetTussenstoppen(vertrekstad, aankomststad);
            double prijs = 0;

            //Enkele reis
            if(steden.Count == 2)
            {
                Treinrit treinrit = treinritService.GetClosestTreinrit(vertrekstad, aankomststad, date);

                //Controleren of genoeg plaats is anders andere treinrit zoeken
                while (treinrit.Vrijeplaatsen < aantalZitp)
                {
                    //TODO: date verder dan 14 dagen?
                    date = treinrit.Vertrek.AddMinutes(10);
                    treinrit = treinritService.GetClosestTreinrit(vertrekstad, aankomststad, date);
                }
                treinritten.Add(treinrit);
                prijs = treinrit.Prijs;
            }
            //Multireis
            else if(steden.Count > 2)
            {
                DateTime depDate = date;
                for (int i = 0; i < steden.Count - 1; i++)
                {
                    //Treinrit zoeken
                    Treinrit treinrit = treinritService.GetClosestTreinrit(steden[i], steden[i + 1], depDate);

                    //Controleren of genoeg plaats is anders andere treinrit zoeken
                    while(treinrit.Vrijeplaatsen < aantalZitp)
                    {
                        //TODO: date verder dan 14 dagen?
                        depDate = treinrit.Vertrek.AddMinutes(10);
                        treinrit = treinritService.GetClosestTreinrit(steden[i], steden[i + 1], depDate);
                    }

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
                Prijs = prijs,
                Aantal = aantalZitp
            };

            ICollection<TreinritReis> colTreinritreis = new Collection<TreinritReis>();
            foreach(Treinrit treinrit in treinritten)
            {
                treinrit.Vertrekstad = stadService.FindById(treinrit.VertrekstadId);
                treinrit.Bestemmingsstad = stadService.FindById(treinrit.BestemmingsstadId);

                for(int i = 0; i < aantalZitp; i++)
                {
                    //Treinritreis aanmaken voor elke plaats
                    TreinritReis treinritreis = new TreinritReis
                    {
                        Treinrit = treinrit,
                        Klasse = klasse,
                        Reis = reis,
                        ReisId = reis.Id,
                        Plaats = (treinrit.AtlZitplaatsen - treinrit.Vrijeplaatsen) + 1
                    };
                    treinrit.Vrijeplaatsen--;
                    colTreinritreis.Add(treinritreis);
                }
            }

            reis.TreinritReis = colTreinritreis;

            reis.Prijs = reis.Prijs * aantalZitp;

            Create(reis);

            return reis;
        }

        public Reis FindById(int? Id)
        {
            return reisDAO.FindById(Id);
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
