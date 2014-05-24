using System;
using System.Collections.Generic;
using System.Configuration;
using System.Threading.Tasks;
using E_Parser.UI;
using E_Parser.UI.Elements;
using HtmlAgilityPack;

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
            Double,
            NodeList,
            Node,
            Boolean,
            PassThrough,
            ParsedItem
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

        public abstract string Name { get; }
        public List<ParameterTypes> InputTypes { get; set; }
        public ParameterTypes OutputType { get; set; }

        public Type OutputTypeAsType
        {
            get
            {
                switch (OutputType)
                {
                    case ParameterTypes.None:
                        return null;
                        break;
                    case ParameterTypes.Any:
                        return typeof (object);
                        break;
                    case ParameterTypes.String:
                        return typeof (string);
                        break;
                    case ParameterTypes.Double:
                        return typeof (int);
                        break;
                    case ParameterTypes.NodeList:
                        return typeof (HtmlNodeCollection);
                        break;
                    case ParameterTypes.Node:
                        return typeof (HtmlNode);
                        break;
                    case ParameterTypes.Boolean:
                        return typeof (bool);
                        break;
                    case ParameterTypes.PassThrough:
                        return typeof (object);
                        break;
                }
                return null;
            }
        }
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
        protected abstract object _mainTaskMethod(object args);

        public virtual void AfterTaskAddition()
        {
            
        }
        public virtual void AfterTaskEnd()
        {
            
        }
        public virtual void BeforeDeletion()
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
        public void StartTask(object args)
        {
            Task<object> task = Task.Factory.StartNew(() =>
            {
                try
                {
                    return _mainTaskMethod(args);
                }
                catch (Exception e)
                {
                    return e;
                }
                return null;
            });
            if (this.GetType() == typeof (TSEnd))
            {
                task.Wait();
                return;
            }
            task.ContinueWith(continuation => OnTaskCompleted(task.Result));
            
        }

        public object StartTaskNoWait(object args)
        {
            return _mainTaskMethod(args);
        }

        private void OnTaskCompleted(object result)
        {
            if (OnTaskEnd != null)
            {
                if (result is Exception)
                {
                    OnTaskEnd(this, new TaskEndEventArgs(false, result));
                }
                else
                {
                    OnTaskEnd(this, new TaskEndEventArgs(true, result));
                }

            }
        }
    }
}