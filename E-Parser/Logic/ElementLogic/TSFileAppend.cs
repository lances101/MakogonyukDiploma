using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_Parser.UI.Elements;

namespace E_Parser.Logic.ElementLogic
{
    [Serializable]
    public class TSFileAppend : TSBase
    {
        public TSFileAppend(TaskSession ts) : base(ts)
        {
            
        }

        

        public override string Name
        {
            get { return "File Append"; }
        }

        protected override object _mainTaskMethod(object args)
        {

            if (!Directory.Exists("./Storage/"))
                Directory.CreateDirectory("./Storage/");
            using (StreamWriter sw = File.AppendText("./Storage/" + DirectStringInput + ".txt"))
            {
                sw.WriteLine(DateTime.Now.ToString() + " | " + args.ToString() + "\n");
                
            }
            return null;
        }

        protected override void StaticTypes(TaskSession ts)
        {
            InputTypes = new List<ParameterTypes>() { ParameterTypes.Any };
            OutputType = ParameterTypes.None;
            ElemType = typeof(ElemFileAppend);
        }
    }
}
