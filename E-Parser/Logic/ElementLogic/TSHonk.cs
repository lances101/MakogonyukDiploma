using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_Parser.UI.Elements;

namespace E_Parser.Logic.ElementLogic
{
    [Serializable]
    class TSHonk : TSBase
    {
        public TSHonk(TaskSession ts) : base(ts)
        {
        }

        public override string Name
        {
            get { return "Honk!"; }
        }

        protected override object _mainTaskMethod(object args)
        {
            Console.Beep(4000, 5000);
            return args;
        }

        protected override void StaticTypes(TaskSession ts)
        {
            InputTypes = new List<ParameterTypes>() { ParameterTypes.Any};
            OutputType = ParameterTypes.Any;
            ElemType = typeof(ElemHonk);
        }
    }
}
