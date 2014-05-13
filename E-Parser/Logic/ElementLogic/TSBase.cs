using System;
using System.Collections.Generic;
using System.Configuration;
using System.Threading.Tasks;
using E_Parser.UI;
using E_Parser.UI.Elements;

namespace E_Parser.Logic.ElementLogic
{
    public delegate void TaskEndedHandler(object sender, TaskEndEventArgs e);

    public class TaskEndEventArgs : EventArgs
    {
        public TaskEndEventArgs(bool success, object result)
        {
            EventSuccessful = success;
            Result = result;
        }

        public bool EventSuccessful { get; set; }
        public object Result { get; set; }
    }

    [Serializable]
    public abstract class TSBase
    {
        public enum ParameterTypes
        {
            None,
            Any,
            String,
            Integer,
            NodeList,
            Node,
            Boolean,
            PassThrough
        }

        private bool _isRunning;

        [NonSerialized] private ElemBase _visualElem;

        protected TSBase(TaskSession ts)
        {
            Session = ts;
            DirectStringInput = "";
            StaticTypes(ts);
        }

        public ElemBase VisualElement
        {
            get { return _visualElem; }
            set { _visualElem = value; }
        }

        public bool IsRunning
        {
            get { return _isRunning; }
            set { _isRunning = value; }
        }

        public abstract string GetName { get; }
        public List<ParameterTypes> InputTypes { get; set; }
        public ParameterTypes OutputType { get; set; }
        protected string directStringInput = "";
        public virtual string DirectStringInput
        {
            get { return directStringInput; }
            set { directStringInput = value; }
        }

        public TSBase NextTask
        {
            get { return Session.GetNextTask(this); }
        }

        public TSBase PreviousTask
        {
            get { return Session.GetPreviousTask(this); }
        }

        public Type ElemType { get; set; }

        public TaskSession Session { get; set; }
        public event TaskEndedHandler OnTaskEnd;
        protected abstract object _mainTaskMethod(object[] args);

        public virtual void AfterTaskAddition()
        {
            
        }
        public virtual void AfterTaskEnd()
        {
            
        }
        
        protected abstract void StaticTypes(TaskSession ts);

        public bool IsBeingSubscribed()
        {
            if (OnTaskEnd == null)
            {
                return false;
            }
            return true;
        }
        public void StartTask(params object[] args)
        {
            
            IsBeingSubscribed();
            Task<object> task = Task.Factory.StartNew(() => { return _mainTaskMethod(args); });
            if (this.GetType() == typeof (TSEnd))
            {
                task.Wait();
                return;
            }
            task.ContinueWith(continuation => OnTaskCompleted(task.Result));
            
        }

        private void OnTaskCompleted(object result)
        {
            if (OnTaskEnd != null)
            {
                OnTaskEnd(this, new TaskEndEventArgs(true, result));
            }
        }
    }
}