using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Diligent.MinimalAPI.Models
{
    public class Course
    {
        [JsonIgnore]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int Points { get; set; }
        [Required]
        public string Code { get; set; }
        [Required]
        public int Semester { get; set; }
        [Required]
        public bool IsOptional { get; set; }
    }
}
