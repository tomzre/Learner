using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Learner.Models;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Diagnostics;

namespace Learner.Services
{
    public class ReservationRepository : IReservationRepository
    {
        private string GetConnectionString()
        {
            var connectionSB = new SqlConnectionStringBuilder();
            connectionSB.DataSource = ".\\SQLExpress";
            connectionSB.InitialCatalog = "Northwind";
            connectionSB.IntegratedSecurity = true;
            return connectionSB.ToString();
        }

        private List<Reservation> data = new List<Reservation>
        {
            new Reservation{ ReservationId = 1, ClientName = "Kowalski", Location="Krakow"},
            new Reservation{ ReservationId = 1, ClientName = "Boberek", Location="Warszawa"},
            new Reservation{ ReservationId = 1, ClientName = "Smith", Location="Londyn"}
        };

        private string queryString =
         "SELECT ProductID, UnitPrice, ProductName from dbo.products "
             + "WHERE UnitPrice > @pricePoint "
             + "ORDER BY UnitPrice DESC;";
        public Reservation Add(Reservation item)
        {
            using (SqlConnection connection = new SqlConnection(GetConnectionString()))
            {
               connection.Open();
                Debug.WriteLine($"State: {connection.State}");
                Debug.WriteLine($"connection string: {connection.ConnectionString}");

                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@pricePont", 5);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Debug.WriteLine($"\t{ reader[0]}\t{ reader[1]}\t{ reader[2]}");
                    }
                    reader.Close();
                }
                catch(Exception ex)
                {
                    Debug.WriteLine(ex);
                }

            }
            //    item.ReservationId = data.Count + 1;
            //data.Add(item);
            return item;

        }

        public Reservation Get(int id)
        {
            var matches = data.Where(r => r.ReservationId == id);
            return matches.Count() > 0 ? matches.FirstOrDefault() : null;
        }

        public IEnumerable<Reservation> GetAll()
        {
            return data;
        }

        public void Remove(int id)
        {
            var item = Get(id);
            if(item != null)
            data.Remove(item);
        }

        public bool Update(Reservation item)
        {
            var storedItem = Get(item.ReservationId);
            if(storedItem != null)
            {
                storedItem.Location = item.Location;
                storedItem.ClientName = item.ClientName;
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}