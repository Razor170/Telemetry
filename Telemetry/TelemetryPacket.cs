
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

    public struct TelemetryPacket
    {
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
        public float session_time_left;
        public byte rev_lights_percent;
        public byte is_spectating;
        public byte spectator_car_index;
        // Car Data
        public byte num_cars;
        public byte player_car_index;//336

        public float SpeedInKMH
        {
            get { return Speed * 3.60f; }
        }
        public int racePos
        {
            get { return ((int)RacePos + 1); }
        }
    }

    public struct CarUDPData
    {
        public float worldPosition_x;
        public float worldPosition_y;
        public float worldPosition_z;
        public float lastLapTime;
        public float currentLapTime;
        public float bestLapTime;
        public float sector1Time;
        public float sector2Time;
        public float lapDistance;
        public byte driverId;
        public byte teamId;
        public byte trackPosition;
        public byte currentLapNum;
        public byte tyreCompound;
        public byte inPits;
        public byte sector;
        public byte currentLapInvalid;
        public byte penalities;
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

        public static CarUDPData ConvertToCarPacket(byte[] bytes)
        {
            // Marshal the byte array into the telemetryPacket structure
            var handle = GCHandle.Alloc(bytes, GCHandleType.Pinned);
            
            var stuff = (CarUDPData)Marshal.PtrToStructure(
                handle.AddrOfPinnedObject(), typeof(CarUDPData));
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
