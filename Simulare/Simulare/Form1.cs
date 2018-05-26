using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TCPLibrary;

namespace Simulare
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
             this.FormClosing += Form1_FormClosing;
             Timer timer = new Timer();
             timer.Interval = 100;
             timer.Tick += timer_Tick;
           
             timer.Start();

        }

        void timer_Tick(object sender, EventArgs e)
        {
            if (alarmState == 0)
            {
                label1.Visible = false;
                count = 0;
                button1.Enabled = true;
                button2.Enabled = true;
                button3.Enabled = true;
                button4.Enabled = true;
                button5.Enabled = true;
                button6.Enabled = true;
                checkBox1.Enabled = true;
                checkBox2.Enabled = true;
                checkBox3.Enabled = true;
            }
            else
            {
                label1.Visible = true;
                count++;
                if(count==50)
                    alarmState = 0;
                button1.Enabled = false;
                button2.Enabled = false;
                button3.Enabled = false;
                button4.Enabled = false;
                button5.Enabled = false;
                button6.Enabled = false;
                checkBox1.Enabled = false;
                checkBox2.Enabled = false;
                checkBox3.Enabled = false;

            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            client.Close();
        }


        int alarmState = 0;
        int count = 0;
        byte[] buffer_send = new byte[4];
        byte[] buffer_receive = new byte[4];
        TcpClient client = null;

        public byte set_bit(byte b, int position)
        {

            return b |= (byte)(1 << position);
        }

        public byte clear_bit(byte b, int position)
        {

            return b &= ((byte)~(1 << position));
        }
        public bool get_bit(byte b, int position)
        {
            if (((b >> position) & 1)==1)
                return true;
            else
                return false;
        }

       
        public void SendToServer()
        {
            TCP_API.send(client, buffer_send);
        }

        public void ReceiveFromServer()
        {
            TCP_API.receive(client, ref buffer_receive);
            Console.WriteLine(buffer_receive[0]);
            Console.WriteLine(get_bit(buffer_receive[0], 6));
            
            Console.WriteLine(get_bit(buffer_receive[0], 5));
            
            Console.WriteLine(get_bit(buffer_receive[0], 4));
            
            Console.WriteLine(get_bit(buffer_receive[0], 3));
            if (get_bit(buffer_receive[0], 6))
                textBox1.BackColor = Color.Green;
            else
                textBox1.BackColor = Color.Red;

            if (get_bit(buffer_receive[0], 5))
                textBox2.BackColor = Color.Green;
            else
                textBox2.BackColor = Color.Red;

            if (get_bit(buffer_receive[0], 4))
                textBox3.BackColor = Color.Green;
            else
                textBox3.BackColor = Color.Red;

            if (get_bit(buffer_receive[0], 3))
                textBox4.BackColor = Color.Green;
            else
                textBox4.BackColor = Color.Red;


            if (get_bit(buffer_receive[0], 7))
            {
                alarmState = 1;
            }
        }

        

        private void button7_Click(object sender, EventArgs e)
        {
            button1.Enabled = true;
            button2.Enabled = true;
            button3.Enabled = true;
            button4.Enabled = true;
            button5.Enabled = true;
            button6.Enabled = true;
            textBox1.Enabled = true;
            textBox2.Enabled = true;
            textBox3.Enabled = true;
            textBox4.Enabled = true;
            textBox1.BackColor = Color.Red;
            textBox2.BackColor = Color.Red;
            textBox3.BackColor = Color.Red;
            textBox4.BackColor = Color.Red;
            checkBox1.Enabled = true;
            checkBox2.Enabled = true;
            checkBox3.Enabled = true;
            button2.Text = "S1- OFF";
            button3.Text = "S2- OFF";
            button4.Text = "S3- OFF";
            button5.Text = "S4- OFF";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            buffer_send[0] = set_bit(buffer_send[0], 7);
            SendToServer();
            ReceiveFromServer();
            buffer_send[0] = clear_bit(buffer_send[0], 7);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (button2.Text == "S1- OFF")
            {
                button2.Text = "S1- ON";
                buffer_send[0] = set_bit(buffer_send[0], 6);
            }
            else if (button2.Text == "S1- ON")
            {
                button2.Text = "S1- OFF";
                buffer_send[0] = clear_bit(buffer_send[0], 6);
            }
            SendToServer();
            ReceiveFromServer();
        }

        private void button3_Click(object sender, EventArgs e)
        {

            if (button3.Text == "S2- OFF")
            {
                button3.Text = "S2- ON";
                buffer_send[0] = set_bit(buffer_send[0], 5);
            }
            else if (button3.Text == "S2- ON")
            {
                button3.Text = "S2- OFF";
                buffer_send[0] = clear_bit(buffer_send[0], 5);
            }
            SendToServer();
            ReceiveFromServer();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (button4.Text == "S3- OFF")
            {
                button4.Text = "S3- ON";
                buffer_send[0] = set_bit(buffer_send[0], 4);
            }
            else if (button4.Text == "S3- ON")
            {
                button4.Text = "S3- OFF";
                buffer_send[0] = clear_bit(buffer_send[0], 4);
            }
            SendToServer();
            ReceiveFromServer();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (button5.Text == "S4- OFF")
            {
                button5.Text = "S4- ON";
                buffer_send[0] = set_bit(buffer_send[0], 3);
            }
            else if (button5.Text == "S4- ON")
            {
                button5.Text = "S4- OFF";
                buffer_send[0] = clear_bit(buffer_send[0], 3);
            }
            SendToServer();
            ReceiveFromServer();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            
            buffer_send[0] = set_bit(buffer_send[0], 2);
            SendToServer();
            ReceiveFromServer();
            buffer_send[0] = clear_bit(buffer_send[0], 2);
        }

        
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox1.Checked==true)
                buffer_send[0] = set_bit(buffer_send[0], 1);
            else
                buffer_send[0] = clear_bit(buffer_send[0], 1);
            SendToServer();
            ReceiveFromServer();
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked == true)
                buffer_send[0] = set_bit(buffer_send[0], 0);
            else
                buffer_send[0] = clear_bit(buffer_send[0], 0);
            SendToServer();
            ReceiveFromServer();
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked == true)
                buffer_send[1] = set_bit(buffer_send[1], 7);
            else
                buffer_send[1] = clear_bit(buffer_send[1], 7);
            SendToServer();
            ReceiveFromServer();
        }

        private void button8_Click(object sender, EventArgs e1)
        {
            String server = textBox5.Text;
            int port = Int32.Parse(textBox6.Text);
            try
            {
                // Create a TcpClient.
                // Note, for this client to work you need to have a TcpServer 
                // connected to the same address as specified by the server, port
                // combination.

                client = new TcpClient(server, port);



                // Close everything.

            }
            catch (ArgumentNullException e)
            {
                Console.WriteLine("ArgumentNullException: {0}", e);
            }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException: {0}", e);
            }
            button8.Enabled = false;

        }

        
    }
}
