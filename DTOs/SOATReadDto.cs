using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SOATApiReact.Model;

namespace SOATApiReact.DTOs
{
    public class SOATReadDto
    {
        [Key]
        [Column(Order = 1)]
        public UserReadDto Owner { get; set; }
        [Key]
        [Column(Order = 2)]
        public VehicleReadDto Vehicle { get; set; }
        [Key]
        [Column(Order = 3, TypeName="Date")]
        public DateTime Year { get; set; }
    }
}