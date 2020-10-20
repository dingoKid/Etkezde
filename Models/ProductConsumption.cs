namespace Etkezde.Models
{
    public class ProductConsumption
    {        
        public string ProductName { get; set; }
        public int TotalConsumption { get; set; }

        public ProductConsumption(string name, int total)
        {
            ProductName = name;
            TotalConsumption = total;
        }
    }
}