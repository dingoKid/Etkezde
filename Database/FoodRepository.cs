using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Etkezde.Models;
using System.Data.SQLite;
using System;

namespace Etkezde.Database
{
    public class FoodRepository
    {
        private static SQLiteConnection _conn;

        public FoodRepository(string connectionString)
        {
            _conn = new SQLiteConnection(connectionString);
        }

        public string GetEmployeeName(int id)
        {
            _conn.Open();
            var sql = "select Nev from dolgozo where id = " + id;
            var command = new SQLiteCommand(sql, _conn);
            using var rdr = command.ExecuteReader();
            rdr.Read();            
            string employeeName = rdr.GetString(0);
            _conn.Close();
            return employeeName;
        }

        public List<int> GetEmployeeIds()
        {
            _conn.Open();
            var sql = "select id from dolgozo";
            var command = new SQLiteCommand(sql, _conn);
            using var rdr = command.ExecuteReader();
            var result = CreateIntListFromQueryResult(rdr);
            _conn.Close();
            return result;
        }

        private List<int> CreateIntListFromQueryResult(SQLiteDataReader reader)
        {
            var result = new List<int>();
            while (reader.Read())
            {
                result.Add(reader.GetInt32(0));
            }
            return result;
        }

        public Dictionary<string, int> GetFoods()
        {
            _conn.Open();
            var sql = "select Nev, Ar from termek";
            var command = new SQLiteCommand(sql, _conn);
            using var rdr = command.ExecuteReader();
            var result = CreateStringListFromQueryResult(rdr);
            _conn.Close();
            return result;
        }

        private Dictionary<string, int> CreateStringListFromQueryResult(SQLiteDataReader reader)
        {
            var result = new Dictionary<string, int>();
            while (reader.Read())
            {
                result.Add(reader.GetString(0), reader.GetInt32(1));
            }
            return result;
        }

        public List<EmployeeConsumption> GetEmployeeConsumptions(int month)
        {
            _conn.Open();

            var sql = "SELECT d.Id AS DolgozoID, SUM(vegosszeg) AS Total FROM Dolgozo d INNER JOIN Ertekesites e ON d.Id = e.dolgozo_id WHERE datum LIKE '2020-" + month + "%' GROUP BY Nev ORDER BY DolgozoID";
            var command = new SQLiteCommand(sql, _conn);
            using var rdr = command.ExecuteReader();
       
            var result = GetBuyingResults(rdr, month);
            _conn.Close();
            return result;
        }

        private List<EmployeeConsumption> GetBuyingResults(SQLiteDataReader reader, int month)
        {
            var result = new List<EmployeeConsumption>();
            while (reader.Read())
            {
                result.Add(new EmployeeConsumption(reader.GetInt32(0), reader.GetInt32(1)));
            }
            return result;
        }

        public List<ProductConsumption> GetProductConsumption(int month)
        {
            _conn.Open();

            var sql = "SELECT t.termek_nev AS Termek, SUM(mennyiseg) AS Mennyiseg from ertekesites e inner join tetel t on e.id = t.ertekesites_id where e.datum like '2020-" + month + "%' group by termek order by Mennyiseg desc";
            var command = new SQLiteCommand(sql, _conn);
            using var rdr = command.ExecuteReader();

            var result = GetProductResult(rdr, month);
            _conn.Close();
            return result;
        }

        private List<ProductConsumption> GetProductResult(SQLiteDataReader reader, int month)
        {
            var result = new List<ProductConsumption>();
            while (reader.Read())
            {
                result.Add(new ProductConsumption(reader.GetString(0), reader.GetInt32(1)));
            }            
            return result;
        }

        public void SaveItems(Dictionary<string, int> basket)
        {
            _conn.Open();
            var sql = "insert into tetel (Datum, Termek_nev, Mennyiseg) values(@Datum, @Termek_nev, @Mennyiseg)";
            SQLiteCommand command;
            var date = DateTime.Now.ToString("yyyy-MM-dd");
            foreach(var nev in basket.Keys)
            {
                command = new SQLiteCommand(sql, _conn);
                command.Parameters.AddWithValue("@Datum", date);
                command.Parameters.AddWithValue("@Termek_nev", nev);
                int num = basket[nev];
                command.Parameters.AddWithValue("@Mennyiseg", num);
                command.Prepare();
                command.ExecuteNonQuery();
            }            
            _conn.Close();
        }
    }
}