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

        public (Reis, String) MakeReis(String naam, Boolean klasse, Stad vertrekstad, Stad aankomststad, DateTime date, int aantalZitp)
        {
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
                    date = treinrit.Vertrek.AddMinutes(30);
                    if (checkWithin14Days(date))
                    {
                        treinrit = treinritService.GetClosestTreinrit(vertrekstad, aankomststad, date);
                    }
                    else
                    {
                        return (null, "Er zijn geen reizen gevonden voor opgegeven data");
                    }
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
                        depDate = treinrit.Vertrek.AddMinutes(10);
                        if (checkWithin14Days(depDate))
                        {
                            treinrit = treinritService.GetClosestTreinrit(steden[i], steden[i + 1], depDate);
                        }else
                        {
                            return (null, "Er zijn geen reizen gevonden voor opgegeven data");
                        }
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
                Prijs = prijs * aantalZitp,
                Aantal = aantalZitp
            };

            Create(reis);

            //Collection van treinritreizen maken die in reis gaat
            ICollection<TreinritReis> colTreinritreis = new Collection<TreinritReis>();
            foreach(Treinrit treinrit in treinritten)
            {
                for(int i = 0; i < aantalZitp; i++)
                {
                    TreinritReis treinritreis = new TreinritReis
                    {
                        Treinrit = treinrit,
                        Klasse = klasse,
                        Reis = reis,
                        ReisId = reis.Id,
                        Plaats = (treinrit.AtlZitplaatsen - treinrit.Vrijeplaatsen) + 1
                    };

                    var vrijeplaatsen = treinrit.Vrijeplaatsen;
                    treinrit.Vrijeplaatsen = vrijeplaatsen - 1;
                    treinritService.Update(treinrit);

                    colTreinritreis.Add(treinritreis);


                    treinritReisService.Create(treinritreis);
                }
            }

            //Steden toevoegen om ze te tonen op ShowReis pagina
            foreach(TreinritReis treinritreis in colTreinritreis)
            {
                Treinrit treinrit = treinritreis.Treinrit;
                treinrit.Bestemmingsstad = stadService.FindById(treinrit.BestemmingsstadId);
                treinrit.Vertrekstad = stadService.FindById(treinrit.VertrekstadId);
            }

            reis.TreinritReis = colTreinritreis;

            return (reis, "");
        }

        public Boolean checkWithin14Days(DateTime date)
        {
            return ((date - DateTime.Now).TotalDays < 14);
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
