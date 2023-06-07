using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diplom
{

    
        using System.Data;
        using System.Data.SqlClient;
        public class Customer
        {
            private const string CONNECTION_STRING =
                "Data Source=(local);Initial Catalog=DesignPatterns;Integrated Security=True";

            public int ID { get; set; }
            public string Name { get; set; }
            public bool IsPremiumMember { get; set; }

            public Customer(int id, string name, bool isPremiumMember)
            {
                ID = id;
                Name = name;
                IsPremiumMember = isPremiumMember;
            }

            // Этот статический метод действует как фабрика объектов для объектов Customer,
            // чтение значений из базы данных и создание объекта.
            //  Итак, код для получения клиента из базы данных может быть:
            //
            //    Customer.GetByID(123);
            //
            public static Customer GetByID(int id)
            {
                using (SqlConnection connection = new SqlConnection(CONNECTION_STRING))
                {
                    connection.Open();

                    using (SqlCommand command = connection.CreateCommand())
                    {
                        command.CommandType = CommandType.Text;

                        command.CommandText = "SELECT TOP 1 * FROM [Customer] WHERE [ID] = @ID";
                        command.Parameters.AddWithValue("@ID", id);

                        SqlDataReader reader = command.ExecuteReader();
                        if (reader.HasRows)
                        {
                            reader.Read();

                            string name = (string)reader["Name"];
                            bool isPremiumMember = (bool)reader["IsPremiumMember"];

                            return new Customer(id, name, isPremiumMember);
                        }
                    }
                }

                return null;
            }

            public void Save()
            {
                // Этот метод должен обрабатывать INSERT и UPDATE.
                // Или вам нужно создать две отдельные функции и вызывать их при необходимости.

                // Этот метод не должен получать параметр с объектом Customer.
                // Он находится внутри объекта Customer, поэтому все значения свойств для него уже доступны.
            }

            public void Delete()
            {
                using (SqlConnection connection = new SqlConnection(CONNECTION_STRING))
                {
                    connection.Open();

                    using (SqlCommand command = connection.CreateCommand())
                    {
                        command.CommandType = CommandType.Text;
                        command.CommandText = "DELETE FROM [Customer] WHERE [ID] = @ID";
                        command.Parameters.AddWithValue("@ID", ID);
                        command.ExecuteNonQuery();
                    }
                }
            }
        }
    }
