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
    /// Interaction logic for ElemBase.xaml
    /// </summary>
    public class ElemBase : UserControl
    {
        protected TSBase _task;

        public ElemBase()
        {
            Console.WriteLine("CONSTRUCTOR CALL!");
        }
        public TaskSession Session
        {
            get
            {
                return (TaskSession)GetValue(TaskSessionProperty);
            }
            set
            {
                SetValue(TaskSessionProperty, value);
            }
        }

        public TSBase Task
        {
            get { return _task; }
            set { _task = value; }
        }

        public static readonly DependencyProperty TaskSessionProperty =
            DependencyProperty.Register(
                "Session",
                typeof(TaskSession),
                typeof(ElemBase),
                new PropertyMetadata(default(TaskSession), OnItemsPropertyChanged));
        private static void OnItemsPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is ElemStart)
            {
                (d as ElemStart).InitializeTask();
            }
        }
       
    }
}
