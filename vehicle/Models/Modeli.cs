using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Vehicle.Models
{
    public class Modeli
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public int MakeId { get; set; }
        public string Abvr { get; set; }
        public virtual Make Make { get; set; }

    }
}