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

        }

        public override string GetName
        {
            get { return "ParameterTap"; }
        }

        public Type DetermineType()
        {
            return null;
        }
        protected override object _mainTaskMethod(object[] args)
        {
            switch (args[0].GetType().Name.ToString())
            {
                case null:
                    break;
                case "String":

                    break;
                case "Integer":

                    break;
                case "HtmlNodeCollection":

                    break;
                case "HtmlNode":

                    break;
                case "Boolean":

                    break;
            }

            return null;
        }

        protected override void StaticTypes(TaskSession ts)
        {
            InputTypes = new List<ParameterTypes>() { ParameterTypes.Any };
            OutputType = ParameterTypes.Any;
            throw new NotImplementedException();
        }
    }
}
