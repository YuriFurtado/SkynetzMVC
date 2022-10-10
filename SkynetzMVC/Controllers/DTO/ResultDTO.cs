namespace SkynetzMVC.Controllers.DTO
{
    public class ResultDTO
    {
        public string Source { get; set; }
        public string Destination { get; set; }
        public int UsedMinutes { get; set; }
        public string UsedPlan { get; set; }
        public string PriceWithPlan { get; set; }
        public string PriceWithoutPlan { get; set; }
    }
}
