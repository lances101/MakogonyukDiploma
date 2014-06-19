using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using E_Parser.UI;
using E_Parser.UI.Elements;

namespace E_Parser.Logic.ElementLogic
{
    [Serializable]
    public class TSStart : TSBase
    {
        
        public TSStart(TaskSession ts) : base(ts)
        {
           
        }

        public override string Name
        {
            get { return "StartSession"; }
        }

        protected override object _mainTaskMethod(object args)
        {
           
            return null;
        }

        protected override void StaticTypes(TaskSession ts)
        {
            InputTypes = new List<ParameterTypes>() { ParameterTypes.None };
            OutputType = ParameterTypes.None;
            ElemType = typeof(ElemStart);
        }
    }
}
