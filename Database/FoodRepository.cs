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
        private static string _connectionString;

        public FoodRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public string GetEmployeeName(int id)
        {
            using var conn = new SQLiteConnection(_connectionString);
            conn.Open();
            var sql = "select Nev from dolgozo where id = " + id;
            var command = new SQLiteCommand(sql, conn);
            using var rdr = command.ExecuteReader();
            rdr.Read();            
            string employeeName = rdr.GetString(0);
            return employeeName;
        }

        public List<int> GetEmployeeIds()
        {
            using var conn = new SQLiteConnection(_connectionString);
            conn.Open();            
            var sql = "select id from dolgozo";
            var command = new SQLiteCommand(sql, conn);
            using var rdr = command.ExecuteReader();
            var result = CreateIntListFromQueryResult(rdr);
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
            using var conn = new SQLiteConnection(_connectionString);
            conn.Open();
            var sql = "select Nev, Ar from termek";
            var command = new SQLiteCommand(sql, conn);
            using var rdr = command.ExecuteReader();
            var result = CreateStringListFromQueryResult(rdr);
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
            using var conn = new SQLiteConnection(_connectionString);
            conn.Open();

            var sql = "SELECT d.Id AS DolgozoID, SUM(vegosszeg) AS Total FROM Dolgozo d INNER JOIN Ertekesites e ON d.Id = e.dolgozo_id WHERE datum LIKE '2020-" + month.ToString("D2") + "%' GROUP BY Nev ORDER BY DolgozoID";
            var command = new SQLiteCommand(sql, conn);
            using var rdr = command.ExecuteReader();
       
            var result = GetBuyingResults(rdr, month);
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
            using var conn = new SQLiteConnection(_connectionString);
            conn.Open();
            var sql = "select termek_nev as Termek, sum(mennyiseg) as Total from tetel where datum like '2020-" + month.ToString("D2") + "%' group by termek_nev order by sum(mennyiseg) desc";
            var command = new SQLiteCommand(sql, conn);
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
            return result;
        }

        public void SaveItems(Dictionary<string, int> basket)
        {
            using var conn = new SQLiteConnection(_connectionString);
            conn.Open();
            var sql = "insert into tetel (Datum, Termek_nev, Mennyiseg) values(@Datum, @Termek_nev, @Mennyiseg)";
            SQLiteCommand command;
            var date = DateTime.Now.ToString("yyyy-MM-dd");
            foreach(var nev in basket.Keys)
            {
                command = new SQLiteCommand(sql, conn);
                command.Parameters.AddWithValue("@Datum", date);
                command.Parameters.AddWithValue("@Termek_nev", nev);
                command.Parameters.AddWithValue("@Mennyiseg", basket[nev]);
                command.Prepare();
                command.ExecuteNonQuery();
            }
        }

        public void SaveConsumption(EmployeeConsumption consumption)
        {
            using var conn = new SQLiteConnection(_connectionString);
            conn.Open();
            var sql = "insert into ertekesites (Datum, Vegosszeg, Dolgozo_id) values(@Datum, @Vegosszeg, @Dolgzo_id)";
            SQLiteCommand command;
            var date = DateTime.Now.ToString("yyyy-MM-dd"); 

            command = new SQLiteCommand(sql, conn);
            command.Parameters.AddWithValue("@Datum", date);
            command.Parameters.AddWithValue("@Vegosszeg", consumption.CostOfConsumption);
            command.Parameters.AddWithValue("@Dolgzo_id", consumption.EmployeeId);
            command.Prepare();
            command.ExecuteNonQuery();            
        }
    }
}