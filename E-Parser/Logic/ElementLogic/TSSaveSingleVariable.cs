using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_Parser.UI.Elements;

namespace E_Parser.Logic.ElementLogic
{
    [Serializable]
    public class TSSaveSingleVariable : TSBase
    {
        public TSSaveSingleVariable(TaskSession ts)
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

        public override string GetName
        {
            get { return "Save variable to HDD"; }
        }

        protected override object _mainTaskMethod(object args)
        {
            if (Session.SaveableVariables.Contains(storedVariable))
            {
                
            }
            else
                Session.SaveableVariables.Add(storedVariable);

            return args;

        }

        protected override void StaticTypes(TaskSession ts)
        {
            InputTypes = new List<ParameterTypes>() { ParameterTypes.Any };
            OutputType = ParameterTypes.Any;
            ElemType = typeof(ElemSaveSingleVariable);
        }
    }
}
