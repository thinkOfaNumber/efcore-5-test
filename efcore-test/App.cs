using System;
using System.Collections.Generic;
using System.Linq;
using efcore_test.Data;
using Microsoft.EntityFrameworkCore;
using Attribute = efcore_test.Data.Attribute;

namespace efcore_test
{
    internal class App
    {
        private const int Devices = 10;

        private static readonly List<string> Locations = new()
        {
            "Helsinki", "California", "London", "Black Stump", "Post Office", "Sheep Station", "Mars", "Your Backyard",
            "The Ocean", "somewhere"
        };
        private readonly ApiContext _apiContext;

        public App(ApiContext apiContext)
        {
            _apiContext = apiContext;
        }

        public List<Device> PrintDevices()
        {
            var devices = _apiContext.Devices.AsNoTracking()
                .Include(d => d.Histories.OrderByDescending(h => h.DateFrom).Take(1))
                .ThenInclude(h => h.Location)
                .Include(d => d.Attributes)
                .Select(d => d.ToModel()).ToList();

            Console.WriteLine($"{devices.Count} Devices:");
            devices.ForEach(d => Console.WriteLine($"{d.DeviceId}\t{d.DeviceName}\t{d.DateFrom}\t{d.State}\t{d.Attributes}\t{d.LocationName}"));
            return devices;
        }

        public void PrintSingleDevice(int deviceId)
        {
            var device = _apiContext.Devices.AsNoTracking()
                .Include(d => d.Histories.OrderByDescending(h => h.DateFrom).Take(1))
                .ThenInclude(h => h.Location)
                .Include(d => d.Attributes)
                .First(d => d.DeviceId == deviceId)
                //.Where(d => d.DeviceId == deviceId) // workaround
                //.AsEnumerable() // switch to client evaluation (LINQ to Objects context)
                //.First() // and execute First here
                .ToModel();

            Console.WriteLine(
                $"{device.DeviceId}\t{device.DeviceName}\t{device.DateFrom}\t{device.State}\t{device.Attributes}\t{device.LocationName}");
        }

        public void AddDevices()
        {
            for (int i = 0; i < Devices; i++)
            {
                var device = new Data.Device { DeviceName = Guid.NewGuid().ToString()};
                var history = new History
                {
                    DateFrom = DateTime.Now,
                    State = "Ready"
                };
                if (i % 3 == 0)
                {
                    history.Location = _apiContext.Locations.OrderBy(r => Guid.NewGuid()).First();
                }
                device.Histories.Add(history);
                device.Attributes.Add(new Attribute {Name = "foo"});
                device.Attributes.Add(new Attribute {Name = "bar"});
                device.Attributes.Add(new Attribute {Name = "baz"});
                _apiContext.Devices.Add(device);
            }
            _apiContext.SaveChanges();
        }

        public void AddLocations()
        {
            Locations.ForEach(l =>
            {
                var location = new Location { LocationName = l };
                _apiContext.Locations.Add(location);
            });
            _apiContext.SaveChanges();
        }

        public void DeleteAll()
        {
            foreach (var d in _apiContext.Devices)
            {
                _apiContext.Remove(d);
            }
            foreach (var l in _apiContext.Locations)
            {
                _apiContext.Remove(l);
            }
            _apiContext.SaveChanges();
        }
    }
}
