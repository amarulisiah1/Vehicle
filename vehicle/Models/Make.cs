using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Vehicle.Models
{
    public class Make
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Abvr { get; set; }
        public virtual ICollection<Modeli> Modelis { get; set; }

    }
}