using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Texad_Server
{
    public partial class Form1 : Form
    {
        public TexadServer server;

        public Form1()
        {
            InitializeComponent();
            listenForClientsButton.Visible = false;
        }

        private void startServerButton_Clicked(object sender, EventArgs e)
        {
            if (server == null) //Server needs to start
            {
                server = new TexadServer(this);
                statusLabel.Text = "Server created";
                connectedClientsLabel.Text = server.clients.Count + " clients connected";
                startServerButton.Text = "Stop Server";
                listenForClientsButton.Visible = true;
            }
            else //Server needs to die
            {
                server.stopServer();
                server = null;
                statusLabel.Text = "Server has been stopped";
                startServerButton.Text = "Start Server";
                connectedClientsLabel.Text = "No Server Running";
                listenForClientsButton.Visible = false;
            }
        }

        private void listenForClientsButtonClicked(object sender, EventArgs e)
        {
            if(server.listeningForClients)
            {
                listenForClientsButton.Text = "Listen For Clients";
                server.stopListeningForClients();
            }
            else
            {
                listenForClientsButton.Text = "Stop Listening";
                server.listenForClients();
            }
        }

        public Label getConnectedClientsLabel() { return connectedClientsLabel; }
        public ListBox getClientListBox() { return clientListBox; }
    }
}
