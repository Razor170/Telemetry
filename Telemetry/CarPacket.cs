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
    /*
    [Serializable]
    public struct CarUDPData : ISerializable
    {
        public CarUDPData(SerializationInfo info, StreamingContext context)
        {
            worldPosition = new float[3];
            worldPosition[0] = info.GetValue<float>("worldPositionX");
            worldPosition[1] = info.GetValue<float>("worldPositionY");
            worldPosition[2] = info.GetValue<float>("worldPositionZ");
        }

        public float[] worldPosition;

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("worldPositionX", worldPosition[0]);
            info.AddValue("worldPositionY", worldPosition[1]);
            info.AddValue("worldPositionZ", worldPosition[2]);
        }
    }
    */
}