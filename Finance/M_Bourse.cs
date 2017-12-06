using LiveCharts;
using LiveCharts.Defaults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finance
{
    public class M_Bourse
    {
        private ChartValues<OhlcPoint> listeBourse;
        private List<String> listeDate;
 
        public M_Bourse()
        {
            listeBourse = new ChartValues<OhlcPoint>();
            listeDate = new List<String>();
        }

        public OhlcPoint NouveauPoint()
        {
            var r = new Random();
            bool estNegatif = false;
            estNegatif = r.NextDouble() > 0.5 ? true : false;

            double aOpen = estNegatif ? -1 * r.Next(0, 50) : 1 * r.Next(0, 50);
            double bMax = r.Next(0, 50);
            double cMin = estNegatif ? -1 * r.Next(0, 50) : 1 * r.Next(0, 50); ;
            double dClose = r.Next(0, 50);



            double high = bMax > cMin ? bMax : cMin;
            double less = high == bMax ? cMin : bMax;
            double open = r.Next(Convert.ToInt32(less), Convert.ToInt32(high));
            double close = r.Next(Convert.ToInt32(less), Convert.ToInt32(high));

            OhlcPoint point = new OhlcPoint(open, high, less, close);
            listeBourse.Add(point);
            listeDate.Add(DateTime.Now.ToString("hh:mm:ss"));
            return point;
        }

        public String LastDate()
        {
            return listeDate[listeDate.Count - 1];
        }
        public String LastPrice()
        {
            return listeBourse[listeBourse.Count - 1].Close.ToString();
        }
        public String Last2Price()
        {
            return listeBourse[listeBourse.Count - 2].Close.ToString();
        }

        public OhlcPoint ListeBourse
        {
            get { return listeBourse[listeBourse.Count - 1]; }
        }


    }
}
