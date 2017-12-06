using System;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using LiveCharts;
using LiveCharts.Wpf;
using System.ComponentModel;
using LiveCharts.Defaults;
using System.Collections.Generic;
using System.Timers;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;
using System.Windows.Controls;
using System.Data;
//using System.Threading;

namespace Finance
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    ///
     
    // VALEUR 50 POINTS MAX
    public partial class MainWindow : Window
    {
        private int periode; // Pour le graphe et l'historique Bourse
       // private double periodeAchat; // pour l'historique d'achat : NE DOIT PAS ETRE SUPERIEUR A periode !!!!

        private MV_Bourse mv_Bourse;
        private MV_HistoriqueCours mv_HistoriqueCours;
        private MV_Achat mv_Achat;
        private int id;

        public MainWindow()
        {
            id = 0;

            //periodeAchat = 0.5;
            periode = 1; // seconde

            InitializeComponent();
            #region Initialisation MV
            mv_Bourse = new MV_Bourse(periode);
            mv_HistoriqueCours = new MV_HistoriqueCours(periode,mv_Bourse.GetM_Bourse(), ItemHistorique,mv_Bourse.MyTimer);
            mv_Achat = new MV_Achat(periode, mv_Bourse.GetM_Bourse(),CompteAchat, mv_Bourse.MyTimer, ResultatCompte);
            #endregion

            #region Graphe Paramaters
            graphe.DisableAnimations = true;
            graphe.ScrollBarFill = Brushes.AntiqueWhite;
            this.graphe.Series = mv_Bourse.PointsSerie;
           
            #endregion
        }

        private void boutonAchat_Click(object sender, RoutedEventArgs e)
        {
            id++;
            M_Bourse bourse = mv_Bourse.GetM_Bourse();
            double benef = 0;
            int quantite = Convert.ToInt32(Quantite.Value);
            double total = 0;
            M_Achat temp = new M_Achat(Transaction.Achat,id, DateTime.Now.ToString("hh:mm:ss"), quantite,Convert.ToDouble(bourse.LastPrice()), benef,total);
            mv_Achat.AjoutAchat(temp);
        }

        private void boutonVente_Click(object sender, RoutedEventArgs e)
        {
            id++;
            M_Bourse bourse = mv_Bourse.GetM_Bourse();
            double benef = 0;
            int quantite = Convert.ToInt32(Quantite.Value);
            double total = 0;
            M_Achat temp = new M_Achat(Transaction.Vente, id, DateTime.Now.ToString("hh:mm:ss"), quantite, Convert.ToDouble(bourse.LastPrice()), benef, total);
            mv_Achat.AjoutAchat(temp);
           
        }

        private void CompteAchat_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            M_Achat temp =(M_Achat) CompteAchat.SelectedItems[0];
            int placementListe = CompteAchat.SelectedIndex;
            

            CompteAchatDetail fenetreCompteDetail = new CompteAchatDetail(temp ,mv_Bourse);
            fenetreCompteDetail.ShowDialog();
            if(fenetreCompteDetail.BoutonChoisi == BoutonResult.FermerPosition)
            {
                ResultatCompte.Benefice =( Convert.ToInt32(ResultatCompte.Benefice) + temp.BenefAchat ).ToString();
                ResultatCompte.Solde = (Convert.ToInt32(ResultatCompte.Solde) + temp.BenefAchat).ToString();
                mv_Achat.ListeAchat.RemoveAt(placementListe);
            }
        }

        
    }

        

        
    
}
