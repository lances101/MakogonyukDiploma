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
    public partial class ElemIfEnd : ElemBase
    {
        

        public ElemIfEnd(TaskSession currentSession)
        {
            InitializeComponent();
            Task = new TSIfStart(currentSession);
        }

        public ElemIfEnd()
        {
            InitializeComponent();
        }
        public ElemIfEnd(TSBase ts)
        {
            InitializeComponent();
            Task = ts;
        }

        private void cmbbxIfType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }
    }
}