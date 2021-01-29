using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SOATApiReact.Model
{
    public class User
    {
        [Key]
        public int Document { get; set; }
        [Required]
        [Range(1, int.MaxValue)]
        public DocumentType DocumentType { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [Required]
        [MaxLength(50)]
        public string Surname { get; set; }
        [Required]
        public string Genre { get; set; }

        public IEnumerable<SOAT> SOATs { get; set; }
    }
}