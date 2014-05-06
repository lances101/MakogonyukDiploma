using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Parser.Logic.ElementLogic
{
    class TSLoadUrl : BaseTaskSequence
    {
        public TSLoadUrl(TaskSession ts) : base(ts)
        {
            task = new Func<object, object>(o => LoadURL(o as string));
            SetMainTask(task, eParameterTypes.String, eParameterTypes.Boolean);
        }

        private Func<object, object> task;
        private bool LoadURL(string url)
        {
            return Session.Client.LoadUrl(url);
        }
    }
}
