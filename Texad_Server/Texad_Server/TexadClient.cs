using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.Text.RegularExpressions;
using System.Security.Cryptography;
using System.Threading;

namespace Texad_Server
{
    public class TexadClient
    {
        public TexadServer myServer;
        public bool clientAlive;
        public int clientID;
        public string clientName = "unnamed client";
        public TcpClient tcpClient;
        private NetworkStream clientStream;
        public bool handshakeDone = false;

        //Player State Data
        public TexadCharacter playerCharacter;
        public List<TexadActionEvent> eventQueue;
        public uint eventStepTime = 0;

        private TexadWorld world;

        public TexadClient(TcpClient c, TexadServer myServer, TexadWorld w)
        {
            playerCharacter = new TexadCharacter(myServer,w.startScene);
            this.myServer = myServer;
            world = w;
            tcpClient = c;
            clientStream = tcpClient.GetStream();
            clientStream.Flush();
            clientAlive = true;
            Thread eventThread = new Thread(stepEventQueue);
            eventThread.Start();
            playerItems = new List<TexadItem>();
            playerStats = new List<TexadStat>();
            availableActions = new List<TexadAction>();
            eventQueue = new List<TexadActionEvent>();

            availableActions.Add(new MoveAction(2000));
            playerItems.Add(new TexadItem("Test Item", 1));
            playerItems.Add(new TexadItem("Test Item2", 2));
            playerItems.Add(new TexadItem("Test Item3", 5));
            playerItems.Add(new TexadItem("Money", 3, new TexadItemAttribute("Quantity", "15")));
            TexadItem bread = new TexadItem("Stale Bread", 4, new TexadItemAttribute("Quantity", "15"));
            TexadAction eatAction = new EatAction(4000, 50);
            eatAction.actionOwner = bread;
            bread.capableOf.Add(eatAction);
            addItem(bread);

            playerStats.Add(new TexadStat("Health",1, 100, 100));
            playerStats.Add(new TexadStat("Hunger",2, 35, 100));
            playerStats.Add(new TexadStat("Fatigue",3, 0));
            playerStats.Add(new TexadStat("Intellegence",4, 55));
            playerStats.Add(new TexadStat("Strength",5, 78));

            currentSector = world.startSector;
            currentScene = world.startScene;
        }

       

        public void stepEventQueue()
        {
            while (true)
            {
                eventStepTime += 10;
                Thread.Sleep(10);
                for (int i = 0; i < eventQueue.Count; i++)
                {
                    TexadActionEvent ae = eventQueue[i];
                    if (ae.doneTime <= eventStepTime)
                    {
                        ae.myAction.doAction(ae.target, ae.source);
                        eventQueue.Remove(ae);
                    }
                }
            }
        }

        public void addToEventQueue(TexadActionEvent ae)
        {
            ae.doneTime = eventStepTime + ae.myAction.actionTime;
            myServer.sendProgressBarUpdate(this, 0, (int)ae.myAction.actionTime);
            eventQueue.Add(ae);
        }

        public void clientReadCycle()
        {
            while(clientAlive)
            {
                while (!clientStream.DataAvailable);
                Byte[] data = new Byte[tcpClient.Available];
                clientStream.Read(data, 0, data.Length);
                myServer.recieveClientMessage(data, this);
            }
        }
    }
}
