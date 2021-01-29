using System.ComponentModel.DataAnnotations;

namespace SOATApiReact.DTOs
{
    public class VehicleCreateDto
    {
        [Required]
        public string Plate { get; set; }
        [Required]
        public string Color { get; set; }
        [Required]
        public string Engine { get; set; }
        [Required]
        [Range(2.0, 20.0)]
        public int Axles { get; set; }
    }
}