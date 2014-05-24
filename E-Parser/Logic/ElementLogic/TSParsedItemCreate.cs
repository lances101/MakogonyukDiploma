using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_Parser.UI.Elements;

namespace E_Parser.Logic.ElementLogic
{
    class TSParsedItemCreate : TSBase
    {
       public TSParsedItemCreate(TaskSession ts)
            : base(ts)
        {
            
        }
        public ParsedItem ParsedItem { get; set; }

        public override string Name
        {
            get { return "New Parsed Item"; }
        }

        protected override object _mainTaskMethod(object args)
        {
            return DirectStringInput;
        }

        protected override void StaticTypes(TaskSession ts)
        {
            InputTypes = new List<ParameterTypes>() { ParameterTypes.Any };
            OutputType = ParameterTypes.ParsedItem;
            ElemType = typeof(ElemStoreSingleVariable);
        }
    
    }
}
