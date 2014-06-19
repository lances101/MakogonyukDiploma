using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Awesomium.Core;
using E_Parser.UI;
using HtmlAgilityPack;
namespace E_Parser.Logic
{
    public class HAPWrap
    {
        private WebView View;
        private HtmlDocument Document = new HtmlDocument();
        public HAPWrap(WebView _view)
        {
            View = _view;
        }
        public void UpdateDocument(string html)
        {
            Document.LoadHtml(html);
        }

        public HtmlNodeCollection GetMultipleElements(string xpath)
        {
            try
            {
                return Document.DocumentNode.SelectNodes(xpath);
                
            }
            catch (Exception e)
            {
                SessionEditor.Log(LogType.Error, "XPath Error : {0}" + e.Message);
            }
            return null;
        }
        public HtmlNode GetSingleElement(string xpath)
        {
            try
            {
                return Document.DocumentNode.SelectSingleNode(xpath);
            }
            catch (Exception e)
            {
                SessionEditor.Log(LogType.Error, "XPath Error : {0}" + e.Message);
            }
            return null;
        }
    }
}
