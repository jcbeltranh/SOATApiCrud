using System.Text.Json.Serialization;
using SOATApiReact.Model;

namespace SOATApiReact.DTOs
{
    public class UserReadDto
    {
        public int Document { get; set; }
        public string DocumentType { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Genre { get; set; }
    }
}