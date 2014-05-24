using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using E_Parser.UI.Elements;
using HtmlAgilityPack;

namespace E_Parser.Logic.ElementLogic
{
    public enum StringTap
    {
        [Description("Lower case")]
        ToLower,
        [Description("Upper case")]
        ToUpper,
        [Description("First [x] characters")]
        GetFirstXChars,
        [Description("Replace [x] with {y}")]
        Replace,
        [Description("Convert to number")]
        Parse
    }

    public enum DoubleTap
    {
        [Description("Augment by 1")]
        AddOne,
        [Description("Decrement by 1")]
        SubtractOne
    }
    public enum NodeTap
    {
        [Description("Attribute")]
        GetAttribute,
        [Description("Inner Text")]
        GetInnerText,
        [Description("Next Sibling")]
        GetNextSibling,
        [Description("PreviousSibling")]
        GetPreviousSibling
    }

    public enum NodeCollectionTap
    {
        [Description("First Node")]
        GetFirstNode,
        [Description("Last Node")]
        GetLastNode
    }
    [Serializable]
    public class TSParameterTap : TSBase
    {
        public NodeTap tNodeTap { get; set; }
        public StringTap tStringTap { get; set; }
        public DoubleTap TDoubleTap { get; set; }
        public TSParameterTap(TaskSession ts) : base(ts)
        {

        }

        public override string GetName
        {
            get { return "ParameterTap"; }
        }

        public ParameterTypes DetermineType(ParameterTypes outputType)
        {
            switch (outputType)
            {
                case ParameterTypes.None:
                    break;
                case ParameterTypes.Any:
                    break;
                case ParameterTypes.String:
                    switch (tStringTap)
                    {
                        case StringTap.Replace:
                            return ParameterTypes.String;
                        case StringTap.ToLower:
                            return ParameterTypes.String;
                            break;
                        case StringTap.ToUpper:
                            return ParameterTypes.String;
                            break;
                        case StringTap.GetFirstXChars:
                            return ParameterTypes.String;
                        case StringTap.Parse:
                            return ParameterTypes.Double;
                    }
                    break;
                case ParameterTypes.Double:
                    switch (TDoubleTap)
                    {
                        case DoubleTap.AddOne:
                            return ParameterTypes.Double;
                        case DoubleTap.SubtractOne:
                            return ParameterTypes.Double;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                    break;
                case ParameterTypes.NodeList:
                    break;
                case ParameterTypes.Node:
                    switch (tNodeTap)
                    {
                        case NodeTap.GetAttribute:
                            return ParameterTypes.String;
                            break;
                        case NodeTap.GetInnerText:
                            return ParameterTypes.String;
                            break;
                        case NodeTap.GetNextSibling:
                            return ParameterTypes.Node;
                            break;
                        case NodeTap.GetPreviousSibling:
                            return ParameterTypes.Node;
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                    break;
                case ParameterTypes.Boolean:
                    break;
                case ParameterTypes.PassThrough:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            return ParameterTypes.None;
        }
        protected override object _mainTaskMethod(object args)
        {
            switch (PreviousTask.OutputType)
            {
                case ParameterTypes.None:
                    break;
                case ParameterTypes.Any:
                    break;
                case ParameterTypes.String:
                    switch (tStringTap)
                    {
                        case StringTap.Replace:
                            var toReplace = args.ToString().Substring(args.ToString().IndexOf('['), args.ToString().IndexOf(']'));
                            var replaceWith = args.ToString().Substring(args.ToString().IndexOf('{'), args.ToString().IndexOf('}'));
                            return args.ToString().Replace(toReplace, replaceWith);
                        case StringTap.ToLower:
                            return args.ToString().ToLower();
                            break;
                        case StringTap.ToUpper:
                            return args.ToString().ToUpper();
                            break;
                        case StringTap.GetFirstXChars:
                            return args.ToString().Substring(0, int.Parse(DirectStringInput));
                        case StringTap.Parse:

                            return double.Parse(Regex.Replace(args.ToString(), @"[^\d]", ""));
                            break;
                    }
                    break;
                case ParameterTypes.Double:
                    switch (TDoubleTap)
                    {
                        case DoubleTap.AddOne:
                            return ((double)args) + 1;
                            break;
                        case DoubleTap.SubtractOne:
                            return ((double) args) - 1;
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                    break;
                case ParameterTypes.NodeList:
                    break;
                case ParameterTypes.Node:
                    switch (tNodeTap)
                    {
                        case NodeTap.GetAttribute:
                            string res = "";
                            (args as HtmlNode).GetAttributeValue(DirectStringInput, res);
                            return res;
                            break;
                        case NodeTap.GetInnerText:
                            return (args as HtmlNode).InnerText;
                            break;
                        case NodeTap.GetNextSibling:
                            return (args as HtmlNode).NextSibling;
                            break;
                        case NodeTap.GetPreviousSibling:
                            return (args as HtmlNode).PreviousSibling;
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                    break;
                case ParameterTypes.Boolean:
                    break;
                case ParameterTypes.PassThrough:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            return ParameterTypes.None;
        }

        protected override void StaticTypes(TaskSession ts)
        {
            InputTypes = new List<ParameterTypes>() { ParameterTypes.Any };
            OutputType = ParameterTypes.Any;
            ElemType = typeof (ElemParameterTap);
        }
    }
}
