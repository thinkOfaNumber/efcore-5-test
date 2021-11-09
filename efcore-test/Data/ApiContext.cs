using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace efcore_test.Data
{
    public class ApiContext : DbContext
    {
        public ApiContext(DbContextOptions<ApiContext> options) : base(options) { }

        public DbSet<Device> Devices { get; set; }
        public DbSet<History> Histories { get; set; }
        public DbSet<Location> Locations { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.LogTo(message => Debug.WriteLine(message));
        }
    }

    public class Device
    {
        public int DeviceId { get; set; }
        public string DeviceName { get; set; }

        public List<History> Histories { get; } = new List<History>();
        public List<Attribute> Attributes { get; } = new List<Attribute>();
    }

    public class History
    {
        public int HistoryId { get; set; }
        public DateTime DateFrom { get; set; }
        public string State { get; set; }

        public int DeviceId { get; set; }
        public Device Device { get; set; }

        public int? LocationId { get; set; }
        public Location Location { get; set; }
    }

    public class Attribute
    {
        public int AttributeId { get; set; }
        public string Name { get; set; }

        public int DeviceId { get; set; }
        public Device Device { get; set; }
    }

    public class Location
    {
        public int LocationId { get; set; }
        public string LocationName { get; set; }

        public List<History> Histories { get; } = new List<History>();
    }

    public static class ContextExtensions
    {
        public static efcore_test.Device ToModel(this Device entity)
        {
            var current = entity.Histories.First();
            return new efcore_test.Device(entity.DeviceId, current.DateFrom, current.State,
                current.Location?.LocationName, string.Join(", ", entity.Attributes.Select(a => a.Name)))
            {
                DeviceName = entity.DeviceName
            };
        }
    }
}
