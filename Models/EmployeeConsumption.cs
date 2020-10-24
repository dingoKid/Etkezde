namespace Etkezde.Models
{
    public class EmployeeConsumption
    {
        public int EmployeeId { get; set; }
        public int CostOfConsumption { get; set; }

        public EmployeeConsumption(int id, int cost)
        {
            EmployeeId = id;
            CostOfConsumption = cost;
        }
    }
}