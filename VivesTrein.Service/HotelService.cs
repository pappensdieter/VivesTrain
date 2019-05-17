using System;
using System.Collections.Generic;
using System.Text;
using VivesTrein.Domain.Entities;
using VivesTrein.Storage;

namespace VivesTrein.Service
{
    public class HotelService
    {
        private StadService stadService;
        private HotelDAO hotelDAO;

        public HotelService()
        {
            stadService = new StadService();
            hotelDAO = new HotelDAO();
        }

        public IList<Hotel> GetHotelsByStadName(string stadstring)
        {
            Stad stad = stadService.FindIdByName(stadstring);

            IList<Hotel> hotels = hotelDAO.GetHotelByStadId(stad.Id);

            return hotels;
        }
    }
}
