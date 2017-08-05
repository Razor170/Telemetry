
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Xml.Serialization;
using System.Runtime.InteropServices;

namespace Telemetry
{
    [Serializable]
    public struct TelemetryPacket : ISerializable
    {
        public TelemetryPacket(SerializationInfo info, StreamingContext context)
        {
            Time = info.GetValue<float>("Time");
            LapTime = info.GetValue<float>("LapTime");
            LapDistance = info.GetValue<float>("LapDistance");
            TotalDistance = info.GetValue<float>("TotalDistance");
            X = info.GetValue<float>("X");
            Y = info.GetValue<float>("Y");
            Z = info.GetValue<float>("Z");
            Speed = info.GetValue<float>("Speed");
            VX = info.GetValue<float>("VX");
            VY = info.GetValue<float>("VY");
            VZ = info.GetValue<float>("VZ");
            RDX = info.GetValue<float>("RDX");
            RDY = info.GetValue<float>("RDY");
            RDZ = info.GetValue<float>("RDZ");
            FDX = info.GetValue<float>("FDX");
            FDY = info.GetValue<float>("FDY");
            FDZ = info.GetValue<float>("FDZ");
            susp_pos = new float[4];
            susp_pos[0] = info.GetValue<float>("susp_pos_rl");
            susp_pos[1] = info.GetValue<float>("susp_pos_rr");
            susp_pos[2] = info.GetValue<float>("susp_pos_fl");
            susp_pos[3] = info.GetValue<float>("susp_pos_fr");
            susp_vel = new float[4];
            susp_vel[0] = info.GetValue<float>("susp_vel_rl");
            susp_vel[1] = info.GetValue<float>("susp_vel_rr");
            susp_vel[2] = info.GetValue<float>("susp_vel_fl");
            susp_vel[3] = info.GetValue<float>("susp_vel_fr");
            wheel_speed = new float[4];
            wheel_speed[0] = info.GetValue<float>("wheelspeed_rl");
            wheel_speed[1] = info.GetValue<float>("wheelspeed_rr");
            wheel_speed[2] = info.GetValue<float>("wheelspeed_fl");
            wheel_speed[3] = info.GetValue<float>("wheelspeed_fr");
            Throttle = info.GetValue<float>("Throttle");
            Steer = info.GetValue<float>("Steer");
            Brake = info.GetValue<float>("Brake");
            Clutch = info.GetValue<float>("Clutch");
            Gear = info.GetValue<float>("Gear");
            gforce_lat = info.GetValue<float>("gforce_lat");
            gforce_lon = info.GetValue<float>("gforce_lon");
            Lap = info.GetValue<float>("Lap");
            RPM = info.GetValue<float>("RPM");
            SLI_Support = info.GetValue<float>("SLI_Support");
            RacePos = info.GetValue<float>("RacePos");
            KERS_Level = info.GetValue<float>("KERS_Level");
            KERS_Max_Level = info.GetValue<float>("KERS_Max_Level");
            DRS = info.GetValue<float>("DRS");
            Traction_Control = info.GetValue<float>("Traction_Control");
            ABS = info.GetValue<float>("ABS");
            Fuel = info.GetValue<float>("Fuel");
            Fuel_Capacity = info.GetValue<float>("Fuel_Capacity");
            In_Pit = info.GetValue<float>("In_Pit");
            Sector = info.GetValue<float>("Sector");
            Sector1_Time = info.GetValue<float>("Sector1_Time");
            Sector2_Time = info.GetValue<float>("Sector2_Time");
            brake_temp = new float[4];
            brake_temp[0] = info.GetValue<float>("brake_temp_rl");
            brake_temp[1] = info.GetValue<float>("brake_temp_rr");
            brake_temp[2] = info.GetValue<float>("brake_temp_fl");
            brake_temp[3] = info.GetValue<float>("brake_temp_fr");
            wheel_pressure = new float[4];
            wheel_pressure[0] = info.GetValue<float>("wheels_pressure_rl");
            wheel_pressure[1] = info.GetValue<float>("wheels_pressure_rr");
            wheel_pressure[2] = info.GetValue<float>("wheels_pressure_fl");
            wheel_pressure[3] = info.GetValue<float>("wheels_pressure_fr");
            Team_ID = info.GetValue<float>("Team_ID");
            Total_Laps = info.GetValue<float>("Total_Laps");
            Track_Size = info.GetValue<float>("Track_Size");
            Last_Lap_Time = info.GetValue<float>("Last_Lap_Time");
            max_RPM = info.GetValue<float>("max_RPM");
            idle_RPM = info.GetValue<float>("idle_RPM");
            max_Gears = info.GetValue<float>("max_Gears");
            SessionType = info.GetValue<float>("SessionType");
            DRS_Allowed = info.GetValue<float>("DRS_Allowed");
            TrackNumber = info.GetValue<float>("TrackNumber");
            FIAFlag = info.GetValue<float>("FIAFlag");
            
            Era = info.GetValue<float>("Era");
            Engine_Temp = info.GetValue<float>("Engine_Temp");
            gforce_vert = info.GetValue<float>("gforce_vert");
            ang_vel_x = info.GetValue<float>("ang_vel_x");
            ang_vel_y = info.GetValue<float>("ang_vel_y");
            ang_vel_z = info.GetValue<float>("ang_vel_z");
            tyre_temp = new byte[4];
            tyre_temp[0] = info.GetValue<byte>("tyre_temp_rl");
            tyre_temp[1] = info.GetValue<byte>("tyre_temp_rr");
            tyre_temp[2] = info.GetValue<byte>("tyre_temp_fl");
            tyre_temp[3] = info.GetValue<byte>("tyre_temp_fr");
            tyre_wear = new byte[4];
            tyre_wear[0] = info.GetValue<byte>("tyre_wear_rl");
            tyre_wear[1] = info.GetValue<byte>("tyre_wear_rr");
            tyre_wear[2] = info.GetValue<byte>("tyre_wear_fl");
            tyre_wear[3] = info.GetValue<byte>("tyre_wear_fr");
            tyre_type = info.GetValue<byte>("tyre_type");
            brake_bias = info.GetValue<byte>("brake_bias");
            fuel_mix = info.GetValue<byte>("fuel_mix");
            currentLapInvalid = info.GetValue<byte>("currentLapInvalid");
            tyre_dmg = new byte[4];
            tyre_dmg[0] = info.GetValue<byte>("tyre_dmg_rl");
            tyre_dmg[1] = info.GetValue<byte>("tyre_dmg_rr");
            tyre_dmg[2] = info.GetValue<byte>("tyre_dmg_fl");
            tyre_dmg[3] = info.GetValue<byte>("tyre_dmg_fr");
            frontwing_dmg_left = info.GetValue<byte>("frontwing_dmg_left");
            frontwing_dmg_right = info.GetValue<byte>("frontwing_dmg_right");
            rearwing_dmg = info.GetValue<byte>("rearwing_dmg");
            engine_dmg = info.GetValue<byte>("engine_dmg");
            gear_dmg = info.GetValue<byte>("gear_dmg");
            exhaust_dmg = info.GetValue<byte>("exhaust_dmg");
            pit_limiter = info.GetValue<byte>("pit_limiter");
            pit_limit_speed = info.GetValue<byte>("pit_limit_speed");
            //Car Data
            num_cars = info.GetValue<byte>("num_cars");
            player_car_index = info.GetValue<byte>("player_car_index");
            car_data = new CarUDPData[20];
            for (int i = 0; i < 20; i++)
            {
                car_data[i] = info.GetValue<CarUDPData>("car_data_"+i);
            }
            
            

            // = info.GetValue<float>("");
        }

        // Time Values
        public float Time;
        public float LapTime;
        //Distances
        public float LapDistance;
        public float TotalDistance;
        // Position and Speed
        public float X;
        public float Y;
        public float Z;
        public float Speed;
        public float VX;
        public float VY;
        public float VZ;
        public float RDX;
        public float RDY;
        public float RDZ;
        public float FDX;
        public float FDY;
        public float FDZ;
        // Wheels
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public float[] susp_pos;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public float[] susp_vel;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public float[] wheel_speed;
        // User controll
        public float Throttle;
        public float Steer;
        public float Brake;
        public float Clutch;
        public float Gear;
        public float gforce_lat;
        public float gforce_lon;
        public float Lap;
        public float RPM;
        public float SLI_Support;
        public float RacePos;
        public float KERS_Level;
        public float KERS_Max_Level;
        public float DRS;
        public float Traction_Control;
        public float ABS;
        public float Fuel;
        public float Fuel_Capacity;
        public float In_Pit;
        public float Sector;
        public float Sector1_Time;
        public float Sector2_Time;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public float[] brake_temp;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public float[] wheel_pressure;
        public float Team_ID;
        public float Total_Laps;
        public float Track_Size;
        public float Last_Lap_Time;
        public float max_RPM;
        public float idle_RPM;
        public float max_Gears;
        public float SessionType;
        public float DRS_Allowed;
        public float TrackNumber;
        public float FIAFlag;
        
        public float Era;
        public float Engine_Temp;
        public float gforce_vert;
        public float ang_vel_x;
        public float ang_vel_y;
        public float ang_vel_z;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public byte[] tyre_temp;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public byte[] tyre_wear;
        public byte tyre_type;
        public byte brake_bias;
        public byte fuel_mix;
        public byte currentLapInvalid;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public byte[] tyre_dmg;
        public byte frontwing_dmg_left;
        public byte frontwing_dmg_right;
        public byte rearwing_dmg;
        public byte engine_dmg;
        public byte gear_dmg;
        public byte exhaust_dmg;
        public byte pit_limiter;
        public byte pit_limit_speed;
        
        // Car Data
        public byte num_cars;
        public byte player_car_index;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
        public CarUDPData[] car_data;
        
        public float SpeedInKMH
        {
            get { return Speed * 3.60f; }
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Time", Time);
            info.AddValue("LapTime", LapTime);
            info.AddValue("LapDistance", LapDistance);
            info.AddValue("TotalDistance", TotalDistance);
            info.AddValue("X", X);
            info.AddValue("Y", Y);
            info.AddValue("Z", Z);
            info.AddValue("Speed", Speed);
            info.AddValue("VX", VX);
            info.AddValue("VY", VY);
            info.AddValue("VZ", VZ);
            info.AddValue("RDX", RDX);
            info.AddValue("RDY", RDY);
            info.AddValue("RDZ", RDZ);
            info.AddValue("FDX", FDX);
            info.AddValue("FDY", FDY);
            info.AddValue("FDZ", FDZ);
            info.AddValue("susp_pos_rl", susp_pos[0]);
            info.AddValue("susp_pos_rr", susp_pos[1]);
            info.AddValue("susp_pos_fl", susp_pos[2]);
            info.AddValue("susp_pos_fr", susp_pos[3]);
            info.AddValue("susp_vel_rl", susp_vel[0]);
            info.AddValue("susp_vel_rr", susp_vel[1]);
            info.AddValue("susp_vel_fl", susp_vel[2]);
            info.AddValue("susp_vel_fr", susp_vel[3]);
            info.AddValue("wheelspeed_rl", wheel_speed[0]);
            info.AddValue("wheelspeed_rr", wheel_speed[1]);
            info.AddValue("wheelspeed_fl", wheel_speed[2]);
            info.AddValue("wheelspeed_fr", wheel_speed[3]);
            info.AddValue("Throttle", Throttle);
            info.AddValue("Steer", Steer);
            info.AddValue("Brake", Brake);
            info.AddValue("Clutch", Clutch);
            info.AddValue("Gear", Gear);
            info.AddValue("gforce_lat", gforce_lat);
            info.AddValue("gforce_lon", gforce_lon);
            info.AddValue("Lap", Lap);
            info.AddValue("RPM", RPM);
            info.AddValue("SLI_Support", SLI_Support);
            info.AddValue("RacePos", RacePos);
            info.AddValue("KERS_Level", KERS_Level);
            info.AddValue("KERS_Max_Level", KERS_Max_Level);
            info.AddValue("DRS", DRS);
            info.AddValue("Traction_Control", Traction_Control);
            info.AddValue("ABS", ABS);
            info.AddValue("Fuel", Fuel);
            info.AddValue("Fuel_Capacity", Fuel_Capacity);
            info.AddValue("In_Pit", In_Pit);
            info.AddValue("Sector", Sector);
            info.AddValue("Sector1_Time", Sector1_Time);
            info.AddValue("Sector2_Time", Sector2_Time);
            info.AddValue("brake_temp_rl", brake_temp[0]);
            info.AddValue("brake_temp_rr", brake_temp[1]);
            info.AddValue("brake_temp_fl", brake_temp[2]);
            info.AddValue("brake_temp_fr", brake_temp[3]);
            info.AddValue("wheels_pressure_rl", wheel_pressure[0]);
            info.AddValue("wheels_pressure_rr", wheel_pressure[1]);
            info.AddValue("wheels_pressure_fl", wheel_pressure[2]);
            info.AddValue("wheels_pressure_fr", wheel_pressure[3]);
            info.AddValue("Team_ID", Team_ID);
            info.AddValue("Total_Laps", Total_Laps);
            info.AddValue("Track_Size", Track_Size);
            info.AddValue("Last_Lap_Time", Last_Lap_Time);
            info.AddValue("max_RPM", max_RPM);
            info.AddValue("idle_RPM", idle_RPM);
            info.AddValue("max_Gears", max_Gears);
            info.AddValue("SessionType", SessionType);
            info.AddValue("DRS_Allowed", DRS_Allowed);
            info.AddValue("TrackNumber", TrackNumber);
            info.AddValue("FIAFlag", FIAFlag);
            
            info.AddValue("Era", Era);
            info.AddValue("Engine_Temp", Engine_Temp);
            info.AddValue("geforce_vert", gforce_vert);
            info.AddValue("ang_vel_x", ang_vel_x);
            info.AddValue("ang_vel_y", ang_vel_y);
            info.AddValue("ang_vel_z", ang_vel_z);
            info.AddValue("tyre_temp_rl", tyre_temp[0]);
            info.AddValue("tyre_temp_rr", tyre_temp[1]);
            info.AddValue("tyre_temp_fl", tyre_temp[2]);
            info.AddValue("tyre_temp_fr", tyre_temp[3]);
            info.AddValue("tyre_wear_rl", tyre_wear[0]);
            info.AddValue("tyre_wear_rr", tyre_wear[1]);
            info.AddValue("tyre_wear_fl", tyre_wear[2]);
            info.AddValue("tyre_wear_fr", tyre_wear[3]);
            info.AddValue("tyre_type", tyre_type);
            info.AddValue("brake_bias", brake_bias);
            info.AddValue("fuel_mix", fuel_mix);
            info.AddValue("currentLapInvalid", currentLapInvalid);
            info.AddValue("tyre_dmg_rl", tyre_dmg[0]);
            info.AddValue("tyre_dmg_rr", tyre_dmg[1]);
            info.AddValue("tyre_dmg_fl", tyre_dmg[2]);
            info.AddValue("tyre_dmg_fr", tyre_dmg[3]);
            info.AddValue("frontwing_dmg_left", frontwing_dmg_left);
            info.AddValue("frontwing_dmg_right", frontwing_dmg_right);
            info.AddValue("rearwing_dmg", rearwing_dmg);
            info.AddValue("engine_dmg", engine_dmg);
            info.AddValue("gear_dmg", gear_dmg);
            info.AddValue("exhaust_dmg", exhaust_dmg);
            info.AddValue("pit_limiter", pit_limiter);
            info.AddValue("pit_limit_speed", pit_limit_speed);
            info.AddValue("num_cars", num_cars);
            info.AddValue("player_car_index", player_car_index);
            for (int i = 0; i < 20; i++)
            {
                info.AddValue("car_data_"+i, car_data[i]);
            }

            //info.AddValue("", );
        }
    }

    //
    // PacketUtilities
    //

    public static class PacketUtilities
    {
        // Helper method to convert the bytes received from the UDP packet into the
        // struct variable format.
        public static TelemetryPacket ConvertToPacket(byte[] bytes)
        {
            // Marshal the byte array into the telemetryPacket structure
            var handle = GCHandle.Alloc(bytes, GCHandleType.Pinned);
            var stuff = (TelemetryPacket)Marshal.PtrToStructure(
                handle.AddrOfPinnedObject(), typeof(TelemetryPacket));
            handle.Free();
            return stuff;
        }

        public static byte[] ConvertPacketToByteArray(TelemetryPacket packet)
        {
            int size = Marshal.SizeOf(packet);
            byte[] arr = new byte[size];
            IntPtr ptr = Marshal.AllocHGlobal(size);

            Marshal.StructureToPtr(packet, ptr, true);
            Marshal.Copy(ptr, arr, 0, size);
            Marshal.FreeHGlobal(ptr);

            return arr;
        }
    }

    //
    // Extensions
    //

    public static class Extensions
    {
        public static string AsTimeString(this float timeInSeconds)
        {
            if (timeInSeconds <= 0)
                return "0:00.0000";

            var ts = TimeSpan.FromSeconds((double)timeInSeconds);
            return ts.ToString(@"m\:ss\.fff");
        }

        public static T GetValue<T>(this SerializationInfo info, string fieldName)
        {
            return (T)info.GetValue(fieldName, typeof(T));
        }
    }
}
