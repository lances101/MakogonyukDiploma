using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_Parser.UI.Elements;

namespace E_Parser.Logic.ElementLogic
{
    [Serializable]
    public class TSStoreSingleVariable : TSBase
    {
        public TSStoreSingleVariable(TaskSession ts)
            : base(ts)
        {
            
        }


        public override string GetName
        {
            get { return "Store Variable"; }
        }

        public override string DirectStringInput
        {
            get { return directStringInput; }
            set
            {
                VariableNameChanged(value);
                directStringInput = value;
            }
        }

        public void VariableNameChanged(string value)
        {
            if (value == directStringInput) return;
            for(int i = 0; i < Session.SessionVariables.Count; i++)
            {
                if (Session.SessionVariables.Keys.ElementAt(i) == directStringInput)
                {
                    Session.SessionVariables.Remove(directStringInput);
                    break;
                }
            }
            Session.SessionVariables.Add(value, null);
        }

        protected override object _mainTaskMethod(object[] args)
        {
            Session.SessionVariables[directStringInput] = args[0];
            return args;
        }

        protected override void StaticTypes(TaskSession ts)
        {
            InputTypes = new List<ParameterTypes>() { ParameterTypes.Any };
            OutputType = ParameterTypes.Any;
            ElemType = typeof(ElemStoreSingleVariable);
        }
    }
}
