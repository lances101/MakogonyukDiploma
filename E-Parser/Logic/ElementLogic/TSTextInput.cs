using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_Parser.UI.Elements;

namespace E_Parser.Logic.ElementLogic
{
    [Serializable]
    public class TSTextInput : TSBase
    {
        public TSTextInput(TaskSession ts) : base(ts)
        {
            
        }

        

        public override string Name
        {
            get { return "TextInput"; }
        }

        protected override object _mainTaskMethod(object args)
        {
            return DirectStringInput;
        }

        protected override void StaticTypes(TaskSession ts)
        {
            InputTypes = new List<ParameterTypes>() { ParameterTypes.Any };
            OutputType = ParameterTypes.String;
            ElemType = typeof(ElemTextInput);
        }
    }
}
