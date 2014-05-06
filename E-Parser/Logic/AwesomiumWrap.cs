using System.Diagnostics;
using System.IO;
using System.Windows.Threading;
using Awesomium.Core;
//using Awesomium.Windows.Controls;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_Parser.Logic;

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
                    Console.WriteLine("[JS Callback] Error: " + e.Message);
                }
                return "CALLBACK WITH NO ARGUMENTS";
            }
            

        }
    }
    public class AwesomiumWrap
    {
        public AwesomiumWrap()
        {
            _view = WebCore.CreateWebView(1024, 640);
            _view.DocumentReady += ViewDocumentReady;
            HAP = new HAPWrap(_view);
        }
        public event JSCallbackHandler Callback;
        public HAPWrap HAP;
        WebView _view;

        public WebView View
        {
            get { return _view; }
            set
            {
                _view = value;
                _view.DocumentReady += ViewDocumentReady;
            }
        }


        public bool LoadUrl(string url)
        {
            if (View.InvokeRequired)
                return (bool)InvokeWithoutArguments(new Func<bool>(() => LoadUrl(url)));
            try
            {
                var uri = new Uri(url);
                View.Source = uri;
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("[URL Validator] " + e.Message);
            }
            return false;
        }

        public bool IsLoading()
        {
            if (View.InvokeRequired)
            {
                return (bool)InvokeWithoutArguments(new Func<bool>(IsLoading));
            }
            return View.IsLoading;
        }
        public void RenderToPng()
        {
            if (View.InvokeRequired)
            {
                InvokeWithoutArguments(new Action(RenderToPng));
                return;
            }
            Console.WriteLine("SAVING " + ((BitmapSurface) View.Surface).SaveToPNG(@"F:\Debug\debug.png"));
            Process.Start(@"F:\Debug\debug.png");
        }
        public bool ClickElement(string xpath)
        {
            JSValue val = _view.ExecuteJavascriptWithResult("ClickElement(" + xpath + ")");
            if (val.IsBoolean)
            {
                if (val == true)
                    return true;
            }
            return false;
        }
        public bool InputValue(string xpath, string value)
        {
            JSValue val = _view.ExecuteJavascriptWithResult("InjectValue(" + xpath + ", "+ value + ")");
            if (val.IsBoolean)
            {
                if (val == true)
                    return true;
            }
            return false;
        }

        private object InvokeWithoutArguments(Delegate method)
        {
            return View.Invoke(method, null);
        }
        private void ViewDocumentReady(object sender, Awesomium.Core.UrlEventArgs e)
        {
            InjectJQuery();
            BindMethods();
            HAP.UpdateDocument(HtmlSource);
        }
        private void InjectJQuery()
        {
           _view.ExecuteJavascript(E_Parser.Properties.Resources.jquery_1_11_1_min);
        }
        private void XPathPickerInject()
        {
            _view.ExecuteJavascript(E_Parser.Properties.Resources.JSInjection);
        }
        private string HtmlSource
        {
            get
            {
                JSValue result = _view.ExecuteJavascriptWithResult("document.documentElement.outerHTML;");
                if (result.IsString)
                {
                    return result;
                }
                return "";
            }
        }
        void BindMethods()
        {
            JSObject jsobject = _view.CreateGlobalJavascriptObject("awesomeCallBackObject");
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
                        Callback(_view, new JSCallbackArgs(args.MethodName, result));
                    }
                }
            }
        }
    }
}
    
