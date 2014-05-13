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
    public class TSLoadUrl : TSBase
    {
        public TSLoadUrl(TaskSession ts) : base(ts)
        {
            
        }


        public override string GetName
        {
            get { return "LoadURL"; }
        }

        protected override object _mainTaskMethod(object[] args)
        {
            if (!Session.Client.LoadUrl(String.IsNullOrEmpty(DirectStringInput)? args[0] as string : DirectStringInput)) return false;
            while (Session.Client.IsLoading())
            {
                
            }
            Session.Client.RenderToPng();
            return true;
        }

        protected override void StaticTypes(TaskSession ts)
        {
            InputTypes = new List<ParameterTypes>() { ParameterTypes.String };
            OutputType = ParameterTypes.Boolean;
            ElemType = typeof(ElemLoadURL);
        }
    }
}
