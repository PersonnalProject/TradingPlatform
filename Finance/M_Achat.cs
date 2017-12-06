using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finance
{
    public class M_Achat : INotifyPropertyChanged
    {

        private String dateAchat;
        private int quantiteAchat;
        private double prixAchat;
        private double benefAchat;
        private double totalAchat;
        private double stopLoss;
        private double takeProfit;
        private bool contientStopLoss;
        private bool contientTakeProfit;
        private Transaction type;
        private String typeText;
        private int id;

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(String name)
        {
            if(PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        public M_Achat(Transaction type, int id,String date, int quantite, double prix, double benef, double tot)
        {
            this.type = type;
            this.typeText = this.type.ToString();
            this.id = 0;
            this.dateAchat = date;
            this.quantiteAchat = quantite;
            this.prixAchat = prix;
            this.benefAchat = benef;
            this.totalAchat = tot;
            this.contientStopLoss = false;
            this.contientTakeProfit = false;
        }

        public bool ContientStopLoss
        {
            get { return contientStopLoss; }
            set { contientStopLoss = value; }
        }

        public bool ContientTakeProfit
        {
            get { return contientTakeProfit; }
            set { contientTakeProfit = value; }
        }

        public double TakeProfit
        {
            get { return takeProfit; }
            set { takeProfit = value; }
        }
        public double StopLoss
        {
            get { return stopLoss; }
            set { stopLoss = value; }
        }
        public String TypeAchat
        {
            get { return typeText; }
            set { typeText = value; }
        }

        public Transaction Type
        {
            get { return type; }
        }

        public double TotalAchat
        {
            get { return totalAchat; }
            set
            {
                if(totalAchat != value)
                {
                    totalAchat = value;
                    OnPropertyChanged("TotalAchat");
                }        
            }
        }

        public String DateAchat
        {
            get { return dateAchat; }
            set { dateAchat = value; }
        }
        public int QuantiteAchat
        {
            get { return quantiteAchat; }
            set { quantiteAchat = value; }
        }
        public double PrixAchat
        {
            get { return prixAchat; }
            set { prixAchat = value; }
        }
        public double BenefAchat
        {
            get { return benefAchat; }
            set
            {
                if( benefAchat != value)
                {
                    benefAchat = value;
                    OnPropertyChanged("BenefAchat");
                }
            }
        }

    }
}
