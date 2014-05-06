using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Parser.Logic.ElementLogic
{
    public abstract class BaseTaskSequence
    {
        public enum eParameterTypes
        {
            None, String, Integer, NodeList, Node, Boolean
        }
       
        public eParameterTypes ParameterTypes = eParameterTypes.None;
        public eParameterTypes ReturnedTypes = eParameterTypes.None;
        public BaseTaskSequence NextTask { get; set; }
        private Func<object, object> _mainTaskMethod;
        protected TaskSession Session;

        public BaseTaskSequence(TaskSession ts)
        {
            Session = ts;
        }

        protected void SetMainTask(Func<object, object> func, eParameterTypes acceptsTypes, eParameterTypes returnsTypes)
        {
            _mainTaskMethod = func;
            ParameterTypes = acceptsTypes;
            ReturnedTypes = returnsTypes;
        }
        public void StartTask(params object[] args)
        {
            var task = Task.Factory.StartNew(() => 
            {
                return _mainTaskMethod(args);
            });
            task.ContinueWith(continuation => OnTaskCompleted(task.Result));
        }
        private void OnTaskCompleted(object result)
        {
            NextTask.StartTask(result);
        }

       
    }
}
