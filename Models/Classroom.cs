using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;

namespace Diligent.MinimalAPI.Models
{
    public class Classroom
    {
        [JsonIgnore]
        public int Id { get; set; }

        [Required]
        public string Identifier { get; set; }

        [Required]
        public int Floor { get; set; }

        [Required]
        public int Capacity { get; set; }

        [Required]
        public string Sector { get; set; }

        [Required]
        public bool IsCopmuterLab { get; set; }


    }
}
