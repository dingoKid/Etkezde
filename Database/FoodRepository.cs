using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Etkezde.Models;
using Microsoft.Data.Sqlite;
using System.Data.SQLite;

namespace Etkezde.Database
{
    public class FoodRepository
    {
        private static SQLiteConnection _conn;

        public FoodRepository(string connectionString)
        {
            _conn = new SQLiteConnection(connectionString);
        }

        public List<EmployeeConsumption> GetEmployeeConsumptions(int month)
        {
            _conn.Open();

            var sql = "SELECT d.Id AS DolgozoID, d.nev AS Nev, SUM(vegosszeg) AS Total FROM Dolgozo d INNER JOIN Ertekesites e ON d.Id = e.dolgozo_id WHERE datum LIKE '2020-" + month + "%' GROUP BY Nev ORDER BY DolgozoID";
            var command = new SQLiteCommand(sql, _conn);
            using var rdr = command.ExecuteReader();
       
            var result = GetBuyingResults(rdr, month);
            return result;
        }

        private List<EmployeeConsumption> GetBuyingResults(SQLiteDataReader reader, int month)
        {
            var result = new List<EmployeeConsumption>();
            while (reader.Read())
            {
                result.Add(new EmployeeConsumption(reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2)));
            }
            _conn.Close();
            return result;
        }

        public List<ProductConsumption> GetProductConsumption(int month)
        {
            _conn.Open();

            var sql = "SELECT t.termek_nev AS Termek, SUM(mennyiseg) AS Mennyiseg from ertekesites e inner join tetelek t on e.id = t.ertekesites_id where e.datum like '2020-" + month + "%' group by termek order by Mennyiseg desc";
            var command = new SQLiteCommand(sql, _conn);
            using var rdr = command.ExecuteReader();

            var result = GetProductResult(rdr, month);
            return result;
        }

        private List<ProductConsumption> GetProductResult(SQLiteDataReader reader, int month)
        {
            var result = new List<ProductConsumption>();
            while (reader.Read())
            {
                result.Add(new ProductConsumption(reader.GetString(0), reader.GetInt32(1)));
            }
            _conn.Close();
            return result;
        }
    }
}