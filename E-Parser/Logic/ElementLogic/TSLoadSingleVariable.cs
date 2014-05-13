using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_Parser.UI.Elements;

namespace E_Parser.Logic.ElementLogic
{
    [Serializable]
    public class TSLoadSingleVariable : TSBase
    {
        public TSLoadSingleVariable(TaskSession ts)
            : base(ts)
        {
            
        }


        public override string GetName
        {
            get { return "Load Variable"; }
        }

        protected override object _mainTaskMethod(object[] args)
        {
            return Session.SessionVariables[args[0].ToString()];
        }

        protected override void StaticTypes(TaskSession ts)
        {
            InputTypes = new List<ParameterTypes>() { ParameterTypes.Any };
            OutputType = ParameterTypes.Any;
            ElemType = typeof(ElemLoadSingleVariable);
        }
    }
}
