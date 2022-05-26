using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Diligent.MinimalAPI.Models
{
    public class Student
    {
        [JsonIgnore]
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public int IndexNum { get; set; }
        [Required]
        public int Semester { get; set; }
        [Required]
        public string Address { get; set; }
    }
}
