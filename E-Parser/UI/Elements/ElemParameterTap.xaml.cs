using System;
using System.Windows;
using System.Windows.Controls;
using E_Parser.Logic.ElementLogic;

namespace E_Parser.UI.Elements
{

    public partial class ElemParameterTap : ElemBase
    {
        public NodeTap tNodeTap { get; set; }
        public StringTap tStringTap { get; set; }


        public ElemParameterTap(TaskSession CurrentSession)
        {
            InitializeComponent();
            Task = new TSParameterTap(CurrentSession);
            SwitchOutputType();
        }

        public void SwitchOutputType()
        {
            switch (Task.NextTask.OutputType)
            {
                case TSBase.ParameterTypes.None:
                    break;
                case TSBase.ParameterTypes.Any:
                    break;
                case TSBase.ParameterTypes.String:
                    UIChanger(cmbbxStringTap);
                    break;
                case TSBase.ParameterTypes.Integer:
                    break;
                case TSBase.ParameterTypes.NodeList:
                    break;
                case TSBase.ParameterTypes.Node:
                    UIChanger(cmbbxNodeTap);
                    break;
                case TSBase.ParameterTypes.Boolean:
                    break;
                case TSBase.ParameterTypes.PassThrough:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void UIChanger(ComboBox nodeTap)
        {
            foreach (ComboBox cmb in cmbbxGrid.Children)
            {
                if (cmb != nodeTap)
                {
                    cmb.Visibility = Visibility.Hidden;
                }
                else
                {
                    cmb.Visibility = Visibility.Visible;
                }
            }
        }
        public ElemParameterTap()
        {
            InitializeComponent();
        }
        public ElemParameterTap(TSBase ts)
        {
            InitializeComponent();
            Task = ts;
        }
    }
}
