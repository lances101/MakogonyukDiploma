using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_Parser.Logic.ElementLogic;
using HtmlAgilityPack;

namespace E_Parser.Logic
{
    static class TypeExtension
    {
        public static TSBase.ParameterTypes ConverToParameterTypes(this Type _type)
        {
            if(_type == typeof(string))
                return TSBase.ParameterTypes.String;
            if (_type == typeof (HtmlNode))
                return TSBase.ParameterTypes.Node;
            if (_type == typeof (HtmlNodeCollection))
                return TSBase.ParameterTypes.NodeList;
            if (_type == typeof (double))
                return TSBase.ParameterTypes.Double;
            if (_type == typeof (int))
                return TSBase.ParameterTypes.Double;
            if (_type == typeof (bool))
                return TSBase.ParameterTypes.Boolean;
            return  TSBase.ParameterTypes.None;
            
        }
    }
}
