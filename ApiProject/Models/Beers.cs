using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiProject.Models
{
    public class Beers
    {

        public double? price { get; set; }
        public string? name { get; set; }
        public string? image { get; set; }

        [JsonProperty("id_beer")]
        public int id { get; set; }
        public TypeBeer? type_beer { get; set; }
    }
}
