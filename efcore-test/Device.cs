using System;

namespace efcore_test
{
    public class Device
    {
        public Device(int deviceId, DateTime dateFrom, string state, string locationName)
        {
            DeviceId = deviceId;
            DateFrom = dateFrom;
            State = state;
            LocationName = locationName;
        }

        public int DeviceId { get; }
        public string DeviceName { get; set; }
        public DateTime DateFrom { get; }
        public string State { get; }
        public string LocationName { get; }
    }
}
