﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Parser.Logic.ElementLogic
{
    public enum IfCompareType
    {
        [Description("==")]
        Equals,
        [Description("!=")]
        NotEquals,
        [Description(">")]
        Bigger,
        [Description(">=")]
        BiggerOrEqual,
        [Description("<")]
        Smaller,
        [Description("<=")]
        SmallerOrEqual,
        [Description("is null")]
        Null,
        [Description("not null")]
        NotNull
    }
    public class TSIf : TSBase
    {
        
        public TSIf(TaskSession ts) : base(ts)
        {
            
        }

        public override string GetName
        {
            get { return "If Comparer"; }
        }

        protected override object _mainTaskMethod(object[] args)
        {
            return null;
        }
    }
}