using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
namespace Diligent.MinimalAPI.Models
{
    public class Book
    {
        [JsonIgnore]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string ISBN { get; set; }

        [Required]
        public DateTime ReleasedDate { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string Publisher { get; set; }
        
        [Required]
        public int PageCount { get; set; }


    }
}
