using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiveCharts;
using LiveCharts.Wpf;
using LiveCharts.Defaults;
using System.Windows.Media;
using System.Collections.Specialized;
using System.Collections;
using System.Windows.Threading;
using System.Windows;

namespace Finance
{
    public class MV_Bourse : INotifyCollectionChanged , IEnumerable
    {
        private M_Bourse m_Bourse;
        private DispatcherTimer myTimer;
        private MV_HistoriqueCours mv_HistoriqueCours;
        private MathsFunctions math;
       // private CartesianChart graphique;

        private int nombrePointMax;

        private int periodicite;
        
        public MV_Bourse(int periode)
        {
            this.m_Bourse = new M_Bourse();
            this.nombrePointMax = 50;
            this.periodicite = periode;
            // this.graphique = graphique;
            this.math = new MathsFunctions();
           

            PointsSerie = new SeriesCollection
            {

              new OhlcSeries()
              {
                  DecreaseBrush = Brushes.Red,
                  IncreaseBrush = Brushes.Green,


                    Values = new ChartValues<OhlcPoint>
                    {
                    }
              },
              new LineSeries()
                {
                    Values = new ChartValues<double> {},
                    Fill = Brushes.Transparent,
                    PointForeround = Brushes.Black,
                },
            };

  

            InitialisationPeriodique();
        }


        private void InitialisationPeriodique()
        {
            myTimer = new DispatcherTimer();
            // Tell the timer what to do when it elapses
            myTimer.Interval = TimeSpan.FromSeconds(periodicite);
            // Set it to go off every five seconds
            myTimer.Tick += this.AjoutPoint;
            // And start it        
            myTimer.Start();
        }


        public MV_Bourse copieMV_Bourse()
        {
            MV_Bourse copie = this;
            return copie;
        }


        public M_Bourse GetM_Bourse()
        {
            return m_Bourse;
        }

        public SeriesCollection PointsSerie { get; set; }

        public event NotifyCollectionChangedEventHandler CollectionChanged;

        public void AjoutPoint(object sender, EventArgs e)
        {
            if (PointsSerie[0].Values.Count > nombrePointMax)
                PointsSerie[0].Values.RemoveAt(0);

            OhlcPoint point = m_Bourse.NouveauPoint();
            PointsSerie[0].Values.Add(point);
            //math.essai();
            this.OnNotifyCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add,PointsSerie));  
        }

       /* public List<OhlcPoint> ListeDesDerniersPoints()
        {
            List<OhlcPoint> liste = new List<OhlcPoint>();
            if(PointsSerie[0].Values.Count > nombrePointMax)
            {
                for (int i = nombrePointMax - 1; i >= 0; i--)
                {
                    liste.Add((LiveCharts.Defaults.OhlcPoint)PointsSerie[0].Values[i]);
                }
            }
            else
            {
                for(int i = PointsSerie[0].Values.Count -1; i>=0;i-- )
                {
                    liste.Add((LiveCharts.Defaults.OhlcPoint)PointsSerie[0].Values[i]);
                }
            }
            
            return liste;
        }*/

        public DispatcherTimer MyTimer
        {
            get { return this.myTimer; }
        }

        public void OnNotifyCollectionChanged(NotifyCollectionChangedEventArgs args)
        {
            if(this.CollectionChanged != null)
            {
                this.CollectionChanged(this, args);
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)PointsSerie).GetEnumerator();
        }
    }
}
