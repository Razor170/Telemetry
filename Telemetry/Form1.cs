using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Collections.Generic;

namespace Telemetry
{
    public partial class Form1 : Form
    {

        //Constants
        private const int PORT = 20777;             // Port 20777
        private const string IP = "127.0.0.1";      // Localhost
        private const int INTERVAL_MS = 100;        // Refresh every 0,1 sec
        // Connection IP
        private IPEndPoint remoteIP;
        // Who sent the data
        private IPEndPoint senderIP = new IPEndPoint(IPAddress.Any, 0);
        // UDP Socket for connection
        private UdpClient udpSocket;
        // Thread for capturing
        private Thread CaptureThread = null;
        // Holds the latest Data
        TelemetryPacket latestData;
        //Mutex Protection
        static object syncObj = new object();

        

        public Form1()
        {
            InitializeComponent();

            remoteIP = new IPEndPoint(IPAddress.Parse(IP), PORT);

            try
            {
                udpSocket = new UdpClient();
                udpSocket.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
                udpSocket.ExclusiveAddressUse = false;
                udpSocket.Client.Bind(remoteIP);
                StartListening();
            }
            catch (Exception error)
            {
                //writeLog(error.ToString());
                //Placeholder
            }
        }

        // Connect to the Game
        private void StartListening()
        {
            if (CaptureThread == null)
            {
                //Start Thread for collecting data
                CaptureThread = new Thread(FetchData);
                CaptureThread.Start();

                timer1.Interval = INTERVAL_MS;
                timer1.Enabled = true;
                timer1.Start();
            }
        }

        private void FetchData()
        {
            while (true)
            {
                // Get the data
                Byte[] receiveBytes = udpSocket.Receive(ref senderIP);

                // Lock access
                Monitor.Enter(syncObj);

                // Convert data to struct
                latestData = PacketUtilities.ConvertToPacket(receiveBytes);
               

                // Release the lock
                Monitor.Exit(syncObj);
            }
        }

        private void UpdateDisplay(Object myObject, EventArgs myEventArgs)
        {
            timer1.Stop();
            Monitor.Enter(syncObj);
            label2.Text = latestData.Throttle.ToString();
            label4.Text = latestData.Brake.ToString();
            switch (latestData.Gear)
            {
                case 0: label6.Text = "R"; break;
                case 1: label6.Text = "N"; break;
                case 2: label6.Text = "1"; break;
                case 3: label6.Text = "2"; break;
                case 4: label6.Text = "3"; break;
                case 5: label6.Text = "4"; break;
                case 6: label6.Text = "5"; break;
                case 7: label6.Text = "6"; break;
                case 8: label6.Text = "7"; break;
                case 9: label6.Text = "8"; break;
            }
            Monitor.Exit(syncObj);
            timer1.Start();
        }
    }
}
