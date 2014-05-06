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
            Client = new AwesomiumWrap();
            startingTask = new TSStart(this)
            {
                NextTask = new TSTextInput(this)
                {
                    DirectStringInput = "http://rozetka.com.ua/",
                    NextTask =  new TSLoadUrl(this)
                    {
                        NextTask = new TSTextInput(this)
                        {
                            DirectStringInput = "//li[@class='main-page-m-catalog-subl-i']/a",
                            NextTask = new TSFindNodes(this)
                            {
                                NextTask = new TSEnd(this)
                            }
                        }
                        
                    }
                }
            };
        }

        public void StartSession()
        {
            startingTask.StartTask(null);
        }

        public void EndSession()
        {
            Console.WriteLine("Session has ended");
        }
    }
}
