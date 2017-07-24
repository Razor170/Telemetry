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
            susp_pos_rl = info.GetValue<float>("susp_pos_rl");
            susp_pos_rr = info.GetValue<float>("susp_pos_rr");
            susp_pos_fl = info.GetValue<float>("susp_pos_fl");
            susp_pos_fr = info.GetValue<float>("susp_pos_fr");
            susp_vel_rl = info.GetValue<float>("susp_vel_rl");
            susp_vel_rr = info.GetValue<float>("susp_vel_rr");
            susp_vel_fl = info.GetValue<float>("susp_vel_fl");
            susp_vel_fr = info.GetValue<float>("susp_vel_fr");
            wheelspeed_rl = info.GetValue<float>("wheelspeed_rl");
            wheelspeed_rr = info.GetValue<float>("wheelspeed_rr");
            wheelspeed_fl = info.GetValue<float>("wheelspeed_fl");
            wheelspeed_fr = info.GetValue<float>("wheelspeed_fr");
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
            brake_temp_rl = info.GetValue<float>("brake_temp_rl");
            brake_temp_rr = info.GetValue<float>("brake_temp_rr");
            brake_temp_fl = info.GetValue<float>("brake_temp_fl");
            brake_temp_fr = info.GetValue<float>("brake_temp_fr");
            wheels_pressure_rl = info.GetValue<float>("wheels_pressure_rl");
            wheels_pressure_rr = info.GetValue<float>("wheels_pressure_rr");
            wheels_pressure_fl = info.GetValue<float>("wheels_pressure_fl");
            wheels_pressure_fr = info.GetValue<float>("wheels_pressure_fr");
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
        public float susp_pos_rl;
        public float susp_pos_rr;
        public float susp_pos_fl;
        public float susp_pos_fr;
        public float susp_vel_rl;
        public float susp_vel_rr;
        public float susp_vel_fl;
        public float susp_vel_fr;
        public float wheelspeed_rl;
        public float wheelspeed_rr;
        public float wheelspeed_fl;
        public float wheelspeed_fr;
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
        public float brake_temp_rl;
        public float brake_temp_rr;
        public float brake_temp_fl;
        public float brake_temp_fr;
        public float wheels_pressure_rl;
        public float wheels_pressure_rr;
        public float wheels_pressure_fl;
        public float wheels_pressure_fr;
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
            info.AddValue("susp_pos_rl", susp_pos_rl);
            info.AddValue("susp_pos_rr", susp_pos_rr);
            info.AddValue("susp_pos_fl", susp_pos_fl);
            info.AddValue("susp_pos_fr", susp_pos_fr);
            info.AddValue("susp_vel_rl", susp_vel_rl);
            info.AddValue("susp_vel_rr", susp_vel_rr);
            info.AddValue("susp_vel_fl", susp_vel_fl);
            info.AddValue("susp_vel_fr", susp_vel_fr);
            info.AddValue("wheelspeed_rl", wheelspeed_rl);
            info.AddValue("wheelspeed_rr", wheelspeed_rr);
            info.AddValue("wheelspeed_fl", wheelspeed_fl);
            info.AddValue("wheelspeed_fr", wheelspeed_fr);
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
            info.AddValue("brake_temp_rl", brake_temp_rl);
            info.AddValue("brake_temp_rr", brake_temp_rr);
            info.AddValue("brake_temp_fl", brake_temp_fl);
            info.AddValue("brake_temp_fr", brake_temp_fr);
            info.AddValue("wheels_pressure_rl", wheels_pressure_rl);
            info.AddValue("wheels_pressure_rr", wheels_pressure_rr);
            info.AddValue("wheels_pressure_fl", wheels_pressure_fl);
            info.AddValue("wheels_pressure_fr", wheels_pressure_fr);
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
