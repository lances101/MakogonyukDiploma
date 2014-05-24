using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Parser.Logic
{
    [Serializable]
    public class ParsedField
    {
        private string itemType;
        
        public ParsedField(string _itemType)
        {
            itemType = _itemType;
        }
        public string Value { get; set; }
        public string ItemType
        {
            get { return itemType; }
            set { itemType = value; }
        }
    }
}
