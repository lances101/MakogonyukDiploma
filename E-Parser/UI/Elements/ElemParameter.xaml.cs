using E_Parser.Logic.ElementLogic;

namespace E_Parser.UI.Elements
{

    public partial class ElemParameter : ElemBase
    {
        public ElemParameter(TaskSession CurrentSession)
        {
            InitializeComponent();
            Task = new TSParameterTap(CurrentSession);
        }

        public ElemParameter()
        {
            InitializeComponent();
        }

    }
}
