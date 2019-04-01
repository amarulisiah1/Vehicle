using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Vehicle.Models
{

    public class VehicleContext : DbContext
    {
        public DbSet<Make> Makes { get; set; }
        public DbSet<Modeli> Modelis { get; set; }
    }
}