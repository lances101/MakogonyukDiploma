using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Awesomium;
using Awesomium.Core;
using Awesomium.Windows.Controls;
namespace TheE_Parser
{
    class HAPTools
    {
        public AwesomiumBrowserTools displayTools = new AwesomiumBrowserTools();
        HtmlAgilityPack.HtmlNodeCollection _nodes = null;
        public HAPTools(WebControl control)
        {
            displayTools.Client = control;
            displayTools.CanNavigate = false;
        }
        
        public int TestXPath(string html, string xpath)
        {
            try
            {
                HtmlAgilityPack.HtmlDocument hapdoc = new HtmlAgilityPack.HtmlDocument();
                hapdoc.LoadHtml(html);
                _nodes = hapdoc.DocumentNode.SelectNodes("." + xpath.Replace("'", "\"").ToLower());
                return _nodes.Count;
            }
            catch (Exception e)
            {
                Console.WriteLine("XPath error" + xpath + "\n" + e.Message);
            }
            
            return -1;
        }
        public void DisplayXPath(int index)
        {
            if (index == -1)
                index = 0;
            displayTools.Client.LoadHTML("<html><body>" + _nodes[index].OuterHtml +"</body></html>");
        }
        public string NodeOuterHtml(int index)
        {
            if (index == -1)
                index = 0;
            return _nodes[index].OuterHtml;
        }
    }
}
