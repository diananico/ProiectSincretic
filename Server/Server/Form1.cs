using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using TCPLibrary;
namespace Server
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            textBox2.Text = "13000";
            comboBox1.SelectedIndex = 0;
        }
        int option;

        BlockingCollection<Int16> last_mag = new BlockingCollection<Int16>();
        Thread main_thread = null;
        private void client_handler(TcpClient client)
        {
            byte[] buffer_send = new byte[4];
            byte[] buffer_receive = new byte[4];
            int i;
            
            while ((i = TCP_API.receive(client, ref buffer_receive)) != 0)
            {
                Console.WriteLine(i);
                Console.WriteLine("Received: {0} {1} {2} {3}", buffer_receive[0], buffer_receive[1], buffer_receive[2], buffer_receive[3]);

                //LOGIC GOES HERE
                if (buffer_receive[0] == 255 && buffer_receive[1] == 255 && buffer_receive[2] == 255 && buffer_receive[3] == 255)
                {
                    Int16 tmp = last_mag.Take();
                    buffer_send = BitConverter.GetBytes(tmp);
                    last_mag.Add(tmp);
                }
                else
                {
                    TCP_VAR var1 = new TCP_VAR(buffer_receive);
                    buffer_send = var1.Calculate_Results(option);
                    while (last_mag.Count > 0)
                    {
                        last_mag.Take();
                    }
                    last_mag.Add(BitConverter.ToInt16(buffer_send, 0));
                }
                TCP_API.send(client, buffer_send);

            }
            Console.WriteLine(i);
            client.Close();
        }

        public void server_thread(String ip,int port)
        {
            TcpListener server = null;
            try
            {
                IPAddress localAddr = IPAddress.Parse(ip);
                server = new TcpListener(localAddr, port);
                server.Start();
                while (true)
                {
                    Console.WriteLine("Waiting for a connection... ");
                    
                    TcpClient client = server.AcceptTcpClient();
                    Console.WriteLine("Connected!");

                    new Thread(delegate () { client_handler(client); }).Start();


                }
            }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException: {0}", e);
            }
            finally
            {
               server.Stop();
            }

            

        }

        private void button1_Click(object sender, EventArgs earg)
        {
            string IP = Dns.GetHostByName(Dns.GetHostName()).AddressList[0].ToString();
            textBox1.Text = IP;
            int port = Int32.Parse(textBox2.Text);
            main_thread = new Thread(delegate () { server_thread(IP, port); });
            main_thread.IsBackground = true;
            main_thread.Start();
            button1.Enabled = false;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            option = comboBox1.SelectedIndex+1;
            Console.WriteLine(option);
        }
    }
}
