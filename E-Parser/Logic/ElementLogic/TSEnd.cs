using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_Parser.UI.Elements;

namespace E_Parser.Logic.ElementLogic
{
    [Serializable]
    public class TSEnd : TSBase
    {
        
        public TSEnd(TaskSession ts) : base(ts)
        {
            
        }

        public override string Name
        {
            get { return "SessionEnd"; }
        }

        protected override object _mainTaskMethod(object args)
        {
            Session.EndSession("Task end reached");
            return null;
        }

        protected override void StaticTypes(TaskSession ts)
        {
            InputTypes = new List<ParameterTypes>() { ParameterTypes.Any };
            OutputType = ParameterTypes.None;
            ElemType = typeof(ElemEnd);
        }
    }
}
