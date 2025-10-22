using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;


namespace ApiProject.Models
{
    public class Games
    {
        public int? id { get; set; }
        public string? name { get; set; }
        public List<string>? genre { get; set; }
        public List<string>? developers { get; set; }
        public List<string>? publishers { get; set; }

        [JsonIgnore]
        [Column("releaseDates")]
        public string? releaseDatesString { get; set; }

        [NotMapped]
        [JsonProperty("releaseDates")]
        public ReleaseDates? releaseDates { get; set; }
    }
}
