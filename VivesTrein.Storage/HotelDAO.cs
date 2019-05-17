using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VivesTrein.Domain.Entities;

namespace VivesTrein.Storage
{
    public class HotelDAO
    {
        private readonly vivestrainContext _db;

        public HotelDAO()
        {
            _db = new vivestrainContext();
        }

        public IList<Hotel> GetHotelByStadId(int id)
        {
            return _db.Hotel.Where(x => x.StadId == id).ToList();
        }
    }
}
