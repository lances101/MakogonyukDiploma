using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace E_Parser.Logic.ElementLogic
{
    public class TSFindNodes : TSBase
    {
        public TSFindNodes(TaskSession ts) : base(ts)
        {
            InputTypes = new List<ParameterTypes>() { ParameterTypes.String, ParameterTypes.Node};
            OutputTypes = new List<ParameterTypes>() { ParameterTypes.NodeList};
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
    }
}
