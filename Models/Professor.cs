using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Diligent.MinimalAPI.Models
{
    public class Professor
    {
        [JsonIgnore]
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string JMBG{ get; set; }
        [Required]
        public int CourseID { get; set; }
        [Required]
        public int YearsOfExperience { get; set; }
    }
}
