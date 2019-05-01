using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using VivesTrein.Domain.Entities;

namespace VivesTrein.Utilities
{
    public static class Seed
    {

        public static async Task CreateTreinritten()
        {
            using (vivestrainContext db = new vivestrainContext())
            {
                var steden = db.Stad.ToList();

                foreach (Stad vertrekstad in steden)
                {
                    foreach (Stad bestemmingsstad in steden)
                    {
                        if (vertrekstad != bestemmingsstad)
                        {
                            var dateNow = DateTime.UtcNow;
                            var depDate = new DateTime(dateNow.Year, dateNow.Month, dateNow.Day, 07, 00, 00);


                            for (int i = 0; i < 5; i++)
                            {
                                (Double reisduur, Boolean day) = FindReisduur(vertrekstad, bestemmingsstad);
                                DateTime arrDate = new DateTime();

                                if(day)
                                {
                                    arrDate = depDate.AddDays(1);
                                    arrDate.AddHours(reisduur);
                                }
                                else
                                {
                                    arrDate = depDate.AddHours(reisduur);
                                }

                                float prijs = 10;
                                Treinrit rit = new Treinrit
                                {
                                    VertrekstadId = vertrekstad.Id,
                                    BestemmingsstadId = bestemmingsstad.Id,
                                    Prijs = prijs,
                                    AtlZitplaatsen = 100,
                                    Vertrek = depDate,
                                    Aankomst = arrDate
                                };

                                await db.AddAsync(rit);

                                depDate = depDate.AddHours(2);
                            }
                        }
                    }
                }
                await db.SaveChangesAsync();
            }
        }

        public static async Task CreateTreinrit()
        {
            using (vivestrainContext db = new vivestrainContext())
            {
                float prijs = 10;
                var steden = db.Stad.ToList();
                Treinrit rit = new Treinrit
                {
                    VertrekstadId = steden.First().Id,
                    BestemmingsstadId = steden[1].Id,
                    Prijs = prijs,
                    AtlZitplaatsen = 100,
                    Vertrek = new DateTime(2019, 04, 29, 23, 29, 00, 00),
                    Aankomst = new DateTime(2019, 04, 29, 23, 30, 00, 00)
                };

                await db.AddAsync(rit);
                await db.SaveChangesAsync();
            }
        }

        public static async Task CreateStad()
        {
            using (vivestrainContext db = new vivestrainContext())
            {
                Stad stad = new Stad
                {
                    Naam = "Banaan"
                };

                await db.AddAsync(stad);
                await db.SaveChangesAsync();
            }
        }

        public static( Double, Boolean) FindReisduur(Stad vertrekstad, Stad bestemmingsstad)
        {
            Double time1u30 = 1.5;
            Double time2u = 2;
            Double time7u = 7;
            Double time15u = 15;
            Double time8u30 = 30;
            Double time19u = 19;
            Double time1d2u = 2;

            Double reisduur = new Double();
            Boolean day = false;
            if (vertrekstad.Naam == "Brussel" || bestemmingsstad.Naam == "Brussel")
            {
                reisduur = time2u;

                if (vertrekstad.Naam == "Parijs" || bestemmingsstad.Naam == "Parijs")
                {
                    reisduur = time1u30;
                }
                else if (vertrekstad.Naam == "Berlijn" || bestemmingsstad.Naam == "Berlijn")
                {
                    reisduur = time7u;
                }
                else if (vertrekstad.Naam == "Rome" || bestemmingsstad.Naam == "Rome")
                {
                    reisduur = time15u;
                }
            }
            else if (vertrekstad.Naam == "Londen" || bestemmingsstad.Naam == "Londen")
            {
                reisduur = time2u;
            }
            else if (vertrekstad.Naam == "Parijs" || bestemmingsstad.Naam == "Parijs")
            {
                if (vertrekstad.Naam == "Berlijn" || bestemmingsstad.Naam == "Berlijn")
                {
                    reisduur = time8u30;

                }
                else if (vertrekstad.Naam == "Rome" || bestemmingsstad.Naam == "Rome")
                {
                    reisduur = time15u;
                }
            }
            else if (vertrekstad.Naam == "Amsterdam" || bestemmingsstad.Naam == "Amsterdam")
            {
                reisduur = time2u;

                if (vertrekstad.Naam == "Berlijn" || bestemmingsstad.Naam == "Berlijn")
                {
                    reisduur = time7u;
                }
            }
            else if (vertrekstad.Naam == "Rome" || bestemmingsstad.Naam == "Rome")
            {
                reisduur = time15u;

                if (vertrekstad.Naam == "Berlijn" || bestemmingsstad.Naam == "Berlijn")
                {
                    reisduur = time19u;
                }
            }
            else if (vertrekstad.Naam == "Berlijn" || bestemmingsstad.Naam == "Berlijn")
            {
                reisduur = time7u;

                if (vertrekstad.Naam == "Moskou" || bestemmingsstad.Naam == "Moskou")
                {
                    reisduur = time1d2u;
                    day = true;
                }
            }
            return (reisduur, day);
        }
    }
}
