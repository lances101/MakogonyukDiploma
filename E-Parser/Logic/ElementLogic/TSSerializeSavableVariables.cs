using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using E_Parser.UI.Elements;

namespace E_Parser.Logic.ElementLogic
{
    [Serializable]
    public class TSSerializeSavableVariables : TSBase
    {
        public TSSerializeSavableVariables(TaskSession ts)
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
            get { return "Serialize saveble"; }
        }

        protected override object _mainTaskMethod(object args)
        {
            XmlSerializer xml = new XmlSerializer(Session.SaveableVariables.GetType());
            using (var fs = new FileStream(DirectStringInput, FileMode.Create))
            {
                xml.Serialize(fs, Session.SaveableVariables);
            }
            return args;

        }

        
        protected override void StaticTypes(TaskSession ts)
        {
            InputTypes = new List<ParameterTypes>() { ParameterTypes.Any };
            OutputType = ParameterTypes.Any;
            ElemType = typeof(ElemSerializeSavableVariables);
        }
    }
}
