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
        public NetworkStream clientStream;
        public bool handshakeDone = false;

        //Player State Data
        public TexadPlayerCharacter playerCharacter;
        public TexadCommandInterpreter cmdIntp;
        public List<TexadActionEvent> eventQueue;
        public uint eventStepTime = 0;

        private TexadWorld world;

        public TexadClient(TcpClient c, TexadServer myServer, TexadWorld w)
        {
            this.myServer = myServer;
            world = w;
            tcpClient = c;
            clientStream = tcpClient.GetStream();
            clientStream.Flush();
            clientAlive = true;
            Thread eventThread = new Thread(stepEventQueue);
            eventThread.Start();
            eventQueue = new List<TexadActionEvent>();
            playerCharacter = new TexadPlayerCharacter(this, myServer, w.startScene);
            cmdIntp = new TexadCommandInterpreter(this, playerCharacter.actionManager);
        }

        public void interperateCommand(string cmd)
        {
            cmdIntp.interperateCommand(cmd);
        }

        public string getStatUpdateString()
        {
            string str = playerCharacter.statManager.serializeStats();
            Console.WriteLine("Stat string sent: " + str);
            return str;
        }

        public string getItemUpdateString()
        {
            return playerCharacter.inventoryManager.serializeInventory();
        }

        public string getLocationUpdateString()
        {
            return playerCharacter.locationManager.getPositionString();
        }

        public string getLocationDescriptionString()
        {
            return playerCharacter.locationManager.getLocationDescription();
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
