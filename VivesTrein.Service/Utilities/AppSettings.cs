using System;
using System.Collections.Generic;
using System.Text;

namespace VivesTrein.Service.Utilities
{
    public class AppSettings
    {
        public static DateTime beginPaasvakantie = new DateTime(2019, 04, 08);
        public static DateTime eindPaasvakantie = new DateTime(2019, 04, 22);

        public static DateTime kerstdag = new DateTime(2019, 12, 25);

        public static Boolean DateInPaasvakantie(DateTime date)
        {
            if(date > beginPaasvakantie && date < eindPaasvakantie)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static Boolean DateMonthBeforeKerst(DateTime date)
        {
            if ((kerstdag - date).TotalDays < 30)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
