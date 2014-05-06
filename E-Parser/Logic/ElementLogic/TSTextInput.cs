using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Parser.Logic.ElementLogic
{
    class TSTextInput : BaseTaskSequence
    {
        public TSTextInput(TaskSession ts) : base(ts)
        {
            InputTypes = new List<ParameterTypes>() { ParameterTypes.Any };
            OutputTypes = new List<ParameterTypes>() { ParameterTypes.String };
        }


        protected override object _mainTaskMethod(object[] args)
        {
            return DirectStringInput;
        }
    }
}
