using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Parser.Logic.ElementLogic
{
    public class TSEnd : TSBase
    {
        public TSEnd(TaskSession ts) : base(ts)
        {
            InputTypes = new List<ParameterTypes>() { ParameterTypes.Any };
            OutputTypes = new List<ParameterTypes>() { ParameterTypes.None };
        }

        public override string GetName
        {
            get { return "SessionEnd"; }
        }

        protected override object _mainTaskMethod(object[] args)
        {
            Session.EndSession();
            return null;
        }
    }
}
