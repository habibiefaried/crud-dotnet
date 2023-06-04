using MySqlConnector;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

public class Users
{
    public static async Task<string> GetUsers()
    {
        string connectionString = "Server=db;Port=3306;Database=mydatabase;Uid=myuser;Pwd=Mypassword2023";
        using var conn = new MySqlConnection(connectionString);

        try
        {
            Console.WriteLine("Connecting to MySQL...");
            await conn.OpenAsync();
            string query = "SELECT * FROM users";
            using var command = new MySqlCommand(query, conn);

            var users = new List<Dictionary<string, object>>();
            using var reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                var row = new Dictionary<string, object>();
                for (var i = 0; i < reader.FieldCount; i++)
                {
                    row[reader.GetName(i)] = reader.IsDBNull(i) ? null : reader.GetValue(i);
                }
                users.Add(row);
            }

            return JsonConvert.SerializeObject(users);
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: " + ex.ToString());
            return ex.ToString();
        }
    }
}
