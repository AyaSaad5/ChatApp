using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChatApp
{
    public partial class Form1 : Form
    {
        IHubProxy prox;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            prox.Invoke("sendMessage", textBox1.Text, textBox2.Text);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //define connection
            HubConnection con = new HubConnection("https://localhost:44328/");

            //create proxy
            prox = con.CreateHubProxy("chat");

            //start connection
            con.Start();

            //  method for client
            //prox.On<data>("newMessage", (m) => mssgs.Invoke( new Action(() => mssgs.Items.Add(m.name + ":" + m.message))));
            prox.On<string, string>("newMessage", (n, m) => mssgs.Invoke(new Action(() => mssgs.Items.Add(n + ":" + m))));

            prox.On<string, string>("newMember", (n, g) => mssgs.Invoke(new Action(() => mssgs.Items.Add(n + " joined " + g))));

            prox.On<string, string, string>("groupmsg", (n, g, m) => mssgs.Invoke(new Action(() => mssgs.Items.Add(n + " from " + g + ": "+ m))));


        }

        private void join_Click(object sender, EventArgs e)
        {
            prox.Invoke("joinGroup", textBox4.Text, textBox1.Text);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            prox.Invoke("sendToGroup", textBox4.Text, textBox1.Text, textBox2.Text);
        }
    }
}
