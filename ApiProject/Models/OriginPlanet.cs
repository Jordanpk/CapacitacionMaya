namespace ApiProject.Models
{
    public class OriginPlanet
    {
        public int id { get; set; }
        public string name { get; set; }
        public bool isDestroyed { get; set; }
        public string description { get; set; }
        public string image { get; set; }
        public object deletedAt { get; set; }
    }
}
