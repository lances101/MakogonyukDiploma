using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace E_Parser.Logic
{
    [Serializable]
    public class StoredVariable
    {
        public string Name { get; set; }


        
        private Type _type;

        public object Value { get; set; }
        
        [XmlIgnore] 
        public Type Type
        {
            get
            {
                if (_type == null)
                    if(Value != null)
                    _type = Value.GetType();
                return _type;
            }
            set { _type = value; }
        }

        
    }
}
