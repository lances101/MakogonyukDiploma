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
using System.Windows.Shapes;
using E_Parser.Logic.ElementLogic;
using E_Parser.UI.Elements;

namespace E_Parser.UI
{
    /// <summary>
    /// Interaction logic for SessionEditor.xaml
    /// </summary>
    public partial class SessionEditor : Window
    {
        private readonly TaskSession _session;
        private List<ElemBase> sessionElements = new List<ElemBase>();

        public List<ElemBase> SessionElements
        {
            get { return sessionElements; }
            set { sessionElements = value; }
        }

        public SessionEditor()
        {
            _session = new TaskSession();
            InitializeComponent();
            AddNewTask(typeof(ElemStart));
        }

        public TaskSession CurrentSession
        {
            get { return _session; }
            set
            {
                
            }
        }

        private void AddNewTask(Type elem)
        {
            ElemBase be = Activator.CreateInstance(elem, CurrentSession) as ElemBase;
            if(SessionElements.Count > 0)
                SessionElements.Last().Task.NextTask = be.Task;
            CurrentSession.TaskList.Add(be.Task);
            SessionElements.Add(be);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AddNewTask(typeof(ElemStart));
            SessionViewer.Items.Refresh();
        }

       
    }
}
