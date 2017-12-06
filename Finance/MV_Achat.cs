using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Threading;

namespace Finance
{
    class MV_Achat : INotifyPropertyChanged
    {
        private ListView compteAchat;
        private List<M_Achat> listeAchat;
        private M_Bourse m_Bourse;
        private DispatcherTimer myTimer;
        private ResultatCompte compte;
        
        private double periodicite;

        private double benefPerte;

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(String name)
        {
            if(PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        public MV_Achat(double periode,M_Bourse mbourse, ListView compteAchat, DispatcherTimer myTimer, ResultatCompte compte)
        {
            this.compteAchat = compteAchat;
            this.periodicite = periode;
            this.m_Bourse = mbourse;
            this.compte = compte;
            this.listeAchat = new List<M_Achat>();
            this.myTimer = myTimer;
            this.myTimer.Tick += this.MiseAJourAchat;
            //InitialisationPeriodique();
        }


        /*private void InitialisationPeriodique()
        {
            this.myTimer = new DispatcherTimer();
            myTimer.Interval = TimeSpan.FromSeconds(periodicite);
            myTimer.Tick += this.MiseAJourAchat;
            myTimer.Start();
        }*/


        public void MiseAJourAchat(object sender, EventArgs e)
        {
            MiseAJourBenef();
            MiseAJourProfit();
            compteAchat.ClearValue(ListView.ItemsSourceProperty);
            compteAchat.ItemsSource = listeAchat;
        }



        private void MiseAJourBenef()
        {
            if(listeAchat.Count != 0)
            {
                for(int i =0; i<listeAchat.Count;i++) 
                {
                    double dernier = Convert.ToDouble(m_Bourse.LastPrice());
                    double prixAchat = listeAchat[i].PrixAchat;
                    
                    if (listeAchat[i].Type == Transaction.Achat) // ACHAT
                    {
                        benefPerte = dernier - prixAchat;
                       
                        
                    }
                    else    // VENTE
                    {
                        benefPerte = prixAchat - dernier;
                    }
                    listeAchat[i].BenefAchat = benefPerte;
                    listeAchat[i].TotalAchat = benefPerte * listeAchat[i].QuantiteAchat;
                    this.BenefPerte = benefPerte;
                } // Mise a jour pour les gains et gains total
            }
        }


        private void MiseAJourProfit() 
        {
            for(int i= 0; i<listeAchat.Count; i++)
            {
                double dernierPrixBourse = Convert.ToDouble(m_Bourse.LastPrice()); //Le comparant
                M_Achat achat = listeAchat[i];
                bool dejaSupprimer = false;
                if (achat.Type == Transaction.Achat) // Si c'est un achat
                {
                    if(achat.ContientTakeProfit) // Si on a une position de TakeProfit
                    {
                        if(dernierPrixBourse > achat.TakeProfit) // Si notre dernierPrixBourse dépasse le TakeProfit
                        {
                            //On enregistre nos gains et on le supprime des achats !
                            compte.Benefice = (Convert.ToInt32(compte.Benefice) + achat.TakeProfit).ToString();
                            compte.Solde = (Convert.ToInt32(compte.Solde) + achat.TakeProfit).ToString();
                            listeAchat.RemoveAt(i);
                            dejaSupprimer = true;
                        }
                    }
                    if(achat.ContientStopLoss)
                    {
                        if(!dejaSupprimer) // Si il a deja ete supprimer pas le peine de rentrer sinon erreur liste 
                        {
                            if (dernierPrixBourse < achat.StopLoss)
                            {
                                //On enregistre nos gains et on le supprime des achats !
                                compte.Benefice = (Convert.ToInt32(compte.Benefice) + achat.StopLoss).ToString();
                                compte.Solde = (Convert.ToInt32(compte.Solde) + achat.StopLoss).ToString();
                                listeAchat.RemoveAt(i);
                            }
                        } 
                    }
                }
                else  //Si c'est pas un achat c'est donc une vente 
                {
                    if(achat.ContientStopLoss)
                    {
                        if(dernierPrixBourse > achat.StopLoss) // Si le cours de la bourse est superieur a notre StopLoss
                        {
                            //On enregistre nos gains et on le supprime des achats !
                            compte.Benefice = (Convert.ToInt32(compte.Benefice) + achat.StopLoss).ToString();
                            compte.Solde = (Convert.ToInt32(compte.Solde) + achat.StopLoss).ToString();
                            listeAchat.RemoveAt(i);
                            dejaSupprimer = true;
                        }
                    }
                    if(achat.ContientTakeProfit)
                    {
                        if(!dejaSupprimer)
                        {
                            if (dernierPrixBourse < achat.TakeProfit)
                            {
                                //On enregistre nos gains et on le supprime des achats !
                                compte.Benefice = (Convert.ToInt32(compte.Benefice) + achat.TakeProfit).ToString();
                                compte.Solde = (Convert.ToInt32(compte.Solde) + achat.TakeProfit).ToString();
                                listeAchat.RemoveAt(i);
                            }
                        } 
                    }
                }
            }
        } // Met fin à un achat si dépassement des positions (StopLoss TakeProfit)

    

        public double BenefPerte
        {
            get { return this.benefPerte; }
            set
            {
                if(benefPerte != value)
                {
                    benefPerte = value;
                    OnPropertyChanged("BenefPerte");
                }
            }
        }






        public void AjoutAchat(M_Achat achat)
        {
            listeAchat.Add(achat);
        }

        public List<M_Achat> ListeAchat
        {
            get { return listeAchat; }
            set { listeAchat = value; }
        }


    }
}
