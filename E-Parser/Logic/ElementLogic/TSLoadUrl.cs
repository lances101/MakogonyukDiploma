using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Awesomium.Core;

namespace E_Parser.Logic.ElementLogic
{
    class TSLoadUrl : BaseTaskSequence
    {
        public TSLoadUrl(TaskSession ts) : base(ts)
        {
            InputTypes = new List<ParameterTypes>() { ParameterTypes.String };
            OutputTypes = new List<ParameterTypes>() { ParameterTypes.Boolean };
        }



        protected override object _mainTaskMethod(object[] args)
        {
            if (!Session.Client.LoadUrl(args[0] as string)) return false;
            while (!Session.Client.View.IsDocumentReady)
            {
                
            }
            Session.Client.RenderToPng();
            return true;
        }
    }
}
