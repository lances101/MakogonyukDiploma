using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_Parser.UI.Elements;

namespace E_Parser.Logic.ElementLogic
{
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
            Boolean
        }

        public abstract string GetName { get; }
        public List<ParameterTypes> InputTypes { get; set; }
        public ParameterTypes OutputType { get; set; }
        public string DirectStringInput { get; set; }
        private TSBase _nextTask;

        public TSBase NextTask
        {
            get { return _nextTask; }
            set
            {
                _nextTask = value;
                _nextTask.PreviousTask = this;
            }
        }

        protected abstract object _mainTaskMethod(object[] args);
        private TaskSession _session;
        protected TSBase PreviousTask;
        public Type ElemType { get; set; }

        public TaskSession Session
        {
            get { return _session; }
            set { _session = value; }
        }


        protected TSBase(TaskSession ts)
        {
            _session = ts;
        }


        public void StartTask(TSBase previous, params object[] args)
        {
            PreviousTask = previous;

            var task = Task.Factory.StartNew(() => { return _mainTaskMethod(args); });
            task.ContinueWith(continuation => OnTaskCompleted(task.Result));
        }

        private void OnTaskCompleted(object result)
        {
            if (NextTask.GetType() != typeof (TSEnd))
                NextTask.StartTask(this, result);
        }
    }
}