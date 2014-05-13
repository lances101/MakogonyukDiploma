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
    /// Interaction logic for ElemDebugMessage.xaml
    /// </summary>
    public partial class ElemDebugMessage : ElemBase
    {
        public ElemDebugMessage(TaskSession ts)
        {
            InitializeComponent();
            Task = new TSDebugMessage(ts);
        }
        public ElemDebugMessage(TSBase ts)
        {
            InitializeComponent();
            Task = ts;
        }

        public ElemDebugMessage()
        {
            
        }
        private void tbxUrl_TextChanged(object sender, TextChangedEventArgs e)
        {       
            //Task.DirectStringInput = tbxUrl.Text;
        }

        public override void NextElementReaction(ElemBase next)
        {
            lblTitle.Content = "Text Input for " + next.Task.GetName;
        }
    }
}
