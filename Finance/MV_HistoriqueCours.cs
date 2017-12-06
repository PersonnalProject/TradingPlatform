using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Threading;

namespace Finance
{
    class MV_HistoriqueCours //: INotifyCollectionChanged, IEnumerable
    {
        private List<M_HistoriqueCours> m_HistoriqueCours;
        private M_Bourse m_Bourse;
        private DispatcherTimer myTimer;
        private ListView _dataGridHistorique;
        private int periodicite;

        

        public MV_HistoriqueCours(int periode,M_Bourse m_Bourse, ListView dataGridHistorique, DispatcherTimer myTimer)
        {
            this.periodicite = periode;
            this.m_Bourse = m_Bourse;
            this.m_HistoriqueCours = new List<M_HistoriqueCours>();
            this._dataGridHistorique = dataGridHistorique;
            this.myTimer = myTimer;
            this.myTimer.Tick += this.AjoutHistorique;
          //  InitialisationPeriodique();
        }


        /*public void InitialisationPeriodique()
        {
            this.myTimer = new DispatcherTimer();
            myTimer.Interval = TimeSpan.FromSeconds(periodicite);           
            myTimer.Tick += this.AjoutHistorique;                  
            myTimer.Start();
        }*/



        public List<M_HistoriqueCours> HistoriqueCours
        {
            get { return m_HistoriqueCours; }
            set { m_HistoriqueCours = value; }
        }

        public void AjoutHistorique(object sender, EventArgs e)
        {

            M_HistoriqueCours temp = new M_HistoriqueCours(m_Bourse.LastDate(), m_Bourse.LastPrice());
            m_HistoriqueCours.Add(temp);
            _dataGridHistorique.ClearValue(ListView.ItemsSourceProperty);
            _dataGridHistorique.ItemsSource = m_HistoriqueCours;
            _dataGridHistorique.ScrollIntoView(_dataGridHistorique.Items[_dataGridHistorique.Items.Count - 1]);
        }

      
    }
}
