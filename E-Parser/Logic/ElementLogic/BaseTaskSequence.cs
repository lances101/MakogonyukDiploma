using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Parser.Logic.ElementLogic
{
    public abstract class BaseTaskSequence
    {
        public enum ParameterTypes
        {
            None, Any, String, Integer, NodeList, Node, Boolean
        }
       
        protected List<ParameterTypes> InputTypes { get; set; }
        protected List<ParameterTypes> OutputTypes { get; set; }
        public string DirectStringInput { get; set; }
        public BaseTaskSequence NextTask { get; set; }
        protected abstract object _mainTaskMethod(object[] args);
        protected TaskSession Session;

        public BaseTaskSequence(TaskSession ts)
        {
            Session = ts;
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
            if(NextTask.GetType() != typeof(TSEnd))
                NextTask.StartTask(result);
        }

       
    }
}
