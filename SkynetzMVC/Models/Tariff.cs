namespace SkynetzMVC.Models
{
    public class Tariff
    {
        public int Id { get; set; }
        public string Source { get; set; }
        public string Destination { get; set; }
        public double MinuteValue { get; set; }
    }
}
