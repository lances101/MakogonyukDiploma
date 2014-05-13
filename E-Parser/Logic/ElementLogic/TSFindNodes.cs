using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace E_Parser.Logic.ElementLogic
{
    [Serializable]
    public class TSFindNodes : TSBase
    {
        public TSFindNodes(TaskSession ts) : base(ts)
        {
           
            

        }

        public override string GetName
        {
            get { return "FindNodes"; }
        }

        protected override object _mainTaskMethod(object[] args)
        {
            HtmlNodeCollection collection = null;

            if (args.Count() == 2)
                collection = (args[1] as HtmlNode).SelectNodes(args[0] as string);
            else
            {
                collection = Session.Client.HAP.GetMultipleElements(args[0] as string);    
            }
            return collection;
        }

        protected override void StaticTypes(TaskSession ts)
        {
            InputTypes = new List<ParameterTypes>() { ParameterTypes.String, ParameterTypes.Node };
            OutputType = ParameterTypes.None;
        }
    }
}
