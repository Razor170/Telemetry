﻿using System;
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
        private bool Overview = true;

        

        public Form1()
        {
            InitializeComponent();
            manager = new TelemetryManager();
        }

        private void Connect(Object myObject, EventArgs myEventArgs)
        {
            button1.Enabled = false;
            button2.Enabled = true;
            remoteIP = new IPEndPoint(IPAddress.Any, Int32.Parse(input_port.Text));

            udpSocket = new UdpClient();
            udpSocket.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
            udpSocket.ExclusiveAddressUse = false;
            udpSocket.Client.Bind(remoteIP);
            StartListening();

        }

        private void Disconnect(Object myObject, EventArgs myEventArgs)
        {
            button1.Enabled = true;
            button2.Enabled = false;
            CaptureThread.Abort();
            timer1.Stop();
            timer1.Enabled = false;
            udpSocket.Client.Close();
            udpSocket = null;
            CaptureThread = null;
        }

        private void OverviewToggle(Object myObject, EventArgs myEventArgs)
        {
            if(Overview)
            {
                this.ClientSize = new System.Drawing.Size(957, 818);
                raceoverview.Visible = false;
                Overview = false;
                return;
            }
            else
            {
                this.ClientSize = new System.Drawing.Size(1396, 818);
                raceoverview.Visible = true;
                Overview = true;
                return;
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
            debug();
            Window2();
            if(Overview)
                RacePos();
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
        

        private void debug()
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
            if (latestData.susp_pos != null)
            {
                susp_pos_rl.Text = latestData.susp_pos[0].ToString();
                susp_pos_rr.Text = latestData.susp_pos[1].ToString();
                susp_pos_fl.Text = latestData.susp_pos[2].ToString();
                susp_pos_fr.Text = latestData.susp_pos[3].ToString();
            };
            if (latestData.susp_vel != null)
            {
                susp_vel_rl.Text = latestData.susp_vel[0].ToString();
                susp_vel_rr.Text = latestData.susp_vel[1].ToString();
                susp_vel_fl.Text = latestData.susp_vel[2].ToString();
                susp_vel_fr.Text = latestData.susp_vel[3].ToString();
            };
            if (latestData.wheel_speed != null)
            {
                wheelspeed_rl.Text = latestData.wheel_speed[0].ToString();
                wheelspeed_rr.Text = latestData.wheel_speed[1].ToString();
                wheelspeed_fl.Text = latestData.wheel_speed[2].ToString();
                wheelspeed_fr.Text = latestData.wheel_speed[3].ToString();
            };
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
            };
            if (latestData.wheel_pressure != null)
            {
                wheelpsi_rl.Text = latestData.wheel_pressure[0].ToString();
                wheelpsi_rr.Text = latestData.wheel_pressure[1].ToString();
                wheelpsi_fl.Text = latestData.wheel_pressure[2].ToString();
                wheelpsi_fr.Text = latestData.wheel_pressure[3].ToString();
            };
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
            {
                circuit_name.Text = Circuits[(int)latestData.TrackNumber, 0];
                circuit_length.Text = Circuits[(int)latestData.TrackNumber, 1];
                circuit_rounds.Text = Circuits[(int)latestData.TrackNumber, 2];
                circuit_tyres.Text = Circuits[(int)latestData.TrackNumber, 3];
                circuit_pitspeed.Text = Circuits[(int)latestData.TrackNumber, 4];
                circuit_tyreuse.Text = Circuits[(int)latestData.TrackNumber, 5];
                circuit_drs.Text = Circuits[(int)latestData.TrackNumber, 6];
            }
            //Throttle
            UpdateThrottleBreak(latestData.Throttle, latestData.Brake);
            //Gear
            data_gear.Text = manager.CurrentGear(latestData.Gear);
            //RPM
            data_rpm.Text = latestData.RPM.ToString("0");
            //Speed
            data_speed.Text = latestData.SpeedInKMH.ToString("0");
            //DRS 
            {
                if (latestData.DRS_Allowed == 1)
                    data_drs.ForeColor = System.Drawing.Color.Orange;
                else if (latestData.DRS == 1)
                    data_drs.ForeColor = System.Drawing.Color.Green;
                else
                    data_drs.ForeColor = System.Drawing.Color.Gray;
            }
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
            //Session Type
            switch ((int)latestData.SessionType)
            {
                case 1:
                    data_racetype.Text = "Training";
                    break;
                case 2:
                    data_racetype.Text = "Qualifying";
                    break;
                case 3:
                    data_racetype.Text = "Rennen";
                    break;
                default:
                    data_racetype.Text = "Error";
                    break;
            }
            //Lap
            data_round.Text = "Runde: " + (int)latestData.Lap + " / " + (int)latestData.Total_Laps;
            //Race Position
            data_position.Text = "Position: " + (int)latestData.RacePos;
            //Front Wing
            data_fw_l.Text = "Status: " + (int)latestData.frontwing_dmg_left;
            data_fw_r.Text = "Status: " + (int)latestData.frontwing_dmg_right;
            //Rear Wing
            data_rw.Text = "Status: " + (int)latestData.rearwing_dmg;
            //Engine & Gear
            {
                data_engine_dmg.Text = "Motor: " + (int)latestData.engine_dmg + "%";
                data_gear_dmg.Text = "Getriebe: " + (int)latestData.gear_dmg + "%";
                data_exhaust.Text = "Erschöpfung: " + (int)latestData.exhaust_dmg + "%";
                data_engine_temp.Text = "Motor: " + (int)latestData.Engine_Temp + " °C";
            }
            //Fuel, ABS & Traction
            {
                data_fuel.Text = "Treibstoff: " + latestData.Fuel.ToString("##0.00' kg'");

                if ((int)latestData.ABS == 1)
                    data_abs.Text = "ABS: Ein";
                else
                    data_abs.Text = "ABS: Aus";

                if ((int)latestData.Traction_Control == 2)
                    data_traction.Text = "Traktionskontrolle: 100%";
                else if ((int)latestData.Traction_Control == 1)
                    data_traction.Text = "Traktionskontrolle: 50%";
                else
                    data_traction.Text = "Traktionskontrolle: 0%";
            }
            //--Tyres
            //PSI
            if (latestData.wheel_pressure != null)
            {
                data_tyre_rl_psi.Text = "Druck: " + latestData.wheel_pressure[0].ToString("##0.0") + " PSI";
                data_tyre_rr_psi.Text = "Druck: " + latestData.wheel_pressure[1].ToString("##0.0") + " PSI";
                data_tyre_fl_psi.Text = "Druck: " + latestData.wheel_pressure[2].ToString("##0.0") + " PSI";
                data_tyre_fr_psi.Text = "Druck: " + latestData.wheel_pressure[3].ToString("##0.0") + " PSI";
            };
            //Temp
            if (latestData.tyre_temp != null)
            {
                data_tyre_rl_temp.Text = "Temperatur: " + latestData.tyre_temp[0] + " °C";
                data_tyre_rr_temp.Text = "Temperatur: " + latestData.tyre_temp[1] + " °C";
                data_tyre_fl_temp.Text = "Temperatur: " + latestData.tyre_temp[2] + " °C";
                data_tyre_fr_temp.Text = "Temperatur: " + latestData.tyre_temp[3] + " °C";
            };
            //Wear
            if (latestData.tyre_wear != null)
            {
                data_tyre_rl_wear.Text = "Verbrauch: " + latestData.tyre_wear[0] + " %";
                data_tyre_rr_wear.Text = "Verbrauch: " + latestData.tyre_wear[1] + " %";
                data_tyre_fl_wear.Text = "Verbrauch: " + latestData.tyre_wear[2] + " %";
                data_tyre_fr_wear.Text = "Verbrauch: " + latestData.tyre_wear[3] + " %";
            };
            //Dmg
            if (latestData.tyre_dmg != null)
            {
                data_tyre_rl_dmg.Text = "Schaden: " + latestData.tyre_dmg[0] + " %";
                data_tyre_rr_dmg.Text = "Schaden: " + latestData.tyre_dmg[1] + " %";
                data_tyre_fl_dmg.Text = "Schaden: " + latestData.tyre_dmg[2] + " %";
                data_tyre_fr_dmg.Text = "Schaden: " + latestData.tyre_dmg[3] + " %";
            };
            //Type
            data_tyre_fl_type.Text = data_tyre_fr_type.Text = data_tyre_rl_type.Text = data_tyre_rr_type.Text = GetTyreLong(latestData.tyre_type);
            //--Brakes // RL, RR, FL, FR
            //Temp
            if (latestData.brake_temp != null)
            {
                data_brake_rl_temp.Text = "Temperatur: " + (int)latestData.brake_temp[0] + " °C";
                data_brake_rr_temp.Text = "Temperatur: " + (int)latestData.brake_temp[1] + " °C";
                data_brake_fl_temp.Text = "Temperatur: " + (int)latestData.brake_temp[2] + " °C";
                data_brake_fr_temp.Text = "Temperatur: " + (int)latestData.brake_temp[3] + " °C";
            };
            //Bias
            data_brake_fl_bias.Text = data_brake_fr_bias.Text = "Kraftverteilung: " + latestData.brake_bias.ToString() + "%";
            data_brake_rl_bias.Text = data_brake_rr_bias.Text = "Kraftverteilung: " + (100 - latestData.brake_bias).ToString() + "%";
        }

        private void RacePos()
        {
            if (latestData.car_data == null)
                return;

            string[,] data = new string[20, 10];
            float[] times = new float[20];
            byte[] round = new byte[20];
            float[] sectors = new float[20];
            for (int i = 0; i < 20; i++)
            {
                // 0: Team
                // 1: Driver
                // 2: Current Lap Time
                // 3: Delta
                // 4: S1
                // 5: S2
                // 6: S3
                // 7: Current Lap
                // 8: Tyre
                // 9: Pit
                // 10: Best Lap Time
                int _WhereAmI = latestData.car_data[i].trackPosition;
                data[_WhereAmI, 0] = GetTeam(latestData.car_data[i].teamId);
                data[_WhereAmI, 1] = GetDriver(latestData.car_data[i].driverId);
                data[_WhereAmI, 2] = latestData.car_data[i].currentLapTime.AsTimeString();
                data[_WhereAmI, 4] = latestData.car_data[i].sector1Time.AsTimeString();
                data[_WhereAmI, 5] = latestData.car_data[i].sector2Time.AsTimeString();
                data[_WhereAmI, 7] = (latestData.car_data[i].currentLapNum + 1).ToString();
                data[_WhereAmI, 8] = GetTyreShort(latestData.car_data[i].tyreCompound);
                data[_WhereAmI, 9] = GetPitStatus(latestData.car_data[i].inPits);
                data[_WhereAmI, 10] = latestData.car_data[i].bestLapTime.AsTimeString();
                times[_WhereAmI] = latestData.car_data[i].currentLapTime;
                round[_WhereAmI] = latestData.car_data[i].currentLapNum;
                sectors[_WhereAmI] = (latestData.car_data[i].sector1Time + latestData.car_data[i].sector2Time);
            };
            for (int i = 0; i < 20; i++)
            {
                // DELTA
                if (i == 0)
                    data[i, 3] = "LEADER";
                else if (round[i] != round[i - 1])
                    data[i, 3] = "+1 Runde";
                else
                    data[i, 3] = AsTimeString(times[i] - times[i - 1]);
                // S3
                float s3 = times[i] - sectors[i];
                if(s3 <= 0)
                    data[i,6] = "0:00.0000";
                else
                {
                    var ts = TimeSpan.FromSeconds((double)s3);
                    data[i,6] = ts.ToString(@"m\:ss\.fff");
                };
            };

            // I HATE MY LIFE SO MUCH
            // SO MUCH DATA
            {

                race_0_team.Text = data[0, 0];
                race_0_driver.Text = data[0, 1];
                race_0_time.Text = data[0, 2];
                race_0_delta.Text = data[0, 3];
                race_0_tyre.Text = data[0, 8];
                race_0_pit.Text = data[0, 9];
                
                race_1_team.Text = data[1, 0];
                race_1_driver.Text = data[1, 1];
                race_1_time.Text = data[1, 2];
                race_1_delta.Text = data[1, 3];
                race_1_tyre.Text = data[1, 8];
                race_1_pit.Text = data[1, 9];

                race_2_team.Text = data[2, 0];
                race_2_driver.Text = data[2, 1];
                race_2_time.Text = data[2, 2];
                race_2_delta.Text = data[2, 3];
                race_2_tyre.Text = data[2, 8];
                race_2_pit.Text = data[2, 9];

                race_3_team.Text = data[3, 0];
                race_3_driver.Text = data[3, 1];
                race_3_time.Text = data[3, 2];
                race_3_delta.Text = data[3, 3];
                race_3_tyre.Text = data[3, 8];
                race_3_pit.Text = data[3, 9];

                race_4_team.Text = data[4, 0];
                race_4_driver.Text = data[4, 1];
                race_4_time.Text = data[4, 2];
                race_4_delta.Text = data[4, 3];
                race_4_tyre.Text = data[4, 8];
                race_4_pit.Text = data[4, 9];

                race_5_team.Text = data[5, 0];
                race_5_driver.Text = data[5, 1];
                race_5_time.Text = data[5, 2];
                race_5_delta.Text = data[5, 3];
                race_5_tyre.Text = data[5, 8];
                race_5_pit.Text = data[5, 9];

                race_6_team.Text = data[6, 0];
                race_6_driver.Text = data[6, 1];
                race_6_time.Text = data[6, 2];
                race_6_delta.Text = data[6, 3];
                race_6_tyre.Text = data[6, 8];
                race_6_pit.Text = data[6, 9];

                race_7_team.Text = data[7, 0];
                race_7_driver.Text = data[7, 1];
                race_7_time.Text = data[7, 2];
                race_7_delta.Text = data[7, 3];
                race_7_tyre.Text = data[7, 8];
                race_7_pit.Text = data[7, 9];

                race_8_team.Text = data[8, 0];
                race_8_driver.Text = data[8, 1];
                race_8_time.Text = data[8, 2];
                race_8_delta.Text = data[8, 3];
                race_8_tyre.Text = data[8, 8];
                race_8_pit.Text = data[8, 9];

                race_9_team.Text = data[9, 0];
                race_9_driver.Text = data[9, 1];
                race_9_time.Text = data[9, 2];
                race_9_delta.Text = data[9, 3];
                race_9_tyre.Text = data[9, 8];
                race_9_pit.Text = data[9, 9];

                race_10_team.Text = data[10, 0];
                race_10_driver.Text = data[10, 1];
                race_10_time.Text = data[10, 2];
                race_10_delta.Text = data[10, 3];
                race_10_tyre.Text = data[10, 8];
                race_10_pit.Text = data[10, 9];

                race_11_team.Text = data[11, 0];
                race_11_driver.Text = data[11, 1];
                race_11_time.Text = data[11, 2];
                race_11_delta.Text = data[11, 3];
                race_11_tyre.Text = data[11, 8];
                race_11_pit.Text = data[11, 9];

                race_12_team.Text = data[12, 0];
                race_12_driver.Text = data[12, 1];
                race_12_time.Text = data[12, 2];
                race_12_delta.Text = data[12, 3];
                race_12_tyre.Text = data[12, 8];
                race_12_pit.Text = data[12, 9];

                race_13_team.Text = data[13, 0];
                race_13_driver.Text = data[13, 1];
                race_13_time.Text = data[13, 2];
                race_13_delta.Text = data[13, 3];
                race_13_tyre.Text = data[13, 8];
                race_13_pit.Text = data[13, 9];

                race_14_team.Text = data[14, 0];
                race_14_driver.Text = data[14, 1];
                race_14_time.Text = data[14, 2];
                race_14_delta.Text = data[14, 3];
                race_14_tyre.Text = data[14, 8];
                race_14_pit.Text = data[14, 9];

                race_15_team.Text = data[15, 0];
                race_15_driver.Text = data[15, 1];
                race_15_time.Text = data[15, 2];
                race_15_delta.Text = data[15, 3];
                race_15_tyre.Text = data[15, 8];
                race_15_pit.Text = data[15, 9];

                race_16_team.Text = data[16, 0];
                race_16_driver.Text = data[16, 1];
                race_16_time.Text = data[16, 2];
                race_16_delta.Text = data[16, 3];
                race_16_tyre.Text = data[16, 8];
                race_16_pit.Text = data[16, 9];

                race_17_team.Text = data[17, 0];
                race_17_driver.Text = data[17, 1];
                race_17_time.Text = data[17, 2];
                race_17_delta.Text = data[17, 3];
                race_17_tyre.Text = data[17, 8];
                race_17_pit.Text = data[17, 9];

                race_18_team.Text = data[18, 0];
                race_18_driver.Text = data[18, 1];
                race_18_time.Text = data[18, 2];
                race_18_delta.Text = data[18, 3];
                race_18_tyre.Text = data[18, 8];
                race_18_pit.Text = data[18, 9];

                race_19_team.Text = data[19, 0];
                race_19_driver.Text = data[19, 1];
                race_19_time.Text = data[19, 2];
                race_19_delta.Text = data[19, 3];
                race_19_tyre.Text = data[19, 8];
                race_19_pit.Text = data[19, 9];
                
            }
        }

        private string GetTeam(byte teamId)
        {
            switch(teamId)
            {
                case 0: return "Redbull";
                case 1: return "Ferrari";
                case 2: return "McLaren";
                case 3: return "Renault";
                case 4: return "Mercedes";
                case 5: return "Sauber";
                case 6: return "Force India";
                case 7: return "Williams";
                case 8: return "Toro Rosso";
                case 11: return "Haas";
                default: return "";
            }
        }

        private string GetDriver(byte driverId)
        {
            switch(driverId)
            {
                case 9: return "HAMILTON";
                case 15: return "BOTTAS";
                case 16: return "RICCIARDO";
                case 22: return "VERSTAPPEN";
                case 0: return "VETTEL";
                case 6: return "RAIKKONEN";
                case 5: return "PEREZ";
                case 33: return "OCON";
                case 3: return "MASSA";
                case 35: return "STROLL";
                case 2: return "ALONSO";
                case 34: return "VANDOORN";
                case 23: return "SAINZ";
                case 1: return "KVYAT";
                case 7: return "GROSJEAN";
                case 14: return "MAGNUSSEN";
                case 10: return "HULKENBERG";
                case 20: return "PALMER";
                case 18: return "ERICSSON";
                case 31: return "WEHRLEIN";
                default: return "";
            }
        }

        private string GetTyreShort(byte TyreType)
        {
            switch (TyreType)
            {
                case 0: return "US";
                case 1: return "SS";
                case 2: return "S";
                case 3: return "M";
                case 4: return "H";
                case 5: return "I";
                case 6: return "W";
                default: return "";
            }
        }

        private string GetTyreLong(byte TyreType)
        {
            switch (TyreType)
            {
                case 0: return "Ultra Soft";
                case 1: return "Super Soft";
                case 2: return "Soft";
                case 3: return "Medium";
                case 4: return "Hard";
                case 5: return "Intermediate";
                case 6: return "Full-Wet";
                default: return "";
            }
        }

        private string GetPitStatus(byte status)
        {
            if (status == 1 || status == 2)
                return "PIT";
            else
                return "";
        }

        public string AsTimeString(float timeInSeconds)
        {
            if (timeInSeconds <= 0)
                return "0:00.0000";

            var ts = TimeSpan.FromSeconds((double)timeInSeconds);
            return ts.ToString(@"m\:ss\.fff");
        }

        // Circuit Background Data
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
