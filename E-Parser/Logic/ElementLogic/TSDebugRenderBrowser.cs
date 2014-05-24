using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using E_Parser.UI.Elements;

namespace E_Parser.Logic.ElementLogic
{
    [Serializable]
    public class TSDebugRenderBrowser : TSBase
    {
        
        public TSDebugRenderBrowser(TaskSession ts) : base(ts)
        {
           
        }

        public override string Name
        {
            get { return "StartSession"; }
        }

        protected override object _mainTaskMethod(object args)
        {
            Session.Client.RenderToPng();
            return args;
        }

        protected override void StaticTypes(TaskSession ts)
        {
            InputTypes = new List<ParameterTypes>() { ParameterTypes.Any};
            OutputType = ParameterTypes.Any;
            ElemType = typeof(ElemStart);
        }
    }
}
