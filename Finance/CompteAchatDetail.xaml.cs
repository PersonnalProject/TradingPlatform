using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Finance
{
    /// <summary>
    /// Logique d'interaction pour CompteAchatDetail.xaml
    /// </summary>
    public partial class CompteAchatDetail : Window
    {

        private M_Achat compteAchat;
        private MV_Bourse mv_bourse;

        private BoutonResult boutonChoisi;


        public CompteAchatDetail(M_Achat achat,MV_Bourse mv_bourse)
        {
            InitializeComponent();
            this.mv_bourse = mv_bourse;
            this.compteAchat = achat;
            this.boutonChoisi = new BoutonResult();
            this.boutonChoisi = BoutonResult.Aucun;

            if (achat.ContientStopLoss)
                StopLossText.Text = achat.StopLoss.ToString();
            if (achat.ContientTakeProfit)
                TakeProfitText.Text = achat.TakeProfit.ToString();
            Quantite.Content = achat.QuantiteAchat;
            PrixAchat.Content = achat.PrixAchat;
            Benefice.DataContext = achat;
            BeneficeTotal.DataContext = achat;
        }

        private void FermerPositionButton_Click(object sender, RoutedEventArgs e)
        {
           
            this.boutonChoisi = BoutonResult.FermerPosition;
            this.Close();
        }

        public BoutonResult BoutonChoisi
        {
            get { return this.boutonChoisi; }
        }


        public String BenefAchat
        {
            get { return compteAchat.BenefAchat.ToString(); }
            set { compteAchat.BenefAchat = Convert.ToInt32(value); }
        }
        public String TotalAchat
        {
            get { return compteAchat.TotalAchat.ToString(); }
            set { compteAchat.TotalAchat = Convert.ToInt32(value); }
        }

        private void EnregistrerPositionButton_Click(object sender, RoutedEventArgs e)
        {
            String textTakeProfit = TakeProfitText.Text;
            String textStopLoss = StopLossText.Text;


            double takeProfit;
            bool convertionTakeProfit = Double.TryParse(textTakeProfit, out takeProfit);
            double stopLoss;
            bool convertionStopLoss = Double.TryParse(textStopLoss, out stopLoss);
            if(textTakeProfit !="")
            {
                if (convertionTakeProfit)
                {
                    compteAchat.ContientTakeProfit = true;
                    compteAchat.TakeProfit = takeProfit;
                }
                else
                    MessageBox.Show("Take profit doit être flottant, utiliser la virgule et non le point");

                if (textStopLoss != "")
                {
                    if (convertionStopLoss)
                    {
                        compteAchat.ContientStopLoss = true;
                        compteAchat.StopLoss = stopLoss;
                    }
                    else
                        MessageBox.Show("Stop Loss doit être flottant, utiliser la virgule et non le point");
                }
            }
            if (textStopLoss != "")
            {
                if (convertionStopLoss)
                {
                    compteAchat.ContientStopLoss = true;
                    compteAchat.StopLoss = stopLoss;
                }
                else
                    MessageBox.Show("Stop Loss doit être flottant, utiliser la virgule et non le point");

                if (textTakeProfit != "")
                {
                    if (convertionTakeProfit)
                    {
                        compteAchat.ContientTakeProfit = true;
                        compteAchat.TakeProfit = takeProfit;
                    }
                    else
                        MessageBox.Show("Take profit doit être flottant, utiliser la virgule et non le point");
                }
            }

            this.Close();
        }
    }
}
