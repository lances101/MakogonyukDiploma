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

        public override string Name
        {
            get { return "FindNodes"; }
        }

        protected override object _mainTaskMethod(object args)
        {
            HtmlNodeCollection collection = null;
            collection = Session.Client.HAP.GetMultipleElements(args as string);    
            return collection;
        }

        protected override void StaticTypes(TaskSession ts)
        {
            InputTypes = new List<ParameterTypes>() { ParameterTypes.String, ParameterTypes.Node };
            OutputType = ParameterTypes.None;
        }
    }
}
