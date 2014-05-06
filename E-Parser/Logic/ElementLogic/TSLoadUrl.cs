using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Awesomium.Core;

namespace E_Parser.Logic.ElementLogic
{
    public class TSLoadUrl : TSBase
    {
        public TSLoadUrl(TaskSession ts) : base(ts)
        {
            InputTypes = new List<ParameterTypes>() { ParameterTypes.String };
            OutputTypes = new List<ParameterTypes>() { ParameterTypes.Boolean };
        }


        public override string GetName
        {
            get { return "LoadURL"; }
        }

        protected override object _mainTaskMethod(object[] args)
        {
            if (!Session.Client.LoadUrl(args[0] as string)) return false;
            while (Session.Client.IsLoading())
            {
                
            }
            Session.Client.RenderToPng();
            return true;
        }

    
        
    }
}
