using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using TheE_Parser;

namespace E_Parser.Logic.ElementLogic
{
    [Serializable]
    public class TaskSession
    {
        private List<TSBase> taskList = new List<TSBase>();

        [NonSerialized()] private AwesomiumWrap client;

        public AwesomiumWrap Client
        {
            get
            {
                if (client == null) client = new AwesomiumWrap();
                return client;
            }
            set { client = value; }
        }

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