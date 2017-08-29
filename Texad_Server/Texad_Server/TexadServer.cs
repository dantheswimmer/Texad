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
using System.Windows.Forms;

//This is the texad server .cs file. Testing Git functionality to see if I figured it out yet

namespace Texad_Server
{
    public class TexadServer
    {
        private Form1 myForm;
        public int nextID = 0;
        public List<TexadClient> clients;
        public string password = "TexadServerPassword";

        public TexadClientListener tccl;
        public bool listeningForClients;
        public int serverSeconds = 3545;
        public int serverHours = 5;
        public int serverDays;
        private int clientTimeTruthCheckInterval = 10;

        private TexadWorld world;

        public TexadServer(Form1 f)
        {
            myForm = f;
            clients = new List<TexadClient>();
            tccl = new TexadClientListener(this);
            Application.ApplicationExit += tccl.shutdown;
            Thread t = new Thread(tccl.listen);
            t.Start();
            Thread timeThread = new Thread(updateServerTime);
            timeThread.Start();
            world = new TexadWorld();
        }

        public void updateServerTime()
        {
            while (true)
            {
                Thread.Sleep(1000);
                serverSeconds++;
                if (serverSeconds % clientTimeTruthCheckInterval == 0)
                {
                    foreach (TexadClient c in clients) { sendServerTimeTruthCheck(c); }
                    Console.WriteLine("Sent time truth Check");
                }
                if (serverSeconds >= 3600)
                {
                    serverSeconds = 0;
                    serverHours++;
                    if (serverHours == 19 && serverSeconds == 0)
                        sendAllStoryUpdate("The sun sinks below the horizon and the creatures of the night emerge. It is now nighttime.");
                    if (serverHours == 6 && serverSeconds == 0)
                        sendAllStoryUpdate("The first light of dawn breaks in the distance, and landscape glows with morning. It is now daytime.");
                }
                if (serverHours >= 24)
                {
                    serverHours = 0;
                    serverDays++;
                }
            }
        }

        public void recieveClientMessage(Byte[] message, TexadClient sender)
        {
            Console.Write("Recieved message from client " + sender.clientName + ". ID: " + sender.clientID);
            Byte[] decodedMessage = WebsocketUtility.decodeWebsocketMessage(message);
            Console.WriteLine("The message was: " + Encoding.Default.GetString(decodedMessage));
            parseClientMessage(decodedMessage, sender);
        }

        public void parseClientMessage(Byte[] message, TexadClient sender)
        {
            String msgStr = Encoding.UTF8.GetString(message);
            char token = msgStr[0];
            Console.WriteLine("Token from message was: " + token);
            switch (token)
            {
                case TexadServerEventSystem.ITEM_UPDATE_REQUEST:
                {
                    sendClientItemUpdate(sender);
                    break;
                }
                case TexadServerEventSystem.STAT_UPDATE_REQUEST:
                {
                    sendClientStatUpdate(sender);
                    break;
                }
                case TexadServerEventSystem.POS_UPDATE_REQUEST:
                {
                    sendClientLocationUpdate(sender);
                    break;
                }
                case TexadServerEventSystem.COMMAND_PROCESS_RERQUEST:
                {
                    TexadCommandInterpreter.interperateCommand(msgStr.Remove(0,1), sender);
                    break;
                }
                case TexadServerEventSystem.COMPANION_UPDATE_REQUEST:
                {
                    break;
                }
                case TexadServerEventSystem.CLIENT_LOGIN_INFO:
                {
                    sender.clientName = msgStr.Remove(0, 1);
                    Console.WriteLine("Got username from client");
                    updateFormInfo();
                    break;
                }                      
                default:
                {
                    Console.WriteLine("Recieved unidentified message code: " + token);
                    break;
                }
            }
        }

        public void sendClientStatUpdate(TexadClient c)
        {
            sendClientMessage(Encoding.UTF8.GetBytes(TexadSerializer.serializeStats(c)), c);
        }

        public void sendClientItemUpdate(TexadClient c)
        {
            sendClientMessage(Encoding.UTF8.GetBytes(TexadSerializer.serializeInventory(c)), c);
        }

        public void sendAllStoryUpdate(string update)
        {
            sendClientMessage(Encoding.UTF8.GetBytes("s" + update), clients);
            Console.WriteLine("Send story update to all");
        }

        public void sendStoryUpdateToAllBut(string update, TexadClient excluded)
        {
            foreach(TexadClient tc in clients)
            {
                if (tc != excluded)
                    sendClientStoryUpdate(update, tc);
            }
        }

        public void sendClientStoryUpdate(string update, TexadClient c)
        {
            sendClientMessage(Encoding.UTF8.GetBytes("s"+update), c);
        }

        public void sendClientLocationUpdate(TexadClient c)
        {
            sendClientMessage(Encoding.UTF8.GetBytes("l" + c.currentScene.sceneName + ": " + c.currentSector.sectorName + ": " + c.currentSector.biome.biomeName), c);
        }

        public void sendLocationDescription(TexadClient c)
        {
            sendClientMessage(Encoding.UTF8.GetBytes("s" + c.getLocationDescription()), c);
        }

        public void sendProgressBarUpdate(TexadClient c, int barType, int time)
        {
            sendClientMessage(Encoding.UTF8.GetBytes("g" + barType + "|" + time), c);
        }

        public void sendServerTimeTruthCheck(TexadClient c)
        {
            sendClientMessage(Encoding.UTF8.GetBytes("t" + serverDays + '|' + serverHours + '|' + serverSeconds), c);
        }

        public void sendClientMessage(Byte[] rawMessage, List<TexadClient> targets)
        {
            Byte[] encodedMessage = WebsocketUtility.encodeWebsocketMessage(rawMessage);
            foreach(TexadClient tc in targets)
            {
                tc.tcpClient.GetStream().Write(encodedMessage, 0, encodedMessage.Length);
            }
        }

        public void sendClientMessage(Byte[] rawMessage, TexadClient target)
        {
            Byte[] encodedMessage = WebsocketUtility.encodeWebsocketMessage(rawMessage);
            target.tcpClient.GetStream().Write(encodedMessage, 0, encodedMessage.Length);
        }

        public void listenForClients()
        {
            listeningForClients = true;
        }

        public void stopListeningForClients()
        {
            listeningForClients = false;
        }

        public void addClient(TcpClient c)
        {
            TexadClient tc = new TexadClient(c,this,world);
            assignClientID(tc);
            clients.Add(tc);
            WebsocketUtility.websocketHandshake(c);
            Thread t = new Thread(tc.clientReadCycle);
            t.Start();
            sendClientMessage(Encoding.UTF8.GetBytes("sWelcome to the server!"), tc);
            sendClientMessage(Encoding.UTF8.GetBytes("s" + tc.currentScene.getSceneDescription()), tc);
            updateFormInfo();
        }

        public bool checkClientConnected(TcpClient c)
        {
            IPAddress newAddr = ((IPEndPoint)c.Client.RemoteEndPoint).Address;
            foreach (TexadClient tc in clients)
            {
                IPAddress thisAddr = ((IPEndPoint)tc.tcpClient.Client.RemoteEndPoint).Address;
                if (thisAddr.Equals(newAddr))
                {
                    Console.WriteLine("Client was alreardy connected");
                    return true;
                }
                else
                {
                    Console.WriteLine(thisAddr.ToString() + " was not equal to the address: " + newAddr.ToString());
                }
            }
            return false;
        }

        public void assignClientID(TexadClient client)
        {
            client.clientID = nextID;
            nextID++;
        }

        public void stopServer()
        {
            tccl.shutdown(null,null);
        }

        public void updateFormInfo()
        {
            myForm.Invoke(new MethodInvoker(delegate { myForm.getClientListBox().Items.Clear(); }));
            myForm.Invoke(new MethodInvoker(delegate { foreach (TexadClient tc in clients) { myForm.getClientListBox().Items.Add(tc.clientName + " ID: " + tc.clientID); } }));
        }
    }

    public class TexadClientListener
    {
        public static int CLIENT_LISTEN_PORT = 55777;
        TexadServer myServer;
        private TcpListener clientListener;
        private bool listening;

        public TexadClientListener(TexadServer owningServer)
        {
            myServer = owningServer;
            Console.WriteLine("Opened connection listener at " + WebsocketUtility.getLocalIP().ToString() + " on port " + CLIENT_LISTEN_PORT);
            clientListener = new TcpListener(WebsocketUtility.getLocalIP(),CLIENT_LISTEN_PORT);
            clientListener.Start();
            listening = true;
            Console.WriteLine("Connection Listener created");
        }

        public void listen()
        {
            while(listening)
            {
                if(clientListener.Pending()) //Got a connection request from a client
                {
                    //Check if this client is already connected or not, possibly create a seperate time for connection before actual play begins
                    TcpClient newClient = clientListener.AcceptTcpClient();
                    
                    Console.WriteLine("Listnener had pending...");
                    bool existingClient = myServer.checkClientConnected(newClient);
                    if (false)//existingClient)
                    {
                        Console.WriteLine("Client Connected Already, not accepted");
                    }
                    else
                    {
                        Console.WriteLine("Client Accepted");
                        myServer.addClient(newClient);
                    }
                }
            }
        }

        public void shutdown(object sender, EventArgs e)
        {
            listening = false;
            clientListener.Stop();
        }
    }
}
