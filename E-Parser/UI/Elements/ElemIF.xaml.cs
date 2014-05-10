using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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
using E_Parser.UI.Elements;

namespace E_Parser.UI.Elements
{

    public partial class ElemIF : ElemBase
    {
        public IfCompareType CurrentCompareType { get; set ;}
        public ElemIF(TaskSession currentSession)
        {
            InitializeComponent();
            Task = new TSIf(currentSession);
        }

        public ElemIF()
        {
            InitializeComponent();
        }

        private void cmbbxIfType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

    }
}
