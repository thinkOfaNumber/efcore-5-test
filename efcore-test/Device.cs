using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace efcore_test
{
    public class Device
    {
        public Device(int deviceId, DateTime dateFrom, string state, string locationName, string attributes)
        {
            DeviceId = deviceId;
            DateFrom = dateFrom;
            State = state;
            LocationName = locationName;
            Attributes = attributes;
        }

        public int DeviceId { get; }
        public string DeviceName { get; set; }
        public DateTime DateFrom { get; }
        public string State { get; }
        public string LocationName { get; }
        public string Attributes { get; }
    }
}
