using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_Parser.UI.Elements;

namespace E_Parser.Logic.ElementLogic
{
    public enum PIInteract
    {
        [Description("Change type")]
        ChangeType,
        [Description("Add field")]
        AddField,
        [Description("Add field")]
        ChangeField,
        [Description("Remove field")]
        RemoveField,
        [Description("Add to list")]
        SaveToList

    }
    [Serializable]
    public class TSParsedItemInteract : TSBase
    {
        public TSParsedItemInteract(TaskSession ts)
            : base(ts)
        {
            
        }

        private ParsedItem parsedItem = new ParsedItem("empty");
        public ParsedItem ParsedItem
        {
            get { return parsedItem; }
            set
            {
                parsedItem = value;
                
            }
        }

        public override string Name
        {
            get { return "Parsed Item Interaction"; }
        }

        protected override object _mainTaskMethod(object args)
        {
            return null;
            //return parsedItem.Value;
        }

        protected override void StaticTypes(TaskSession ts)
        {
            InputTypes = new List<ParameterTypes>() { ParameterTypes.Any};
            OutputType = ParameterTypes.ParsedItem;
            //ElemType = typeof(ElemParsedItemInteract);
        }
    }
}
