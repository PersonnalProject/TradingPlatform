using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Finance
{
    /// <summary>
    /// Logique d'interaction pour ResultatCompte.xaml
    /// </summary>
    public partial class ResultatCompte : UserControl
    {
        public ResultatCompte()
        {
            InitializeComponent();
        }


        [Category("Configuration")]
        public String Solde
        {
            get { return SoldeText.Text; }
            set { SoldeText.Text = value; }
        }

        [Category("Configuration")]
        public String Benefice
        {
            get { return BeneficeText.Text; }
            set { BeneficeText.Text = value; }
        }
    }
}
