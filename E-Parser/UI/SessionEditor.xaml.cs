using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using E_Parser.Logic;
using E_Parser.Logic.ElementLogic;
using E_Parser.UI.Elements;
using Microsoft.Win32;
using TheE_Parser;

namespace E_Parser.UI
{
    public enum LogType
    {
        Debug, Message, Error
    }

    public partial class SessionEditor : Window
    {
        private static RichTextBox rtbxLogBox;
        private static bool displayDebug = false;
        private TaskSession _session;
        private List<ElemBase> _visualElements = new List<ElemBase>();
        public List<ElemBase> VisualElements
        {
            get { return _visualElements; }
            set { _visualElements = value; }
        }

        public bool RestartOnError
        {
            get { return _session.RestartOnError; }
            set { _session.RestartOnError = value; }
        }
        private static void LogUninvoked(LogType type, string mess)
        {
            string color = "";
            switch (type)
            {
                case LogType.Debug:
                    if (!displayDebug) return;
                    color = "Blue";
                    break;
                case LogType.Message:
                    color = "Black";
                    break;
                case LogType.Error:
                    color = "Red";
                    break;
            }
            
            rtbxLogBox.AppendText(DateTime.Now.ToShortTimeString() + " | " + type.ToString() + " - " + mess + "\n", color);
            rtbxLogBox.ScrollToEnd();
            if (!Directory.Exists("./Logs/"))
                Directory.CreateDirectory("./Logs/");
            using (StreamWriter sw = File.AppendText("./Logs/SessionLog.txt"))
            {
                sw.Write(DateTime.Now.ToString() + " | " + type.ToString() + " - " + mess + "\r\n");
            }
        }
        public static void Log(LogType type, string message)
        {

            if (rtbxLogBox == null) return;
            rtbxLogBox.Dispatcher.InvokeAsync(new Action(() => LogUninvoked(type, message))); 
        }

        public static void Log(LogType type, string format, params object[] args)
        {
            Log(type, String.Format(format, args));
        }

        public static void Log(LogType type, object obj)
        {
            Log(type, obj.ToString());
        }

        public bool OpenFileDialog(bool save, out string filename, out string title)
        {
            // Create OpenFileDialog 
            FileDialog dlg = null;
            if (save)
                dlg = new Microsoft.Win32.SaveFileDialog();
            else
                dlg = new Microsoft.Win32.OpenFileDialog();

            // Set filter for file extension and default file extension 
            dlg.DefaultExt = ".psf";
            dlg.Filter = "Parser Sequence File|*.psf";


            // Display OpenFileDialog by calling ShowDialog method 
            Nullable<bool> result = dlg.ShowDialog();


            // Get the selected file name and display in a TextBox 
            if (result == true)
            {
                // Open document 
                filename = dlg.FileName;
                title = dlg.SafeFileName;
            }
            else
            {
                filename = "";
                title = "";
            }
            return result.Value;

        }
        public void LoadSession()
        {
            string fileName, title;
            if (!OpenFileDialog(false, out fileName, out title)) return;
            lastOpenedFile = fileName;
            lastOpenedFileTitle = title;
            BinaryFormatter bf = new BinaryFormatter();
            using (FileStream fs = new FileStream(fileName, FileMode.Open))
            {
                CurrentSession = (TaskSession) bf.Deserialize(fs);
            }
            CurrentSession.VisualElements = VisualElements;
            CurrentSession.Client = new AwesomiumWrap();
            VisualElements.Clear();
            foreach (var e in CurrentSession.FunctionalElements)
                CurrentSession.RestoreTask(e);
            this.Title = title;
            
            Log(LogType.Message, "Session {0} was loaded", title);
        }

        private string lastOpenedFile = "";
        private string lastOpenedFileTitle = "";
        public void SaveSession(bool current)
        {
            string fileName = "", title = "";
            if (current)
            {
                fileName = lastOpenedFile;
                title = lastOpenedFileTitle;
            }
            else
            {
                if (!OpenFileDialog(true, out fileName, out title)) return;    
            }
            
            if (String.IsNullOrEmpty(fileName)) return;
            BinaryFormatter bf = new BinaryFormatter();
            using (FileStream fs = new FileStream(fileName, FileMode.Create))
            {
                bf.Serialize(fs, _session);
            }
            lastOpenedFile = fileName;
            lastOpenedFileTitle = title;
            Log(LogType.Message, "Session {0} saved", title);
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
                    case "tElemDebugRenderBrowser":
                        AddNewTask(SessionViewer.SelectedItem as ElemBase, typeof(ElemDebugBrowserRender));
                        break;
                    case "tElemHonk":
                        AddNewTask(SessionViewer.SelectedItem as ElemBase, typeof(ElemHonk));
                        break;
                            
                    //routine specific
                    case "tElemStart":
                        AddNewTask(SessionViewer.SelectedItem as ElemBase, typeof(ElemStart));
                        break;
                    case "tElemEnd":
                        AddNewTask(SessionViewer.SelectedItem as ElemBase, typeof(ElemEnd));
                        break;
                    case "tElemRestart":
                        AddNewTask(SessionViewer.SelectedItem as ElemBase, typeof(ElemRestart));
                        break;

                    //File control
                    case "tElemFileAppend":
                        AddNewTask(SessionViewer.SelectedItem as ElemBase, typeof(ElemFileAppend));
                        break;

                    //variable control
                    case "tElemTextInput":
                        AddNewTask(SessionViewer.SelectedItem as ElemBase, typeof(ElemTextInput));
                        break;
                    case "tElemStoreSingleVariable":
                        AddNewTask(SessionViewer.SelectedItem as ElemBase, typeof(ElemStoreSingleVariable));
                        break;
                    case "tElemLoadSingleVariable":
                        AddNewTask(SessionViewer.SelectedItem as ElemBase, typeof(ElemVariableReadLoaded));
                        break;
                    case "tElemParameterTap":
                        AddNewTask(SessionViewer.SelectedItem as ElemBase, typeof(ElemParameterTap));
                        break;

                        // variable / serialization
                        case "tElemSaveSingleVariable":
                            AddNewTask(SessionViewer.SelectedItem as ElemBase, typeof(ElemSaveSingleVariable));
                            break;
                        case "tElemSerializeSavebleVariables":
                            AddNewTask(SessionViewer.SelectedItem as ElemBase, typeof(ElemSerializeSavableVariables));
                            break;
                        case "tElemDeserializeSavebleVariables":
                            AddNewTask(SessionViewer.SelectedItem as ElemBase, typeof(ElemDeserializeSavebleVariables));
                            break;
                    case "tElemVariableReadLoaded":
                            AddNewTask(SessionViewer.SelectedItem as ElemBase, typeof(ElemVariableReadLoaded));
                            break;
                    
                    //cyclomatic stuff
                    case "tElemIfStart":
                        AddNewTask(SessionViewer.SelectedItem as ElemBase, typeof(ElemIfStart));
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

        private void StartSession_Click(object sender, RoutedEventArgs e)
        {
            CurrentSession.StartSession("Start pressed");
        }
        private void SaveSession_Click(object sender, RoutedEventArgs e)
        {
            SaveSession(true);
            SessionViewer.Items.Refresh();
        }

        private void LoadSession_Click(object sender, RoutedEventArgs e)
        {
            LoadSession();
            SessionViewer.Items.Refresh();
        }

        private void Context_DeleteClick(object sender, RoutedEventArgs e)
        {
            if (SessionViewer.SelectedItem == null) return;
            CurrentSession.DeleteItem(SessionViewer.SelectedItem as ElemBase);
            SessionViewer.Items.Refresh();       
        }

        private void btn_ForceStop(object sender, RoutedEventArgs e)
        {
            CurrentSession.ForceStop();
        }

        private void btn_Clear(object sender, RoutedEventArgs e)
        {
            CurrentSession.Clear();
            SessionViewer.Items.Refresh();
        }

        private void SaveSessionAs_Click(object sender, RoutedEventArgs e)
        {
            SaveSession(false);
            SessionViewer.Items.Refresh();
        }

        private void ChkbxDisplayDebug_OnChecked(object sender, RoutedEventArgs e)
        {
            displayDebug = chkbxDisplayDebug.IsChecked.Value;
        }

        private void ChkbxDisplayDebug_OnUnchecked(object sender, RoutedEventArgs e)
        {
            displayDebug = chkbxDisplayDebug.IsChecked.Value;
        }

        private void btnVariableList_Click(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
