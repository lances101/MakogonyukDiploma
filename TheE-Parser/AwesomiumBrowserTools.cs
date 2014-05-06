using Awesomium.Core;
using Awesomium.Windows.Controls;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheE_Parser
{
    public delegate void JSCallbackHandler(object source, JSCallbackArgs e);
    public class JSCallbackArgs : EventArgs
    {
    	private string _methodName;
        private string[] _args;
    	public JSCallbackArgs(string methodname, params string[] args)
    	{
    		_methodName = methodname;
            _args = args;
    	}
        public string Method
        {
            get { return _methodName; }
        }
        public string[] Arguments
        {
            get { return _args; }
        }
        public string TryGetResult
        {
            get
            {
                try
                {
                    return _args[0];
                }
                catch (Exception e)
                {

                }
                return "CALLBACK WITH NO ARGUMENTS";
            }
            

        }
    }
    class AwesomiumBrowserTools
    {
        WebControl wc; bool _canNavigate = true;

        public bool CanNavigate
        {
            get { return _canNavigate; }
            set { _canNavigate = value; }
        }


        public WebControl Client
        {
            get { return wc; }
            set
            {
                wc = value;
                wc.ContextMenu = new System.Windows.Controls.ContextMenu();
                wc.DocumentReady += wc_DocumentReady;
                
            }
        }
        void wc_DocumentReady(object sender, Awesomium.Core.UrlEventArgs e)
        {
            XPathPickerInject();
            BindMethods();
        }
        public bool XPathPickerInject()
        {
            wc.ExecuteJavascript(Properties.Resources.JS_ExtractXPath);
            return true;
        }
        public void ParentXPath()
        {
           wc.ExecuteJavascript("getXPathParent()");     
        }
        public string OuterHtml
        {
            get
            {
                JSValue result = wc.ExecuteJavascriptWithResult("document.documentElement.outerHTML;");
                if (result.IsString)
                {
                    return result;
                }
                return "";
            }
        }

        public event JSCallbackHandler Callback;
        void BindMethods()
        {
            JSObject jsobject = wc.CreateGlobalJavascriptObject("jsobject");
            jsobject.Bind("callNETReturnXpath", false, JSHandler);
        }
        void JSHandler(object sender, JavascriptMethodEventArgs args)
        {
            if (args.MethodName == "callNETReturnXpath")
            {
                JSValue result = args.Arguments[0];
                if (result.IsString)
                {
                    if (Callback != null)
                    {
                        Callback(wc, new JSCallbackArgs(args.MethodName, result));
                    }
                }
            }
        }

    
    }
}
    
