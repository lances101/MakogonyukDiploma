using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_Parser.UI.Elements;

namespace E_Parser.Logic.ElementLogic
{
    [Serializable]
    public class TSStart : TSBase
    {
        public TSStart(TaskSession ts) : base(ts)
        {
            InputTypes = new List<ParameterTypes>() { ParameterTypes.None };
            OutputType = ParameterTypes.None;
            ElemType = typeof(ElemStart);
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
