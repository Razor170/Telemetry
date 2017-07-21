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
            wheel_speed_rl = info.GetValue<float>("wheel_speed_rl");
            wheel_speed_rr = info.GetValue<float>("wheel_speed_rr");
            wheel_speed_fl = info.GetValue<float>("wheel_speed_fl");
            wheel_speed_fr = info.GetValue<float>("wheel_speed_fr");
            Throttle = info.GetValue<float>("Throttle");
            Steer = info.GetValue<float>("Steer");
            Brake = info.GetValue<float>("Brake");
            Clutch = info.GetValue<float>("Clutch");
            Gear = info.GetValue<float>("Gear");
            = info.GetValue<float>("");
            = info.GetValue<float>("");
            = info.GetValue<float>("");
            = info.GetValue<float>("");
            = info.GetValue<float>("");
            = info.GetValue<float>("");
            = info.GetValue<float>("");
            = info.GetValue<float>("");
            = info.GetValue<float>("");
            = info.GetValue<float>("");
            = info.GetValue<float>("");
            = info.GetValue<float>("");
            = info.GetValue<float>("");
            = info.GetValue<float>("");
            = info.GetValue<float>("");
            = info.GetValue<float>("");
            = info.GetValue<float>("");
            = info.GetValue<float>("");
            = info.GetValue<float>("");
            = info.GetValue<float>("");
            = info.GetValue<float>("");
            = info.GetValue<float>("");
            = info.GetValue<float>("");
            = info.GetValue<float>("");
            = info.GetValue<float>("");
            = info.GetValue<float>("");
            = info.GetValue<float>("");
            = info.GetValue<float>("");
            = info.GetValue<float>("");
            = info.GetValue<float>("");

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
        public float wheel_speed_rl;
        public float wheel_speed_rr;
        public float wheel_speed_fl;
        public float wheel_speed_fr;
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
        public float Anti_Lock_Breakes;
        public float Fuel;
        public float Fuel_Capacity;
        public float In_Pit;
        public float Sector;
        public float Sector1_Time;
        public float Sector2_Time;
        public float brake_temp;
        public float wheels_pressure;
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
            info.AddValue("wheel_speed_rl", wheel_speed_rl);
            info.AddValue("wheel_speed_rr", wheel_speed_rr);
            info.AddValue("wheel_speed_fl", wheel_speed_fl);
            info.AddValue("wheel_speed_fr", wheel_speed_fr);
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
            info.AddValue("Anti_Lock_Breakes", Anti_Lock_Breakes);
            info.AddValue("Fuel", Fuel);
            info.AddValue("Fuel_Capacity", Fuel_Capacity);
            info.AddValue("In_Pit", In_Pit);
            info.AddValue("Sector", Sector);
            info.AddValue("", Sector1_Time);
            info.AddValue("", Sector2_Time);
            info.AddValue("", brake_temp);
            info.AddValue("", wheels_pressure);
            info.AddValue("", Team_ID);
            info.AddValue("", Total_Laps);
            info.AddValue("", Track_Size);
            info.AddValue("", Last_Lap_Time);
            info.AddValue("", max_RPM);
            info.AddValue("", idle_RPM);
            info.AddValue("", max_Gears);
            info.AddValue("", SessionType);
            info.AddValue("", DRS_Allowed);
            info.AddValue("", TrackNumber);
            info.AddValue("", FIAFlag);
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
