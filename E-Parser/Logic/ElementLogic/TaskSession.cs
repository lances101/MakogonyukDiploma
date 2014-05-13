using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
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
        public Dictionary<string, object> SessionVariables = new Dictionary<string, object>();
        private int currentTaskIndex;

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
            AddNewTask(null, typeof (ElemStart));

        }

        #region SessionControl
        public void StartSession()
        {
            CurrentTaskIndex = 0;
            ClearVariables();
            _functionalElements.ElementAt(0).StartTask(null);
            
        }

        void ClearVariables()
        {
            for (int i = 0; i < SessionVariables.Count; i++)
            {
                SessionVariables[SessionVariables.Keys.ElementAt(i)] = null;
            }
        }
        
        public void TryAddEventSubscription(TSBase ts)
        {
            if (!ts.IsBeingSubscribed())
                ts.OnTaskEnd += Task_OnTaskEnd;
        }


        private void Task_OnTaskEnd(object sender, TaskEndEventArgs e)
        {
            
            if (!e.EventSuccessful) throw new Exception("NEED TO HANDLE THIS");
            var lastTask = sender as TSBase;
            CurrentTaskIndex = GetTaskIndex(lastTask);
            lastTask.AfterTaskEnd();
            if (lastTask.GetType() != typeof(TSEnd))
            {
                FunctionalElements.ElementAt(CurrentTaskIndex + 1).StartTask(e.Result);
            }
            else
                EndSession();
        }

        public void EndSession()
        {
            SessionEditor.Log("Session has ended");
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
            task.AfterTaskAddition();
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

    }
}