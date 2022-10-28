using System.ComponentModel.DataAnnotations;

namespace SkynetzMVC.Models
{
    public class Plan
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int FreeMinutes { get; set; }
    }
}
