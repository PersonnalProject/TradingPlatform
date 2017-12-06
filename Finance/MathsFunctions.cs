using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using STATCONNECTORCLNTLib;
using StatConnectorCommonLib;
using STATCONNECTORSRVLib;
using System.Windows;

namespace Finance
{
    class MathsFunctions
    {

        public MathsFunctions()
        { }

        public void essai()
        {
            object o1;
            int n = 20;
            StatConnector sc1 = new StatConnector();
            sc1.Init("R");
            sc1.SetSymbol("n1", n);
            sc1.Evaluate("x1<-rnorm(n1)");
            o1 = sc1.GetSymbol("x1");
            double Xrnd = (double)o1;
            MessageBox.Show(Xrnd.ToString());
        }
        

    }
}
