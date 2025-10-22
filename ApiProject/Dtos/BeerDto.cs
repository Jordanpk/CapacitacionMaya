namespace ApiProject.Dtos
{
    public class BeerDto
    {
        public string price { get; set; }
        public string name { get; set; }
        public Rating rating { get; set; }
        public string image { get; set; }
        public int id { get; set; }
    }
    public class Rating
    {
        public double average { get; set; }
        public int reviews { get; set; }
    }
}
