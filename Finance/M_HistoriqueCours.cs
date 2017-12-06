using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finance
{
    class M_HistoriqueCours
    {
        private String dateHistorique;
        private String priceHistorique;
    


        public M_HistoriqueCours(String date, String prix)
        {
            this.dateHistorique = date;
            this.priceHistorique = prix;
        }

        public String DateHistorique
        {
            get { return dateHistorique; }
            set { dateHistorique = value; }
        }

        public String PriceHistorique
        {
            get { return priceHistorique; }
            set { priceHistorique = value; }
        }





    }
}
