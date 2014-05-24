using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Media;
using E_Parser.UI;
using E_Parser.UI.Elements;
using TheE_Parser;

namespace E_Parser.Logic.ElementLogic
{
    [Serializable]
    public class TaskSession
    {
        private List<TSBase> _functionalElements = new List<TSBase>();
        [NonSerialized()] public List<ElemBase> VisualElements;
        [NonSerialized()] private AwesomiumWrap _client;
        public List<StoredVariable> SessionVariables = new List<StoredVariable>();
        public List<StoredVariable> SaveableVariables = new List<StoredVariable>(); 
        public List<ParsedItem> ParsedItems = new List<ParsedItem>();
        private int currentTaskIndex;
        public bool TaskIsRunning { get; set; }
        private bool ShouldForceStop = false;

        public AwesomiumWrap Client
        {
            get
            {
                if (_client == null)
                {
                    _client = new AwesomiumWrap();
                }
                return _client;
            }
            set { _client = value; }
        }
        public List<TSBase> FunctionalElements
        {
            get { return _functionalElements; }
            set { _functionalElements = value; }
        }

        public int CurrentTaskIndex
        {
            get { return currentTaskIndex; }
            set { currentTaskIndex = value; }
        }

        public TaskSession(List<ElemBase> visualElements)
        {
            
            VisualElements = visualElements;
            Client = new AwesomiumWrap();
            var start = AddNewTask(null, typeof (ElemStart));
            SaveableVariables = new List<StoredVariable>();
            AddNewTask(start, typeof(ElemEnd));

        }

        #region SessionControl
        public void StartSession()
        {
            CurrentTaskIndex = 0;
            ClearVariables();
            TaskIsRunning = true;
            _functionalElements.ElementAt(0).StartTask(null);
            
        }

        void ClearVariables()
        {
            foreach (var storedVariable in SessionVariables)
            {
                storedVariable.Value = null;
            }
        }
        
        public void TryAddEventSubscription(TSBase ts)
        {
            if (!ts.IsBeingSubscribed())
                ts.OnTaskEnd += Task_OnTaskEnd;
        }


        private void Task_OnTaskEnd(object sender, TaskEndEventArgs e)
        {
            var lastTask = sender as TSBase;
            CurrentTaskIndex = GetTaskIndex(lastTask);
            if (ShouldForceStop)
            {
                EndSession("Force stop fired");
                return;
            }
            if (!e.EventSuccessful)
            {
                EndSession(String.Format("Task error at {0}\n{1} - {2}", CurrentTaskIndex, lastTask.Name, (e.Result as Exception).Message));
                return;
            }
            
            lastTask.AfterTaskEnd();
            if (lastTask.GetType() == typeof (TSRestart))
            {
                RestartSession();
                return;
            }
            if(lastTask.GetType() == typeof(TSEnd))
            {
                EndSession("Task reached End node");
            }

            FunctionalElements.ElementAt(CurrentTaskIndex + 1).StartTask(e.Result);
        }

        public void EndSession(string reason)
        {
            ShouldForceStop = false;
            TaskIsRunning = false;
            SessionEditor.Log("[Session End] Reason : " + reason);
            
        }

        public void RestartSession()
        {
            EndSession("Session restart fired");
            Thread.Sleep(1000);
            StartSession();
        }
        public void ForceStop()
        {
            ShouldForceStop = true;
        }

        public void Clear()
        {
            FunctionalElements.Clear();
            VisualElements.Clear();
            SessionVariables.Clear();
            var start = AddNewTask(null, typeof(ElemStart));
            AddNewTask(start, typeof(ElemEnd));
        }
        #endregion

        #region TaskManagement&Validation
        public ElemBase AddNewTask(ElemBase selected, Type elemType)
        {
            ElemBase newElem = Activator.CreateInstance(elemType, this) as ElemBase;
            TryAddEventSubscription(newElem.Task);
            if (VisualElements.Count > 0)
            {
                if (newElem is ElemStart) return null;
                if (newElem is ElemEnd && VisualElements.Exists(o => o.GetType() == typeof (ElemEnd))) return null;
                if (selected == null)
                {
                    if (TryAddNewElement(VisualElements.Last(), newElem)) return newElem;
                }
                else
                {
                    if (TryAddNewElement(selected, newElem)) return newElem;
                }
            }
            else
            {
                if (newElem is ElemStart)
                {
                    TryAddNewElement(null, newElem);
                    return newElem;
                }
            }
            return null;
        }
        public void RestoreTask(TSBase task)
        {
            var newElem = Activator.CreateInstance(task.ElemType, task) as ElemBase;
            TryAddEventSubscription(task);
            task.VisualElement = newElem;
            VisualElements.Add(newElem);
            newElem.AfterElementAddition();
            //task.AfterTaskAddition();
        }
        private bool TryAddNewElement(ElemBase origin, ElemBase adding)
        {
            if (origin != null && adding.GetType() != typeof(ElemStart))
            {
                if (!IsParameterCompatible(origin, adding)) return false;
                if (!IsLogicCompatible(origin, adding)) return false;
            }
            InsertTaskAfterElement(origin, adding);
            if (origin != null) origin.NextElementReaction(adding);
            adding.Task.AfterTaskAddition();
            adding.AfterElementAddition();
            return true;
        }
        private void InsertTaskAfterElement(ElemBase origin, ElemBase adding)
        {
            adding.Task.VisualElement = adding;
            if (origin == null && adding.GetType() == typeof(ElemStart))
            {
                FunctionalElements.Add(adding.Task);
                VisualElements.Add(adding);
                return;
            }
            FunctionalElements.Insert(FunctionalElements.IndexOf(origin.Task) + 1, adding.Task);
            VisualElements.Insert(VisualElements.IndexOf(origin) + 1, adding);
            
        }
        private bool IsParameterCompatible(ElemBase origin, ElemBase adding)
        {
            if (adding.Task.InputTypes.Contains(TSBase.ParameterTypes.Any)) return true;
            if ((from inp in adding.Task.InputTypes
                where origin.Task.OutputType == inp
                select inp)
                .Any())
            {
                return true;
            }
            return false;
        }
        private bool IsLogicCompatible(ElemBase origin, ElemBase adding)
        {
            if (origin.GetType() == typeof (ElemEnd)) return false;
            return true;
        }
        #endregion

        #region ExtensionMethods

        public int GetTaskIndex(TSBase task)
        {
            return _functionalElements.FindIndex(o => o == task);
        }
        public TSBase GetNextTask(TSBase origin)
        {
            var originIndex = _functionalElements.FindIndex(o => o == origin);
            if (originIndex + 1 >= _functionalElements.Count) return null;
            return _functionalElements[originIndex + 1];
        }
        public TSBase GetPreviousTask(TSBase origin)
        {
            var originIndex = _functionalElements.FindIndex(o => o == origin);
            if (originIndex == 0 ) return null;
            return _functionalElements[originIndex - 1];
        
        }
        #endregion

        public void DeleteItem(ElemBase elemBase)
        {
            int toDelete = this.GetTaskIndex(elemBase.Task);
            elemBase.Task.BeforeDeletion();
            this.FunctionalElements.RemoveAt(toDelete);
            this.VisualElements.RemoveAt(toDelete);
            
        }



    }

   
}