namespace Etkezde.Models
{
    public class EmployeeConsumption
    {
        public int EmployeeId { get; set; }
        public string Name { get; set; }
        public int CostOfConsumption { get; set; }

        public EmployeeConsumption(int id, string name, int cost)
        {
            EmployeeId = id;
            Name = name;
            CostOfConsumption = cost;
        }
    }
}