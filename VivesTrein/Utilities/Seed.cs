using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using VivesTrein.Domain.Entities;
using VivesTrein.Service.Utilities;

namespace VivesTrein.Utilities
{
    public static class Seed
    {

        public static async Task CreateTreinritten()
        {
            using (vivestrainContext db = new vivestrainContext())
            {
                var steden = db.Stad.ToList();
                var dateNow = DateTime.UtcNow;

                for (int i = 0; i < 35; i++)
                {
                    foreach (Stad vertrekstad in steden)
                    {
                        foreach (Stad bestemmingsstad in steden)
                        {
                            if (vertrekstad != bestemmingsstad)
                            {
                                var depDate = new DateTime(dateNow.Year, dateNow.Month, dateNow.Day, 07, 00, 00);


                                for (int j = 0; j < 3; j++)
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
                                        Aankomst = arrDate,
                                        Vrijeplaatsen = 5
                                    };

                                    rit = CheckPeriod(rit);

                                    await db.AddAsync(rit);

                                    depDate = depDate.AddHours(2);
                                }
                            }
                        }
                    }
                    dateNow = dateNow.AddDays(1);

                    await db.SaveChangesAsync();
                }
            }
        }

        public static async Task CreateHotels()
        {
            using (vivestrainContext db = new vivestrainContext())
            {
                Hotel hotel1 = new Hotel()
                {
                    Naam = "The London Penthouse",
                    Adres = "Drake House, Lambeth, London",
                    Link = "https://www.booking.com/hotel/gb/the-london-penthouse.html?aid=397594;label=gog235jc-1DCAEoggI46AdIM1gDaBWIAQGYATG4ARfIAQzYAQPoAQH4AQKIAgGoAgO4AuPC9uYFwAIB;sid=9e663e6157d3f6f37b2a50f2d0b33d01;bhgwe_bhr=0;dest_id=-2601889;dest_type=city;dist=0;group_adults=2;hapos=5;hpos=5;room1=A%2CA;sb_price_type=total;sr_order=popularity;srepoch=1558029116;srpvid=f4967d9e790a01ba;type=total;ucfs=1&#hotelTmpl",
                    StadId = 1
                };

                await db.AddAsync(hotel1);

                Hotel hotel2 = new Hotel()
                {
                    Naam = "London Kings Hotel",
                    Adres = "246 & 254 Edgware Road, Westminster Borough, London",
                    Link = "https://www.booking.com/hotel/gb/london-king-39-s-london3.html?aid=397594;label=gog235jc-1DCAEoggI46AdIM1gDaBWIAQGYATG4ARfIAQzYAQPoAQH4AQKIAgGoAgO4AuPC9uYFwAIB;sid=9e663e6157d3f6f37b2a50f2d0b33d01;dest_id=-2601889;dest_type=city;dist=0;group_adults=2;hapos=6;hpos=6;room1=A%2CA;sb_price_type=total;sr_order=popularity;srepoch=1558029116;srpvid=f4967d9e790a01ba;type=total;ucfs=1&#hotelTmpl",
                    StadId = 1
                };

                await db.AddAsync(hotel2);

                Hotel hotel3 = new Hotel()
                {
                    Naam = "Princes Gardens",
                    Adres = "Imperial College London, Prince's Garden, Watts Way, Westminster Borough, London",
                    Link = "https://www.booking.com/hotel/gb/princes-gardens.html?aid=397594;label=gog235jc-1DCAEoggI46AdIM1gDaBWIAQGYATG4ARfIAQzYAQPoAQH4AQKIAgGoAgO4AuPC9uYFwAIB;sid=9e663e6157d3f6f37b2a50f2d0b33d01;dest_id=-2601889;dest_type=city;dist=0;group_adults=2;hapos=13;hpos=13;room1=A%2CA;sb_price_type=total;sr_order=popularity;srepoch=1558029116;srpvid=f4967d9e790a01ba;type=total;ucfs=1&#hotelTmpl",
                    StadId = 1
                };

                await db.AddAsync(hotel3);

                Hotel hotel4 = new Hotel()
                {
                    Naam = "Motel One Brussels",
                    Adres = "Rue Royale 120, 1000 Brussels, Belgium",
                    Link = "https://www.booking.com/hotel/be/motel-one-brussels.html?aid=397594;label=gog235jc-1DCAEoggI46AdIM1gDaBWIAQGYATG4ARfIAQzYAQPoAQH4AQKIAgGoAgO4AuPC9uYFwAIB;sid=9e663e6157d3f6f37b2a50f2d0b33d01;dest_id=-1955538;dest_type=city;dist=0;hapos=1;hpos=1;room1=A%2CA;sb_price_type=total;sr_order=popularity;srepoch=1558028686;srpvid=78a07cc741fd03d5;type=total;ucfs=1&#hotelTmpl",
                    StadId = 2
                };

                await db.AddAsync(hotel4);

                Hotel hotel5 = new Hotel()
                {
                    Naam = "La Monnaie Residence",
                    Adres = "Rue fossé aux loups 13-21, 1000 Brussels, Belgium",
                    Link = "https://www.booking.com/hotel/be/la-monnaie-residence.html?aid=397594;label=gog235jc-1DCAEoggI46AdIM1gDaBWIAQGYATG4ARfIAQzYAQPoAQH4AQKIAgGoAgO4AuPC9uYFwAIB;sid=9e663e6157d3f6f37b2a50f2d0b33d01;bhgwe_bhr=0;dest_id=-1955538;dest_type=city;dist=0;hapos=3;hpos=3;room1=A%2CA;sb_price_type=total;sr_order=popularity;srepoch=1558028686;srpvid=78a07cc741fd03d5;type=total;ucfs=1&#hotelTmpl",
                    StadId = 2
                };

                await db.AddAsync(hotel5);

                Hotel hotel6 = new Hotel()
                {
                    Naam = "The President",
                    Adres = "Boulevard du Roi Albert II 44, 1000 Brussels, Belgium",
                    Link = "https://www.booking.com/hotel/be/presidentwtc.html?aid=397594;label=gog235jc-1DCAEoggI46AdIM1gDaBWIAQGYATG4ARfIAQzYAQPoAQH4AQKIAgGoAgO4AuPC9uYFwAIB;sid=9e663e6157d3f6f37b2a50f2d0b33d01;dest_id=-1955538;dest_type=city;dist=0;hapos=8;hpos=8;room1=A%2CA;sb_price_type=total;sr_order=popularity;srepoch=1558028686;srpvid=78a07cc741fd03d5;type=total;ucfs=1&#hotelTmpl",
                    StadId = 2
                };

                await db.AddAsync(hotel6);

                Hotel hotel7 = new Hotel()
                {
                    Naam = "Hôtel Baby",
                    Adres = "1 Rue Chenier, 2nd arr., 75002 Paris",
                    Link = "https://www.booking.com/hotel/fr/hotel-chenier.html?aid=397594;label=gog235jc-1DCAEoggI46AdIM1gDaBWIAQGYATG4ARfIAQzYAQPoAQH4AQKIAgGoAgO4AuPC9uYFwAIB;sid=9e663e6157d3f6f37b2a50f2d0b33d01;dest_id=-1456928;dest_type=city;dist=0;group_adults=2;hapos=2;hpos=2;nflt=ht_id%3D204%3B;room1=A%2CA;sb_price_type=total;sr_order=popularity;srepoch=1558029340;srpvid=a53e7e0dfb0301fc;type=total;ucfs=1&#hotelTmpl",
                    StadId = 3
                };

                await db.AddAsync(hotel7);

                Hotel hotel8 = new Hotel()
                {
                    Naam = "Hôtel Dress Code & Spa",
                    Adres = "5 Rue de Caumartin, 9th arr., 75008 Paris",
                    Link = "https://www.booking.com/hotel/fr/dress-code-amp-spa.html?aid=397594;label=gog235jc-1DCAEoggI46AdIM1gDaBWIAQGYATG4ARfIAQzYAQPoAQH4AQKIAgGoAgO4AuPC9uYFwAIB;sid=9e663e6157d3f6f37b2a50f2d0b33d01;dest_id=-1456928;dest_type=city;dist=0;group_adults=2;hapos=3;hpos=3;nflt=ht_id%3D204%3B;room1=A%2CA;sb_price_type=total;sr_order=popularity;srepoch=1558029340;srpvid=a53e7e0dfb0301fc;type=total;ucfs=1&#hotelTmpl",
                    StadId = 3
                };

                await db.AddAsync(hotel8);

                Hotel hotel9 = new Hotel()
                {
                    Naam = "Hotel London",
                    Adres = "32 Boulevard des Italiens, 9th arr., 75009 Paris",
                    Link = "https://www.booking.com/hotel/fr/london.html?aid=397594;label=gog235jc-1DCAEoggI46AdIM1gDaBWIAQGYATG4ARfIAQzYAQPoAQH4AQKIAgGoAgO4AuPC9uYFwAIB;sid=9e663e6157d3f6f37b2a50f2d0b33d01;dest_id=-1456928;dest_type=city;dist=0;group_adults=2;hapos=5;hpos=5;nflt=ht_id%3D204%3B;room1=A%2CA;sb_price_type=total;sr_order=popularity;srepoch=1558029340;srpvid=a53e7e0dfb0301fc;type=total;ucfs=1&#hotelTmpl",
                    StadId = 3
                };

                await db.AddAsync(hotel9);

                Hotel hotel10 = new Hotel()
                {
                    Naam = "Mövenpick Hotel Amsterdam",
                    Adres = "Piet Heinkade 11, Zeeburg, 1019 BR Amsterdam",
                    Link = "https://www.booking.com/hotel/nl/moevenpick-amsterdam-city-centre.html?aid=397594;label=gog235jc-1DCAEoggI46AdIM1gDaBWIAQGYATG4ARfIAQzYAQPoAQH4AQKIAgGoAgO4AuPC9uYFwAIB;sid=9e663e6157d3f6f37b2a50f2d0b33d01;dest_id=-2140479;dest_type=city;dist=0;group_adults=2;hapos=1;hpos=1;nflt=ht_id%3D204%3B;room1=A%2CA;sb_price_type=total;sr_order=popularity;srepoch=1558029585;srpvid=90617e88b77a0009;type=total;ucfs=1&#hotelTmpl",
                    StadId = 4
                };

                await db.AddAsync(hotel10);

                Hotel hotel11 = new Hotel()
                {
                    Naam = "Amsterdam Wiechmann Hotel",
                    Adres = "Prinsengracht 328 - 332, Amsterdam City Center, 1016 HX Amsterdam, Netherlands",
                    Link = "https://www.booking.com/hotel/nl/amsterdam-wiechmann.html?aid=397594;label=gog235jc-1DCAEoggI46AdIM1gDaBWIAQGYATG4ARfIAQzYAQPoAQH4AQKIAgGoAgO4AuPC9uYFwAIB;sid=9e663e6157d3f6f37b2a50f2d0b33d01;dest_id=-2140479;dest_type=city;dist=0;group_adults=2;hapos=3;hpos=3;nflt=ht_id%3D204%3B;room1=A%2CA;sb_price_type=total;sr_order=popularity;srepoch=1558029585;srpvid=90617e88b77a0009;type=total;ucfs=1&#hotelTmpl",
                    StadId = 4
                };

                await db.AddAsync(hotel11);

                Hotel hotel12 = new Hotel()
                {
                    Naam = "Sir Adam Hotel",
                    Adres = "Overhoeksplein 7, Amsterdam Noord, 1031 KS Amsterdam",
                    Link = "https://www.booking.com/hotel/nl/sir-adam.html?aid=397594;label=gog235jc-1DCAEoggI46AdIM1gDaBWIAQGYATG4ARfIAQzYAQPoAQH4AQKIAgGoAgO4AuPC9uYFwAIB;sid=9e663e6157d3f6f37b2a50f2d0b33d01;dest_id=-2140479;dest_type=city;dist=0;group_adults=2;hapos=4;hpos=4;nflt=ht_id%3D204%3B;room1=A%2CA;sb_price_type=total;sr_order=popularity;srepoch=1558029585;srpvid=90617e88b77a0009;type=total;ucfs=1&#hotelTmpl",
                    StadId = 4
                };

                await db.AddAsync(hotel12);

                Hotel hotel13 = new Hotel()
                {
                    Naam = "Le Meridien Visconti Rome",
                    Adres = "Via Federico Cesi 37, Vaticano Prati	, 00193 Rome",
                    Link = "https://www.booking.com/hotel/it/visconti-palace.html?aid=397594;label=gog235jc-1DCAEoggI46AdIM1gDaBWIAQGYATG4ARfIAQzYAQPoAQH4AQKIAgGoAgO4AuPC9uYFwAIB;sid=9e663e6157d3f6f37b2a50f2d0b33d01;dest_id=2282;dest_type=district;dist=0;group_adults=2;hapos=1;hpos=1;nflt=ht_id%3D204%3B;room1=A%2CA;sb_price_type=total;sr_order=popularity;srepoch=1558029800;srpvid=48f87ef3fcf8019b;type=total;ucfs=1&#hotelTmpl",
                    StadId = 5
                };

                await db.AddAsync(hotel13);

                Hotel hotel14 = new Hotel()
                {
                    Naam = "Hotel Raphael",
                    Adres = "argo Febo 2, Navona, 00186 Rome",
                    Link = "https://www.booking.com/hotel/it/raphael.html?aid=397594;label=gog235jc-1DCAEoggI46AdIM1gDaBWIAQGYATG4ARfIAQzYAQPoAQH4AQKIAgGoAgO4AuPC9uYFwAIB;sid=9e663e6157d3f6f37b2a50f2d0b33d01;dest_id=2282;dest_type=district;dist=0;group_adults=2;hapos=2;hpos=2;nflt=ht_id%3D204%3B;room1=A%2CA;sb_price_type=total;sr_order=popularity;srepoch=1558029800;srpvid=48f87ef3fcf8019b;type=total;ucfs=1&#hotelTmpl",
                    StadId = 5
                };

                await db.AddAsync(hotel14);

                Hotel hotel15 = new Hotel()
                {
                    Naam = "Hotel Ariston",
                    Adres = "Via Filippo Turati 16, Central Station, 00185 Rome",
                    Link = "https://www.booking.com/hotel/it/ariston-roma.html?aid=397594;label=gog235jc-1DCAEoggI46AdIM1gDaBWIAQGYATG4ARfIAQzYAQPoAQH4AQKIAgGoAgO4AuPC9uYFwAIB;sid=9e663e6157d3f6f37b2a50f2d0b33d01;dest_id=2282;dest_type=district;dist=0;group_adults=2;hapos=5;hpos=5;nflt=ht_id%3D204%3B;room1=A%2CA;sb_price_type=total;sr_order=popularity;srepoch=1558029800;srpvid=48f87ef3fcf8019b;type=total;ucfs=1&#hotelTmpl",
                    StadId = 5
                };

                await db.AddAsync(hotel15);

                Hotel hotel16 = new Hotel()
                {
                    Naam = "Hotel 38",
                    Adres = "Oranienburger Str. 38, Mitte, 10117 Berlin",
                    Link = "https://www.booking.com/hotel/de/am-scheunenviertel-garni.html?aid=397594;label=gog235jc-1DCAEoggI46AdIM1gDaBWIAQGYATG4ARfIAQzYAQPoAQH4AQKIAgGoAgO4AuPC9uYFwAIB;sid=9e663e6157d3f6f37b2a50f2d0b33d01;dest_id=-1746443;dest_type=city;dist=0;group_adults=2;hapos=10;hpos=10;nflt=ht_id%3D204%3B;room1=A%2CA;sb_price_type=total;sr_order=popularity;srepoch=1558029916;srpvid=2fc27f2deb6c003b;type=total;ucfs=1&#hotelTmpl",
                    StadId = 6
                };

                await db.AddAsync(hotel16);

                Hotel hotel17 = new Hotel()
                {
                    Naam = "Alex Hotel",
                    Adres = "Greifswalder Str. 3, Prenzlauer Berg, 10405 Berlin",
                    Link = "https://www.booking.com/hotel/de/alex.html?aid=397594;label=gog235jc-1DCAEoggI46AdIM1gDaBWIAQGYATG4ARfIAQzYAQPoAQH4AQKIAgGoAgO4AuPC9uYFwAIB;sid=9e663e6157d3f6f37b2a50f2d0b33d01;dest_id=-1746443;dest_type=city;dist=0;group_adults=2;hapos=15;hpos=15;nflt=ht_id%3D204%3B;room1=A%2CA;sb_price_type=total;sr_order=popularity;srepoch=1558029916;srpvid=2fc27f2deb6c003b;type=total;ucfs=1&#hotelTmpl",
                    StadId = 6
                };

                await db.AddAsync(hotel17);

                Hotel hotel18 = new Hotel()
                {
                    Naam = "Hotel AMANO",
                    Adres = "Auguststr. 43/Ecke Rosenthaler Str., Mitte, 10119 Berlin",
                    Link = "https://www.booking.com/hotel/de/amano.html?aid=397594;label=gog235jc-1DCAEoggI46AdIM1gDaBWIAQGYATG4ARfIAQzYAQPoAQH4AQKIAgGoAgO4AuPC9uYFwAIB;sid=9e663e6157d3f6f37b2a50f2d0b33d01;dest_id=-1746443;dest_type=city;dist=0;group_adults=2;hapos=16;hpos=1;nflt=ht_id%3D204%3B;room1=A%2CA;sb_price_type=total;sr_order=popularity;srepoch=1558029938;srpvid=b2a07f386ae90352;type=total;ucfs=1&#hotelTmpl",
                    StadId = 6
                };

                await db.AddAsync(hotel18);

                Hotel hotel19 = new Hotel()
                {
                    Naam = "Izmailovo Beta Hotel",
                    Adres = "Izmailovskoye Shosse 71 Bld.2B, Izmailovo, 105613 Moscow",
                    Link = "https://www.booking.com/hotel/ru/izmaylovo-beta.html?aid=397594;label=gog235jc-1DCAEoggI46AdIM1gDaBWIAQGYATG4ARfIAQzYAQPoAQH4AQKIAgGoAgO4AuPC9uYFwAIB;sid=9e663e6157d3f6f37b2a50f2d0b33d01;dest_id=-2960561;dest_type=city;dist=0;group_adults=2;hapos=1;hpos=1;nflt=ht_id%3D204%3B;room1=A%2CA;sb_price_type=total;sr_order=popularity;srepoch=1558030068;srpvid=48f87f7902220217;type=total;ucfs=1&#hotelTmpl",
                    StadId = 7
                };

                await db.AddAsync(hotel19);

                Hotel hotel20 = new Hotel()
                {
                    Naam = "Milan Hotel",
                    Adres = "Shipilovskaya Str., 28А, 155563 Moscow",
                    Link = "https://www.booking.com/hotel/ru/milan-moscow.html?aid=397594;label=gog235jc-1DCAEoggI46AdIM1gDaBWIAQGYATG4ARfIAQzYAQPoAQH4AQKIAgGoAgO4AuPC9uYFwAIB;sid=9e663e6157d3f6f37b2a50f2d0b33d01;dest_id=-2960561;dest_type=city;dist=0;group_adults=2;hapos=5;hpos=5;nflt=ht_id%3D204%3B;room1=A%2CA;sb_price_type=total;sr_order=popularity;srepoch=1558030068;srpvid=48f87f7902220217;type=total;ucfs=1&#hotelTmpl",
                    StadId = 7
                };

                await db.AddAsync(hotel20);

                Hotel hotel21 = new Hotel()
                {
                    Naam = "Brestol Hotel",
                    Adres = "Strastnoy Bulvar 7 stroenie 2, Tverskoy, 127006 Moscow",
                    Link = "https://www.booking.com/hotel/ru/brestol-moskva.html?aid=397594;label=gog235jc-1DCAEoggI46AdIM1gDaBWIAQGYATG4ARfIAQzYAQPoAQH4AQKIAgGoAgO4AuPC9uYFwAIB;sid=9e663e6157d3f6f37b2a50f2d0b33d01;dest_id=-2960561;dest_type=city;dist=0;group_adults=2;hapos=6;hpos=6;nflt=ht_id%3D204%3B;room1=A%2CA;sb_price_type=total;sr_order=popularity;srepoch=1558030068;srpvid=48f87f7902220217;type=total;ucfs=1&#hotelTmpl",
                    StadId = 7
                };

                await db.AddAsync(hotel21);

                await db.SaveChangesAsync();
            }
        }

        private static Treinrit CheckPeriod(Treinrit rit)
        {
            if (AppSettings.DateInPaasvakantie(rit.Vertrek))
            {
                if (rit.BestemmingsstadId == 1 || rit.BestemmingsstadId == 2 || rit.BestemmingsstadId == 4)
                {
                    rit.AtlZitplaatsen = 130;
                    rit.Vrijeplaatsen = 130;
                }
            }
            if (AppSettings.DateMonthBeforeKerst(rit.Vertrek))
            {
                if (rit.BestemmingsstadId == 1 || rit.BestemmingsstadId == 1)
                {
                    rit.AtlZitplaatsen = 130;
                    rit.Vrijeplaatsen = 130;
                }
            }

            return rit;
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
