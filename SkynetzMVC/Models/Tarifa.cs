namespace SkynetzMVC.Models
{
    public class Tarifa
    {
        public int Id { get; set; }
        public string Origem { get; set; }
        public string Destino { get; set; }
        public double ValorMinuto { get; set; }
    }
}
