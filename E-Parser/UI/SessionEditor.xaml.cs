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
using TheE_Parser;

namespace E_Parser.UI
{
    /// <summary>
    /// Interaction logic for SessionEditor.xaml
    /// </summary>
    public partial class SessionEditor : Window
    {
        private static RichTextBox rtbxLogBox;
        private TaskSession _session;
        private List<ElemBase> _visualElements = new List<ElemBase>();
        public List<ElemBase> VisualElements
        {
            get { return _visualElements; }
            set { _visualElements = value; }
        }

        public static void Log(string message)
        {
            if (rtbxLogBox == null) return;
            try
            {
                rtbxLogBox.AppendText(message + "\n");
            }
            catch (Exception e)
            {
                rtbxLogBox.Dispatcher.Invoke(() => Log(message));
            }

        }

        public static void Log(string format, params object[] args)
        {
            Log(String.Format(format, args));
        }

        public static void Log(object obj)
        {
            Log(obj.ToString());
        }
        public void LoadSession()
        {
            BinaryFormatter bf = new BinaryFormatter();
            using (FileStream fs = new FileStream("./lastSession.psf", FileMode.Open))
            {
                CurrentSession = (TaskSession) bf.Deserialize(fs);
            }
            CurrentSession.VisualElements = VisualElements;
            CurrentSession.Client = new AwesomiumWrap();
            VisualElements.Clear();
            foreach (var e in CurrentSession.FunctionalElements)
            {
                SessionEditor.Log(e.GetName);
                CurrentSession.RestoreTask(e);
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
            _session = new TaskSession(VisualElements);
            InitializeComponent();
            rtbxLogBox = rtbxLog;
        }

        public void TreeViewFiller()
        {
        }

        public TaskSession CurrentSession
        {
            get { return _session; }
            set
            {
                _session = value;
            }
        }

        private void OnTreeItem_Click(object sender, MouseButtonEventArgs args)
        {
            if (sender is TreeViewItem)
            {
                if (ElementTreeView.SelectedItem == null) return;
                switch ((ElementTreeView.SelectedItem as TreeViewItem).Name.ToString())
                {
                    //Debug
                    case "tElemDebugMessage":
                        AddNewTask(SessionViewer.SelectedItem as ElemBase, typeof(ElemDebugMessage));
                        break;
                            
                    //routine specific
                    case "tElemStart":
                        AddNewTask(SessionViewer.SelectedItem as ElemBase, typeof(ElemStart));
                        break;
                    case "tElemEnd":
                        AddNewTask(SessionViewer.SelectedItem as ElemBase, typeof(ElemEnd));
                        break;

                    //variable control
                    case "tElemTextInput":
                        AddNewTask(SessionViewer.SelectedItem as ElemBase, typeof(ElemTextInput));
                        break;
                    case "tElemStoreSingleVariable":
                        AddNewTask(SessionViewer.SelectedItem as ElemBase, typeof(ElemStoreSingleVariable));
                        break;


                    //web control
                    case "tElemLoadURL":
                        AddNewTask(SessionViewer.SelectedItem as ElemBase, typeof (ElemLoadURL));
                        break;


                    //parser control
                    case "tElemFindSingleNode":
                        AddNewTask(SessionViewer.SelectedItem as ElemBase,typeof (ElemFindSingleNode));
                        break;

                }
                SessionViewer.Items.Refresh();
         
            }

        }

        private void AddNewTask(ElemBase origin, Type newElem)
        {
            var res = CurrentSession.AddNewTask(origin, newElem);
            if (res != null) SessionViewer.SelectedItem = res;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            CurrentSession.AddNewTask(SessionViewer.SelectedItem as ElemBase, typeof(ElemStart));
            SessionViewer.Items.Refresh();
            
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            CurrentSession.AddNewTask(SessionViewer.SelectedItem as ElemBase, typeof(ElemTextInput));
            SessionViewer.Items.Refresh();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            CurrentSession.AddNewTask(SessionViewer.SelectedItem as ElemBase, typeof(ElemLoadURL));
            SessionViewer.Items.Refresh();
        }

       

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            CurrentSession.AddNewTask(SessionViewer.SelectedItem as ElemBase, typeof(ElemEnd));
            SessionViewer.Items.Refresh();
        }
        private void StartSession_Click(object sender, RoutedEventArgs e)
        {
            CurrentSession.StartSession();
        }
        private void SaveSession_Click(object sender, RoutedEventArgs e)
        {
            SaveSession();
            SessionViewer.Items.Refresh();
        }

        private void LoadSession_Click(object sender, RoutedEventArgs e)
        {
            LoadSession();
            SessionViewer.Items.Refresh();
        }

       
    }
}
