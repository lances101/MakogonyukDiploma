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

namespace E_Parser.Elements
{
    /// <summary>
    /// Interaction logic for BaseElement.xaml
    /// </summary>
    public partial class BaseAction : UserControl
    {
        public enum eAcceptedTypes
        {
            None, String, Integer, NodeList, Node
        }
        public enum eReturnsTypes
        {
            None, String, Integer, NodeList, Node
        }
        public BaseAction()
        {
            InitializeComponent();
            System.Windows.Threading.DispatcherTimer runningTimer = new System.Windows.Threading.DispatcherTimer();
            runningTimer.Tick += isRunningTimer_Tick;
            runningTimer.Interval = new TimeSpan(0, 0, 0, 0, 500);
            runningTimer.Start();
        }
        public readonly eAcceptedTypes AcceptedTypes = eAcceptedTypes.None;
        public readonly eReturnsTypes ReturnedTypes = eReturnsTypes.None;
        
        
        protected virtual object MainTaskMethod(params object[] args)
        {
            return null;
        }
        public object ExecuteTask(params object[] args)
        {
            isRunning = true;
            var task = Task.Factory.StartNew(() => 
            {
                return MainTaskMethod(args);
            });
            isRunning = false;
            return task.Result;
        }
        private bool isRunning, isRunningToggler = false;
        void isRunningTimer_Tick(object sender, EventArgs e)
        {
            if (isRunning)
            {
                rbtnRunning.IsChecked = isRunningToggler;
                isRunningToggler = !isRunningToggler;
            }
            else
                rbtnRunning.IsChecked = false;
        }

    }
}
