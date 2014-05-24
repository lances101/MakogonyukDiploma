using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_Parser.UI.Elements;

namespace E_Parser.Logic.ElementLogic
{
    [Serializable]
    public class TSVariableReadLoaded : TSBase
    {
        public TSVariableReadLoaded(TaskSession ts)
            : base(ts)
        {
            
        }

        private StoredVariable storedVariable = new StoredVariable();
        public StoredVariable StoredVariable
        {
            get { return storedVariable; }
            set
            {
                storedVariable = value;
                this.OutputType = storedVariable.Type.ConverToParameterTypes();
            }
        }

        public override string Name
        {
            get { return "Load Variable"; }
        }

        protected override object _mainTaskMethod(object args)
        {
            return storedVariable.Value;
        }

        protected override void StaticTypes(TaskSession ts)
        {
            InputTypes = new List<ParameterTypes>() { ParameterTypes.Any };
            OutputType = ParameterTypes.Any;
            ElemType = typeof(ElemVariableReadLoaded);
        }
    }
}
