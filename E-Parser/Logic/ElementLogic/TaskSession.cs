using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheE_Parser;

namespace E_Parser.Logic.ElementLogic
{
    public class TaskSession
    {
        List<BaseTaskSequence> taskList = new List<BaseTaskSequence>();
        public AwesomiumWrap Client { get; set; }
        private BaseTaskSequence startingTask;


        public TaskSession()
        {
            startingTask = new TSStart(this);
            startingTask.NextTask = new TSLoadUrl(this);

        }

        public void StartSession()
        {
            startingTask.StartTask(null);
        }

    }
}
