using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Parser.Logic.ElementLogic
{
    class ParameterOperation
    {
        enum StringOperations
        {
            [Description("Lower Case")]
            ToLowerCase,
            [Description("Upper Case")]
            ToUpperCase,
            [Description("Replace")]
            ReplaceWithDirect
        }

        enum HtmlNodeOperations
        {
            [Description("Extract Attribute")]
            AttributeExtract,
            [Description("Inner Text")]
            InnerText,
            [Description("Inner HTML")]
            InnerHTML
        }
    }
}
