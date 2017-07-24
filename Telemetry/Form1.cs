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
        public TelemetryPacket latestData;
        //Mutex Protection
        static object syncObj = new object();

        private TelemetryManager manager;

        

        public Form1()
        {
            InitializeComponent();

            manager = new TelemetryManager();

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
            update_raceinfo();
            //label2.Text = latestData.DRS.ToString();
            //label4.Text = latestData.DRS_Allowed.ToString();
            //UpdateThrottleBreak(latestData.Throttle, latestData.Brake);
            //label6.Text = manager.CurrentGear(latestData.Gear);
            Monitor.Exit(syncObj);
            timer1.Start();
        }
        /*
        private void UpdateThrottleBreak(float throttle, float brake)
        {
            const float MaxHeight = 75;

            BrakeBar.Height = (int)(MaxHeight * brake);
            ThrottleBar.Height = (int)(MaxHeight * throttle);

            BrakeBar.Location = new Point(732, 66 - BrakeBar.Height);
            ThrottleBar.Location = new Point(666, 66 - ThrottleBar.Height);
        }
        */

        private void update_raceinfo()
        {
            time.Text = latestData.Time.ToString();
            laptime.Text = latestData.LapTime.ToString();
            lapdistance.Text = latestData.LapTime.ToString();
            totaldistance.Text = latestData.TotalDistance.ToString();
            x.Text = latestData.X.ToString();
            y.Text = latestData.Y.ToString();
            z.Text = latestData.Z.ToString();
            speed.Text = latestData.Speed.ToString();
            vx.Text = latestData.VX.ToString();
            vy.Text = latestData.VY.ToString();
            vz.Text = latestData.VZ.ToString();
            rdx.Text = latestData.RDX.ToString();
            rdy.Text = latestData.RDY.ToString();
            rdz.Text = latestData.RDZ.ToString();
            fdx.Text = latestData.FDX.ToString();
            fdy.Text = latestData.FDY.ToString();
            fdz.Text = latestData.FDZ.ToString();
            susp_pos_rl.Text = latestData.susp_pos_rl.ToString();
            susp_pos_rr.Text = latestData.susp_pos_rr.ToString();
            susp_pos_fl.Text = latestData.susp_pos_fl.ToString();
            susp_pos_fr.Text = latestData.susp_pos_fr.ToString();
            susp_vel_rl.Text = latestData.susp_vel_rl.ToString();
            susp_vel_rr.Text = latestData.susp_vel_rr.ToString();
            susp_vel_fl.Text = latestData.susp_vel_fl.ToString();
            susp_vel_fr.Text = latestData.susp_vel_fr.ToString();
            wheelspeed_rl.Text = latestData.wheelspeed_rl.ToString();
            wheelspeed_rr.Text = latestData.wheelspeed_rr.ToString();
            wheelspeed_fl.Text = latestData.wheelspeed_fl.ToString();
            wheelspeed_fr.Text = latestData.wheelspeed_fr.ToString();
            throttle.Text = latestData.Throttle.ToString();
            steer.Text = latestData.Steer.ToString();
            brake.Text = latestData.Brake.ToString();
            clutch.Text = latestData.Clutch.ToString();
            gear.Text = latestData.Gear.ToString();
            gforce_lat.Text = latestData.gforce_lat.ToString();
            gforce_lon.Text = latestData.gforce_lon.ToString();
            lap.Text = latestData.Lap.ToString();
            rpm.Text = latestData.RPM.ToString();
            sli.Text = latestData.SLI_Support.ToString();
            racepos.Text = latestData.RacePos.ToString();
            kers_level.Text = latestData.KERS_Level.ToString();
            kers_max.Text = latestData.KERS_Max_Level.ToString();
            drs.Text = latestData.DRS.ToString();
            traction_control.Text = latestData.Traction_Control.ToString();
            abs.Text = latestData.ABS.ToString();
            fuel.Text = latestData.Fuel.ToString();
            fuel_capacity.Text = latestData.Fuel_Capacity.ToString();
            in_pit.Text = latestData.In_Pit.ToString();
            sector.Text = latestData.Sector.ToString();
            sector1.Text = latestData.Sector1_Time.ToString();
            sector2.Text = latestData.Sector2_Time.ToString();
            braketemp_rl.Text = latestData.brake_temp_rl.ToString();
            braketemp_rr.Text = latestData.brake_temp_rr.ToString();
            braketemp_fl.Text = latestData.brake_temp_fl.ToString();
            braketemp_fr.Text = latestData.brake_temp_fr.ToString();
            wheelpsi_rl.Text = latestData.wheels_pressure_rl.ToString();
            wheelpsi_rr.Text = latestData.wheels_pressure_rr.ToString();
            wheelpsi_fl.Text = latestData.wheels_pressure_fl.ToString();
            wheelpsi_fr.Text = latestData.wheels_pressure_fr.ToString();
            teamid.Text = latestData.Team_ID.ToString();
            totallaps.Text = latestData.Total_Laps.ToString();
            tracksize.Text = latestData.Track_Size.ToString();
            lastlap.Text = latestData.Last_Lap_Time.ToString();
            maxrpm.Text = latestData.max_RPM.ToString();
            idlerpm.Text = latestData.idle_RPM.ToString();
            maxgear.Text = latestData.max_Gears.ToString();
            sessiontype.Text = latestData.SessionType.ToString();
            drs_allowed.Text = latestData.DRS_Allowed.ToString();
            trackid.Text = latestData.TrackNumber.ToString();
            fia_flag.Text = latestData.FIAFlag.ToString();
        }
    }
}
