using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Awesomium.Core;
using E_Parser.UI.Elements;

namespace E_Parser.Logic.ElementLogic
{
    [Serializable]
    public class TSFindSingleNode : TSBase
    {
        public TSFindSingleNode(TaskSession ts): base(ts)
        {
            InputTypes = new List<ParameterTypes>() { ParameterTypes.Any };
            OutputType = ParameterTypes.Node;
            ElemType = typeof(ElemFindSingleNode);
        }


        public override string GetName
        {
            get { return "Find Single Node"; }
        }

        protected override object _mainTaskMethod(object[] args)
        {
            return Session.Client.HAP.GetSingleElement(DirectStringInput);

        }

    
        
    }
}
