using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VivesTrein.Domain.Entities;
using VivesTrein.Storage;

namespace VivesTrein.Utilities
{
    public class TreinritHelper
    {
        private StadDAO stadDAO;
        private IList<Stad> tussenstops;
        private int atlTussenstops;

        public IEnumerable<Stad> FindRoute(Stad vertrekstad, Stad aankomststad)
        {
            stadDAO = new StadDAO();
            var steden = stadDAO.GetAll();

            switch (vertrekstad.Naam)
            {
                case "Londen":
                    if(aankomststad.Naam == "Brussel")
                    {
                        tussenstops = null;
                    } else if(aankomststad.Naam == "Moskou")
                    {
                        tussenstops.Add(steden.Where(x => x.Naam == "Moskou").FirstOrDefault());
                    } else
                    {
                        tussenstops.Add(steden.Where(x => x.Naam == "Brussel").FirstOrDefault());
                    }
                    break;

                case "Parijs":
                    if(aankomststad.Naam == "Moskou")
                    {
                        tussenstops.Add(steden.Where(x => x.Naam == "Brussel").FirstOrDefault());
                        tussenstops.Add(steden.Where(x => x.Naam == "Berlijn").FirstOrDefault());
                    }else if(aankomststad.Naam == "Londen" || aankomststad.Naam == "Amsterdam")
                    {
                        tussenstops.Add(steden.Where(x => x.Naam == "Brussel").FirstOrDefault());
                    }
                    else
                    {
                        tussenstops = null;
                    }
                    break;

                case "Rome":
                    if(aankomststad.Naam == "Moskou")
                    {
                        tussenstops.Add(steden.Where(x => x.Naam == "Berlijn").FirstOrDefault());
                    }else if(aankomststad.Naam == "Londen"|| aankomststad.Naam == "Amsterdam")
                    {
                        tussenstops.Add(steden.Where(x => x.Naam == "Brussel").FirstOrDefault());
                    }
                    else
                    {
                        tussenstops = null;
                    }
                    break;

                case "Amsterdam":
                    if(aankomststad.Naam == "Moskou")
                    {
                        tussenstops.Add(steden.Where(x => x.Naam == "Berlijn").FirstOrDefault());
                    }else if(aankomststad.Naam == "Brussel" || aankomststad.Naam == "Berlijn")
                    {
                        tussenstops = null;
                    }
                    else
                    {
                        tussenstops.Add(steden.Where(x => x.Naam == "Brussel").FirstOrDefault());
                    }
                    break;

                case "Berlijn":
                    if(aankomststad.Naam == "Parijs"|| aankomststad.Naam == "Londen"||aankomststad.Naam == "Amsterdam")
                    {
                        tussenstops.Add(steden.Where(x => x.Naam == "Brussel").FirstOrDefault());
                    }
                    else
                    {
                        tussenstops = null;
                    }
                    break;

                case "Moskou":
                    if(aankomststad.Naam == "Berlijn")
                    {
                        tussenstops = null;
                    } else if(aankomststad.Naam == "Parijs"|| aankomststad.Naam == "Londen")
                    {
                        tussenstops.Add(steden.Where(x => x.Naam == "Berlijn").FirstOrDefault());
                        tussenstops.Add(steden.Where(x => x.Naam == "Brussel").FirstOrDefault());
                    }
                    else
                    {
                        tussenstops.Add(steden.Where(x => x.Naam == "Berlijn").FirstOrDefault());
                    }
                    break;

                case "Brussel":
                    if(aankomststad.Naam == "Moskou")
                    {
                        tussenstops.Add(steden.Where(x => x.Naam == "Berlijn").FirstOrDefault());
                    }
                    else
                    {
                        tussenstops = null;
                    }
                    break;

                default:
                    //set default
                    break;
            }

            return tussenstops;
        }
    }
}
