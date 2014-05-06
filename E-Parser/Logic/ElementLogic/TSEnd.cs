using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Parser.Logic.ElementLogic
{
    class TSEnd : BaseTaskSequence
    {
        public TSEnd(TaskSession ts) : base(ts)
        {
            InputTypes = new List<ParameterTypes>() { ParameterTypes.Any };
            OutputTypes = new List<ParameterTypes>() { ParameterTypes.None };
        }

        protected override object _mainTaskMethod(object[] args)
        {
            Session.EndSession();
            return null;
        }
    }
}
