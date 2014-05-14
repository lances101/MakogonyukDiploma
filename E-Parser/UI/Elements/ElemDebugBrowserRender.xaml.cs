using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using E_Parser.Logic.ElementLogic;
using E_Parser.UI.Elements;

namespace E_Parser.UI.Elements
{

    public partial class ElemDebugBrowserRender : ElemBase
    {
        public ElemDebugBrowserRender()
        {
        }

        public ElemDebugBrowserRender(TaskSession CurrentSession)
        {
            InitializeComponent();
            Task = new TSDebugRenderBrowser(CurrentSession);
        }

        public ElemDebugBrowserRender(TSBase ts)
        {
            InitializeComponent();
            Task = ts;
        }

    }
}
