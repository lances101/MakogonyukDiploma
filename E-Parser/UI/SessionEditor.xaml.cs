using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
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
        private TaskSession _session;
        private List<ElemBase> sessionElements = new List<ElemBase>();

        public List<ElemBase> SessionElements
        {
            get { return sessionElements; }
            set { sessionElements = value; }
        }

        public void LoadSession()
        {
            BinaryFormatter bf = new BinaryFormatter();
            using (FileStream fs = new FileStream("./lastSession.psf", FileMode.Open))
            {
                CurrentSession = (TaskSession) bf.Deserialize(fs);
            }
            SessionElements.Clear();
            foreach (var e in CurrentSession.TaskList)
            {
                RestoreTask(e);
            }

        }
        public void SaveSession()
        {
            BinaryFormatter bf = new BinaryFormatter();
            using (FileStream fs = new FileStream("./lastSession.psf", FileMode.Create))
            {
                bf.Serialize(fs, _session);
            }
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
            if (SessionElements.Count > 0)
            {
                if (be is ElemStart) return;
                if (SessionElements.Last().TryAddNewElement(be))
                {
                    CurrentSession.TaskList.Add(be.Task);
                    SessionElements.Add(be);
                    SessionViewer.Items.Refresh();
                }
            }
            else
            {
                if (be is ElemStart)
                {
                    CurrentSession.TaskList.Add(be.Task);
                    SessionElements.Add(be);
                    SessionViewer.Items.Refresh();
                }
            }
        }

        private void RestoreTask(TSBase task)
        {
            ElemBase be = Activator.CreateInstance(task.ElemType, task.Session) as ElemBase;
            if (SessionElements.Count == 0)
            {
                SessionElements.Add(be);
            }
            else if (SessionElements.Last().TryAddNewElement(be))
            {
                SessionElements.Add(be);
            }
            SessionViewer.Items.Refresh();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AddNewTask(typeof(ElemStart));
            
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            AddNewTask(typeof(ElemTextInput));
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            AddNewTask(typeof(ElemLoadURL));
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            CurrentSession.StartSession();
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            AddNewTask(typeof(ElemEnd));
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            SaveSession();
        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            LoadSession();
        }

       
    }
}
