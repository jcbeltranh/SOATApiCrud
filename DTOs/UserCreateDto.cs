using System.ComponentModel.DataAnnotations;
using SOATApiReact.Model;

namespace SOATApiReact.DTOs
{
    public class UserCreateDto
    {
        [Key]
        [Required]
        public int Document { get; set; }
        [Required]
        public DocumentType DocumentType { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [Required]
        [MaxLength(50)]
        public string Surname { get; set; }
        [Required]
        public string Genre { get; set; }
    }
}