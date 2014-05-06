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
        List<TSBase> taskList = new List<TSBase>();
        public AwesomiumWrap Client { get; set; }
        
        public List<TSBase> TaskList
        {
            get { return taskList; }
            set { taskList = value; }
        }

        public TaskSession()
        {
            Client = new AwesomiumWrap();
        }

        public void StartSession()
        {
            taskList.ElementAt(0).StartTask(null);
        }

        public void EndSession()
        {
            Console.WriteLine("Session has ended");
        }
    }
}
