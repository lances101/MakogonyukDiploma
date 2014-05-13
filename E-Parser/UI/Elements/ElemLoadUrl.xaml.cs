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
using System.Windows.Navigation;
using System.Windows.Shapes;
using E_Parser.Logic.ElementLogic;

namespace E_Parser.UI.Elements
{
    /// <summary>
    /// Interaction logic for ElemLoadURL.xaml
    /// </summary>
    public partial class ElemLoadURL : ElemBase
    {
        public ElemLoadURL()
        {
            
        }
        public ElemLoadURL(TaskSession ts) 
        {
            InitializeComponent();
            Task = new TSLoadUrl(ts);
        }
        public ElemLoadURL(TSBase ts)
        {
            InitializeComponent();
            Task = ts;
        }
    }
}
