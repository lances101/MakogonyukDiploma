using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_Parser.UI.Elements;

namespace E_Parser.Logic.ElementLogic
{
    [Serializable]
    public class TSParameterTap : TSBase
    {
        public TSParameterTap(TaskSession ts) : base(ts)
        {
            InputTypes = new List<ParameterTypes>() { ParameterTypes.Any };
            OutputType = ParameterTypes.Any;
        }

        public override string GetName
        {
            get { return "ParameterTap"; }
        }

        protected override object _mainTaskMethod(object[] args)
        {
            switch (PreviousTask.OutputType)
            {
                case ParameterTypes.None:
                    break;
                case ParameterTypes.String:
                    
                    break;
                case ParameterTypes.Integer:
                    break;
                case ParameterTypes.NodeList:
                    break;
                case ParameterTypes.Node:
                    break;
                case ParameterTypes.Boolean:
                    break;
            }

            return null;
        }
    }
}
