using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
using TheE_Parser;

namespace E_Parser.UI.Elements
{
    /// <summary>
    /// Interaction logic for ElemTextInput.xaml
    /// </summary>
    public partial class ElemFindSingleNode : ElemBase
    {
        public ElemFindSingleNode(TaskSession ts)
        {
            InitializeComponent();
            Task = new TSFindSingleNode(ts);
        }
        public ElemFindSingleNode(TSBase ts)
        {
            InitializeComponent();
            Task = ts;
        }

        public ElemFindSingleNode()
        {
            
        }
        
        private void Target_DoubleClick(object sender, MouseButtonEventArgs e)
        {
           
                int ind = Session.GetTaskIndex(Task);
                var prevNode = Session.FunctionalElements.Last(o => o.GetType() == typeof(TSLoadUrl)  && o.Session.GetTaskIndex(o) < ind).PreviousTask;
                EPathExtractor diagWin = new EPathExtractor(prevNode.DirectStringInput);
           
                
            try
            {
                
                if (diagWin.ShowDialog() == true)
                {
                    tbxUrl.Text = diagWin.XPathResult;
                }
            }
            catch (Exception err)
            {
                
            }

        }

    }
}
