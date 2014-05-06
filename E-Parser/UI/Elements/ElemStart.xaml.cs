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

namespace E_Parser.Elements
{
    /// <summary>
    /// Interaction logic for BaseElement.xaml
    /// </summary>
    public partial class ElemStart : UserControl
    {
        private TSStart taskSequence;
        public bool IsRunning { get; set; }

        public ElemStart()
        {
           
        }

    }
}
