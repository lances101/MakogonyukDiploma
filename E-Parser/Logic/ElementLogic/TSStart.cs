using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Parser.Logic.ElementLogic
{
    public class TSStart : TSBase
    {
        public TSStart(TaskSession ts) : base(ts)
        {
            InputTypes = new List<ParameterTypes>() { ParameterTypes.None };
            OutputTypes = new List<ParameterTypes>() { ParameterTypes.None };
        }

        public override string GetName
        {
            get { return "StartSession"; }
        }

        protected override object _mainTaskMethod(object[] args)
        {
            return null;
        }
    }
}
