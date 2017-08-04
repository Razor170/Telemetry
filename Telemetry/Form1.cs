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
            //if (latestData.brake_temp != null)
            //{
                update_raceinfo();
                Window2();
                UpdateThrottleBreak(latestData.Throttle, latestData.Brake);
                //Speed RPM & Gear
                data_gear.Text = manager.CurrentGear(latestData.Gear);
                data_rpm.Text = latestData.RPM.ToString("0");
                data_speed.Text = latestData.SpeedInKMH.ToString("0");
                //DRS 
                if (latestData.DRS_Allowed == 1)
                    data_drs.ForeColor = System.Drawing.Color.Orange;
                else if (latestData.DRS == 1)
                    data_drs.ForeColor = System.Drawing.Color.Green;
                else
                    data_drs.ForeColor = System.Drawing.Color.Gray;
                //FIA Flag
                switch ((int)latestData.FIAFlag)
                {
                    case 1:
                        data_flag.Text = "Green";
                        data_flag.ForeColor = System.Drawing.Color.Green;
                        break;
                    case 2:
                        data_flag.Text = "Blue";
                        data_flag.ForeColor = System.Drawing.Color.Blue;
                        break;
                    case 3:
                        data_flag.Text = "Yellow";
                        data_flag.ForeColor = System.Drawing.Color.Yellow;
                        break;
                    case 4:
                        data_flag.Text = "Red";
                        data_flag.ForeColor = System.Drawing.Color.Red;
                        break;
                    default:
                        data_flag.Text = "";
                        data_flag.ForeColor = System.Drawing.Color.Gray;
                        break;
                }
            //}
            Monitor.Exit(syncObj);
            timer1.Start();
        }
        
        private void UpdateThrottleBreak(float throttle, float brake)
        {
            const float MaxHeight = 52;

            BrakeBar.Height = (int)(MaxHeight * brake);
            ThrottleBar.Height = (int)(MaxHeight * throttle);

            BrakeBar.Location = new Point(331, 64 - BrakeBar.Height);
            ThrottleBar.Location = new Point(353, 64 - ThrottleBar.Height);
        }
        

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
            if (latestData.brake_temp != null)
            {
                braketemp_rl.Text = latestData.brake_temp[0].ToString();
                braketemp_rr.Text = latestData.brake_temp[1].ToString();
                braketemp_fl.Text = latestData.brake_temp[2].ToString();
                braketemp_fr.Text = latestData.brake_temp[3].ToString();
            }
            /*
            braketemp_rl.Text = latestData.brake_temp_rl.ToString();
            braketemp_rr.Text = latestData.brake_temp_rr.ToString();
            braketemp_fl.Text = latestData.brake_temp_fl.ToString();
            braketemp_fr.Text = latestData.brake_temp_fr.ToString();
            */
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

        private void Window2()
        {
            //Streckeninformationen
            circuit_name.Text = Circuits[(int)latestData.TrackNumber, 0];
            circuit_length.Text = Circuits[(int)latestData.TrackNumber, 1];
            circuit_rounds.Text = Circuits[(int)latestData.TrackNumber, 2];
            circuit_tyres.Text = Circuits[(int)latestData.TrackNumber, 3];
            circuit_pitspeed.Text = Circuits[(int)latestData.TrackNumber, 4];
            circuit_tyreuse.Text = Circuits[(int)latestData.TrackNumber, 5];
            circuit_drs.Text = Circuits[(int)latestData.TrackNumber, 6];
        }

        
            string [,] Circuits = new string[25, 7]
            {
                /*  Land - Ort - Strecke
                 *  Rundenlänge
                 *  Rundenanzahl
                 *  Reifentypen
                 *  Boxengeschwindigkeit
                 *  Reifenverschleiß
                 *  DRS-Zonen
                 */
                {
                    //0
                    "Australien - Melbourne - Albert Park Circuit",
                    "5,303 km",
                    "58 Runden",
                    "US, SS, S", // CHECKEN
                    "60 km/h", // CHECKEN
                    "mittel",
                    "2x via 1 MP"
                },
                {
                    //1
                    "Malaysia - Sepang - Sepang International Circuit",
                    "5,543 km",
                    "56 Runden",
                    "SS, S, M", // CHECKEN
                    "60 km/h", // CHECKEN
                    "hoch",
                    "2x via 2 MP"
                },
                {
                    //2
                    "China - Shanghai - Shanghai International Circuit",
                    "5,451 km",
                    "56 Runden",
                    "SS, S, M", // CHECKEN
                    "60 km/h", // CHECKEN
                    "hoch",
                    "2x via 2 MP"
                },
                {
                    //3
                    "Bahrain - Sachir - Bahrain International Circuit",
                    "5,412 km",
                    "57 Runden",
                    "SS, S, M", // CHECKEN
                    "60 km/h", // CHECKEN
                    "mittel",
                    "2x via 2 MP"
                },
                {
                    //4
                    "Spanien - Montmelo - Circuit de Barcelona-Catalunya",
                    "4,655 km",
                    "66 Runden",
                    "S, M, H", // CHECKEN
                    "60 km/h", // CHECKEN
                    "mittel",
                    "2x DRS" // CHECKEN
                },
                {
                    //5
                    "Monaco - Monte Carlo - Circuit de Monaco",
                    "3,337 km",
                    "78 Runden",
                    "US, SS, S", // CHECKEN
                    "60 km/h", // CHECKEN
                    "hoch",
                    "1x via 1 MP"
                },
                {
                    //6
                    "Kanada - Montreal - Circuit Gilles-Villeneuve",
                    "4,361 km",
                    "70 Runden",
                    "US, SS, S", // CHECKEN
                    "60 km/h", // CHECKEN
                    "niedrig",
                    "2x via 1 MP"
                },
                {
                    //7
                    "Großbritannien - Silverstone - Silverstone Circuit",
                    "5,891 km",
                    "52 Runden",
                    "SS, S, M", // CHECKEN
                    "60 km/h", // CHECKEN
                    "mittel",
                    "2x via 2 MP"
                },
                {
                    //8
                    "Deutschland - Hockenheim - Hockenheimring Baden-Württemberg",
                    "4,574 km",
                    "67 Runden",
                    "SS, S, M", // CHECKEN
                    "60 km/h", // CHECKEN
                    "mittel",
                    "1x via 1 MP"
                },
                {
                    //9
                    "Ungarn - Mogyorod - Hungaroring",
                    "4,381 km",
                    "70 Runden",
                    "SS, S, M", // CHECKEN
                    "60 km/h", // CHECKEN
                    "mittel",
                    "1x via 1 MP"
                },
                {
                    //10
                    "Belgien - Spa - Circuit de Spa-Francorchamps",
                    "7,004 km",
                    "44 Runden",
                    "SS, S, M", // CHECKEN
                    "60 km/h", // CHECKEN
                    "niedrig",
                    "2x via 2 MP"
                },
                {
                    //11
                    "Italien - Monza - Autodromo Nazionale Monza",
                    "5,793 km",
                    "53 Runden",
                    "SS, S, M", // CHECKEN
                    "60 km/h", // CHECKEN
                    "niedrig",
                    "2x via 2 MP"
                },
                {
                    //12
                    "Singapur - Singapur - Marina Bay Street Circuit",
                    "5,073 km",
                    "61 Runden",
                    "US, SS, S", // CHECKEN
                    "60 km/h", // CHECKEN
                    "mittel",
                    "2x via 2 MP"
                },
                {
                    //13
                    "Japan - Suzuka - Suzuka International Racing Course",
                    "5,807 km",
                    "53 Runden",
                    "SS, S, M", // CHECKEN
                    "60 km/h", // CHECKEN
                    "hoch",
                    "1x via 1 MP"
                },
                {
                    //14
                    "Vereinigte Arabische Emirate - Abu Dhabi - Yas Marina Circuit",
                    "5,554 km",
                    "55 Runden",
                    "US, SS, S", // CHECKEN
                    "60 km/h", // CHECKEN
                    "niedrig",
                    "2x via 2 MP"
                },
                {
                    //15
                    "USA - Austin - Circuit of The Americas",
                    "5,516 km",
                    "56 Runden",
                    "US, SS, S", // CHECKEN
                    "60 km/h", // CHECKEN
                    "mittel",
                    "2x via 2 MP"
                },
                {
                    //16
                    "Brasilien - Sao Paulo - Autodromo Jose Carlos Pace",
                    "4,309 km",
                    "71 Runden",
                    "SS, S, M", // CHECKEN
                    "60 km/h", // CHECKEN
                    "mittel",
                    "2x via 2 MP"
                },
                {
                    //17
                    "Österreich - Spielberg - Red Bull Ring",
                    "4,326 km",
                    "71 Runden",
                    "US, SS, S", // CHECKEN
                    "60 km/h", // CHECKEN
                    "gering",
                    "2x via 2 MP"
                },
                {
                    //18
                    "Russland - Sotschi - Sochi Autodrom",
                    "5,848 km",
                    "53 Runden",
                    "US, SS, S", // CHECKEN
                    "60 km/h", // CHECKEN
                    "niedrig",
                    "2x via 2 MP"
                },
                {
                    //19
                    "Mexiko - Mexiko-Stadt - Autodromo Hermanos Rodriguez",
                    "4,304 km",
                    "66 Runden",
                    "US, SS, S", // CHECKEN
                    "60 km/h", // CHECKEN
                    "mittel",
                    "2x via 1 MP"
                },
                {
                    //20
                    "Aserbaidschan - Baku - Baku City Circuit",
                    "6,003 km",
                    "51 Runden",
                    "SS, S, M", // CHECKEN
                    "60 km/h", // CHECKEN
                    "mittel",
                    "2x via 1 MP"
                },
                {
                    //21
                    "Land - Stadt - Strecke",
                    "0,000 km",
                    "00 Runden",
                    "US, SS, S", // CHECKEN
                    "60 km/h", // CHECKEN
                    "mittel",
                    "0x DRS"
                },
                {
                    //22
                    "Land - Stadt - Strecke",
                    "0,000 km",
                    "00 Runden",
                    "US, SS, S", // CHECKEN
                    "60 km/h", // CHECKEN
                    "mittel",
                    "0x DRS"
                },
                {
                    //23
                    "Land - Stadt - Strecke",
                    "0,000 km",
                    "00 Runden",
                    "US, SS, S", // CHECKEN
                    "60 km/h", // CHECKEN
                    "mittel",
                    "0x DRS"
                },
                {
                    //24
                    "Land - Stadt - Strecke",
                    "0,000 km",
                    "00 Runden",
                    "US, SS, S", // CHECKEN
                    "60 km/h", // CHECKEN
                    "mittel",
                    "0x DRS"
                }
            };
    }
}
