using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
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

namespace E_Parser.UI.Elements
{
   

    [Serializable]
    public class ElemBase : UserControl
    {
        protected TSBase _task;

        public bool IsRunning
        {
            get
            {
                if (_task != null)
                    return _task.IsRunning;
                else return false;
            }
            set { _task.IsRunning = value; }
        }

        public string Description { get; set; }
        public ElemBase()
        {
        }


        public ElemBase(TaskSession _sess, TSBase task)
        {
            _task = task;
        }

        public virtual void AfterElementAddition()
        {
            
        }
        public virtual void NextElementReaction(ElemBase next)
        {
        }

        public TSBase Task
        {
            get { return _task; }
            set { _task = value; }
        }

        public TaskSession Session
        {
            get { return Task.Session; }
            set { Task.Session = value; }
        }
    }
}