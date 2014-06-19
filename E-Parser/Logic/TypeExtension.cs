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
        public static ParameterTypes ConverToParameterTypes(this Type _type)
        {
            if(_type == typeof(string))
                return ParameterTypes.String;
            if (_type == typeof (HtmlNode))
                return ParameterTypes.Node;
            if (_type == typeof (HtmlNodeCollection))
                return ParameterTypes.NodeList;
            if (_type == typeof (double))
                return ParameterTypes.Double;
            if (_type == typeof (int))
                return ParameterTypes.Double;
            if (_type == typeof (bool))
                return ParameterTypes.Boolean;
            return  ParameterTypes.None;
            
        }
    }
}
