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
    public struct CarUDPData : ISerializable
    {
        public CarUDPData(SerializationInfo info, StreamingContext context)
        {
            worldPosition = new float[3];
            worldPosition[0] = info.GetValue<float>("worldPositionX");
            worldPosition[1] = info.GetValue<float>("worldPositionY");
            worldPosition[2] = info.GetValue<float>("worldPositionZ");
            lastLapTime = info.GetValue<float>("lastLapTime");
            currentLapTime = info.GetValue<float>("currentLapTime");
            bestLapTime = info.GetValue<float>("bestLapTime");
            sector1Time = info.GetValue<float>("sector1Time");
            sector2Time = info.GetValue<float>("sector2Time");
            lapDistance = info.GetValue<float>("lapDistance");
            driverId = info.GetValue<byte>("driverId");
            teamId = info.GetValue<byte>("teamId");
            trackPosition = info.GetValue<byte>("trackPosition");
            currentLapNum = info.GetValue<byte>("currentLapNum");
            tyreCompound = info.GetValue<byte>("tyreCompound");
            inPits = info.GetValue<byte>("inPits");
            sector = info.GetValue<byte>("sector");
            currentLapInvalid = info.GetValue<byte>("currentLapInvalid");
            penalities = info.GetValue<byte>("penalities");
        }
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        public float[] worldPosition;
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

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("worldPositionX", worldPosition[0]);
            info.AddValue("worldPositionY", worldPosition[1]);
            info.AddValue("worldPositionZ", worldPosition[2]);
            info.AddValue("lastLapTime", lastLapTime);
            info.AddValue("currentLapTime", currentLapTime);
            info.AddValue("bestLapTime", bestLapTime);
            info.AddValue("sector1Time", sector1Time);
            info.AddValue("sector2Time", sector2Time);
            info.AddValue("lapDistance", lapDistance);
            info.AddValue("driverId", driverId);
            info.AddValue("teamId", teamId);
            info.AddValue("trackPosition", trackPosition);
            info.AddValue("currentLapNum", currentLapNum);
            info.AddValue("tyreCompound", tyreCompound);
            info.AddValue("inPits", inPits);
            info.AddValue("sector", sector);
            info.AddValue("currentLapInvalid", currentLapInvalid);
            info.AddValue("penalities", penalities);
        }
    }
    
}