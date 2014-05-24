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

        public override void AfterTaskAddition()
        {
            DirectStringInput = String.Format("variable{0}", Session.SessionVariables.Count);
            Session.SessionVariables.Add(new StoredVariable()
            {
                Name = DirectStringInput,
                Type = PreviousTask.OutputTypeAsType,
                Value = null
            });
        }

        public override void BeforeDeletion()
        {
            Session.SessionVariables.RemoveAll(o => o.Name == DirectStringInput);
        }

        public override string Name
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
            foreach (var storedVariable in Session.SessionVariables)
            {
                if (storedVariable.Name == directStringInput)
                {
                    storedVariable.Name = value;
                    storedVariable.Type = PreviousTask.OutputTypeAsType;
                }
            }

        }

        protected override object _mainTaskMethod(object args)
        {
            Session.SessionVariables.Find(o => o.Name == directStringInput).Value = args;
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
