using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SOATApiReact.Model
{
    public class SOAT
    {
        public User Owner { get; set; }
        public Vehicle Vehicle { get; set; }
        [Key, Column(Order = 1)]
        public int OwnerDocument { get; set; }
        [Key, Column(Order = 2)]
        public string VehiclePlate { get; set; }
        [Key, Column(Order = 3, TypeName="Date")]
        public DateTime Year { get; set; }
    }
}