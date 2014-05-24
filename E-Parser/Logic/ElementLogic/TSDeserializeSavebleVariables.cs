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
    public class TSDeserializeSavebleVariables : TSBase
    {
        public TSDeserializeSavebleVariables(TaskSession ts)
            : base(ts)
        {
            
        }

        public override string GetName
        {
            get { return "Serialize saveble"; }
        }

        public void DeserializeNow()
        {
            if (!File.Exists(DirectStringInput)) return;
            XmlSerializer xml = new XmlSerializer(Session.SaveableVariables.GetType());
            using (var fs = new FileStream(DirectStringInput, FileMode.Open))
            {
                Session.SaveableVariables = xml.Deserialize(fs) as List<StoredVariable>;
            }
        }

        protected override object _mainTaskMethod(object args)
        {
           
            return args;
        }

        
        protected override void StaticTypes(TaskSession ts)
        {
            InputTypes = new List<ParameterTypes>() { ParameterTypes.Any };
            OutputType = ParameterTypes.Any;
            ElemType = typeof(ElemDeserializeSavebleVariables);
        }
    }
}
