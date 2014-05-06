using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Parser.Logic.ElementLogic
{
    class TSWhile : TSBase
    {
        public TSWhile(TaskSession ts) : base(ts)
        {
            
        }

        public override string GetName
        {
            get { return "While Loop"; }
        }

        protected override object _mainTaskMethod(object[] args)
        {
            return null;
        }
    }
}
