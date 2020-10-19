using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Etkezde.Models;

namespace Database
{
    public class FoodRepository
    {
        private static SqlConnection _connection;

        public FoodRepository(string connectionString)
        {
            _connection = new SqlConnection(connectionString);
        }

        public List<EmployeeConsumption> GetEmployeeConsumptions(int month)
        {            
            _connection.Open();

            var sql = "SELECT d.Id AS DolgozoID, d.nev AS Nev, SUM(ertek) AS Total FROM Dolgozo d INNER JOIN Ertekesites e ON d.Id = e.dolgozo_id WHERE datum LIKE '2020-@month%' GROUP BY Nev ORDER BY DolgozoID";

            var command = CreateCommand(sql, month);

            using SqlDataReader reader = command.ExecuteReader();
            
            _connection.Close();

            return GetResults(reader, month);
        }

        private SqlCommand CreateCommand(string sql, int month)
        {
            using var command = new SqlCommand(sql, _connection);
            command.Parameters.Add("@month", SqlDbType.Int).Value = month;
            command.Prepare();
            return command;
        }

        private List<EmployeeConsumption> GetResults(SqlDataReader reader, int month)
        {
            var result = new List<EmployeeConsumption>();
            while(reader.Read())
            {
                result.Add(new EmployeeConsumption(reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2), month));
            }
            return result;
        }
    }
}