using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Timers;

namespace RobotControl
{
    public partial class Form1 : Form
    {
        private Socket _serverSocket, _clientSocket;
        private byte[] _buffer;
        private bool _connected;
        System.Timers.Timer _timer;


        public Form1()
        {
            InitializeComponent();
            _connected = false;
            
            _timer = new System.Timers.Timer();
            _timer.Elapsed += new System.Timers.ElapsedEventHandler(OnTimedEvent);
            _timer.Interval = 1000;
            StartServer();
            
        }

        private void AppendToTextbox(string text)
        {
            MethodInvoker invoker = new MethodInvoker(delegate
            {
                textBox1.Text = textBox1.Text + text + "\r\n";
            });
            this.Invoke(invoker);
        }

        private void AppendToClientsTextbox(string text)
        {
            MethodInvoker invoker = new MethodInvoker(delegate
            {
                txtClients.Text = txtClients.Text + text + "\r\n";
            });
            this.Invoke(invoker);
        }

        private  void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            string text = "*";
            //AppendToTextbox(text);
            Send(text);
        }

        private void StartServer()
        {

            try
            {
                _serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                _serverSocket.Bind(new IPEndPoint(IPAddress.Any, 50001));
                _serverSocket.Listen(0);
                _serverSocket.BeginAccept(new AsyncCallback(AcceptCallback), null);
            }
            catch (Exception ex)
            {
                MessageBox.Show("[StartServer]" + ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            _timer.Enabled = true;
        }

        private void AcceptCallback(IAsyncResult AR)
        {
            try
            {
                _clientSocket = _serverSocket.EndAccept(AR);
                var remoteEndPoint = _clientSocket.RemoteEndPoint as IPEndPoint;
                var remoteAddress = remoteEndPoint.Address;
                var remotePort = remoteEndPoint.Port;
                AppendToClientsTextbox(remoteAddress.ToString() + " connected");
                _buffer = new byte[_clientSocket.ReceiveBufferSize];
                _clientSocket.BeginReceive(_buffer, 0, _buffer.Length, SocketFlags.None, new AsyncCallback(ReceiveCallback), null);
            }
            catch (Exception ex)
            {
                MessageBox.Show("[AcceptCallback]" + ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Send(string send)
        {
            byte[] send_buf;
            //send += "\r\n";
            send_buf = Encoding.ASCII.GetBytes(send);

            if (_clientSocket != null)
            {
                try
                {
                    _clientSocket.Send(send_buf, SocketFlags.None);
                }

                catch (SocketException ex)
                {
                    AppendToTextbox("socket error");
                }
            }
            
            
        }

        private void ReceiveCallback(IAsyncResult AR)
        {
            try
            {
                int received = _clientSocket.EndReceive(AR);
                var remoteEndPoint = _clientSocket.RemoteEndPoint as IPEndPoint;
                var remoteAddress = remoteEndPoint.Address;
                var remotePort = remoteEndPoint.Port;
                Array.Resize(ref _buffer, received);
                string text = "IP address: " + remoteAddress.ToString() + ' ' + Encoding.ASCII.GetString(_buffer);
                AppendToTextbox(text);
                Array.Resize(ref _buffer, _clientSocket.ReceiveBufferSize);
                _clientSocket.BeginReceive(_buffer, 0, _buffer.Length, SocketFlags.None, new AsyncCallback(ReceiveCallback), null);
            }
            catch (System.Exception ex)
            {

                //MessageBox.Show("[ReceiveCallback]" + ex.Message + " code ", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                _clientSocket.Close();
                StartServer();
            }
        }



        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.F1)
            {
                button1.PerformClick();
                return true;
            }

            if (keyData == Keys.F2)
            {
                button2.PerformClick();
                return true;
            }

            if (keyData == Keys.F3)
            {
                button3.PerformClick();
                return true;
            }

            if (keyData == Keys.F4)
            {
                button4.PerformClick();
                return true;
            }

            if (keyData == Keys.F5)
            {
                button5.PerformClick();
                return true;
            }

            if (keyData == Keys.F6)
            {
                button6.PerformClick();
                return true;
            }

            if (keyData == Keys.F7)
            {
                button7.PerformClick();
                return true;
            }

            if (keyData == Keys.F8)
            {
                button8.PerformClick();
                return true;
            }

            if (keyData == Keys.F9)
            {
                button9.PerformClick();
                return true;
            }

            if (keyData == Keys.F10)
            {
                button10.PerformClick();
                return true;
            }

            if (keyData == Keys.F11)
            {
                button11.PerformClick();
                return true;
            }

            if (keyData == Keys.F12)
            {
                button12.PerformClick();
                return true;
            }

            if (keyData == Keys.F13)
            {
                button13.PerformClick();
                return true;
            }

            if (keyData == Keys.F14)
            {
                button14.PerformClick();
                return true;
            }

            if (keyData == Keys.F15)
            {
                button15.PerformClick();
                return true;
            }

            if (keyData == Keys.F16)
            {
                button16.PerformClick();
                return true;
            }

            if (keyData == Keys.F17)
            {
                button17.PerformClick();
                return true;
            }

            if (keyData == Keys.F18)
            {
                button18.PerformClick();
                return true;
            }

            if (keyData == Keys.F19)
            {
                button19.PerformClick();
                return true;
            }

            if (keyData == Keys.F20)
            {
                button20.PerformClick();
                return true;
            }

            if (keyData == Keys.Up)
            {
                btnForwards.PerformClick();
                return true;
            }

            if (keyData == Keys.Down)
            {
                btnBackwards.PerformClick();
                return true;
            }

            if (keyData == Keys.Left)
            {
                btnLeft.PerformClick();
                return true;
            }

            if (keyData == Keys.Right)
            {
                btnRight.PerformClick();
                return true;
            }

            if (keyData == Keys.Escape)
            {
                btnStop.PerformClick();
                return true;
            }

            if (keyData == Keys.Space)
            {
                btnStop.PerformClick();
                return true;
            }

            if (keyData == Keys.D1)
            {
                Send(":SLOW");
                return true;
            }

            if (keyData == Keys.D2)
            {
                Send(":MEDIUM");
                return true;
            }

            if (keyData == Keys.D3)
            {
                Send(":FAST");
                return true;
            }


            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Send("turn_to_180");
            //MessageBox.Show("F1");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Send("turn_to_090");
            //MessageBox.Show("F2");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Send("turn_to_000");
            //MessageBox.Show("F3");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("F4");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Send("arm_down");
            //MessageBox.Show("F5");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Send("arm_up");
            //MessageBox.Show("F6");
        }

        private void button7_Click(object sender, EventArgs e)
        { 
           //MessageBox.Show("F7");
        }

        private void button8_Click(object sender, EventArgs e)
        {
            MessageBox.Show("F8");
        }

        private void button9_Click(object sender, EventArgs e)
        {
            Send("gripper_close");
            //MessageBox.Show("F9");
        }

        private void button10_Click(object sender, EventArgs e)
        {
            Send("gripper_open");
            //MessageBox.Show("F10");
        }

        private void button11_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("F11");
        }

        private void button12_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("F12");
        }

        private void button13_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("F13");
        }

        private void button14_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("F14");
        }

        private void button15_Click(object sender, EventArgs e)
        {
            MessageBox.Show("F15");
        }

        private void button16_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("F16");
        }

        private void button17_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("F17");
        }

        private void button18_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("F18");
        }

        private void button19_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("F19");
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            Send(txtSend.Text);
        }

        private void btnForwards_Click(object sender, EventArgs e)
        {
            Send(":FORWARDS");
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            Send(":STOP");
        }

        private void btnBackwards_Click(object sender, EventArgs e)
        {
            Send(":BACKWARDS");
        }

        private void btnLeft_Click(object sender, EventArgs e)
        {
            Send(":TURNLEFT");
        }

        private void btnRight_Click(object sender, EventArgs e)
        {
            Send(":TURNRIGHT");
        }

        private void button21_Click(object sender, EventArgs e)
        {
            //axvlcplugin = "http://192.168.86.4:8081";
            //axVLCPlugin21.BaseURL=("http://192.168.86.4:8081");
            axVLCPlugin21.playlist.add("http://192.168.86.4:8081");
            axVLCPlugin21.playlist.play();
            axVLCPlugin21.AutoPlay = true;
        }

        private void button20_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("F20");
        }


    }

    
}
