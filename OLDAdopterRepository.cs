/*
using MySqlConnector;
using System;
using System.Collections.Generic;


class AdopterRepository
{
    private readonly string connStr; // connection string
    public AdopterRepository(string connStr)
    {
        this.connStr = connStr;
    }

    public List<Adopter> SelectAll() // returns all values within the Adopter table
    {
        var adopters = new List<Adopter>();
        using var conn = new MySqlConnection(connStr);
        conn.Open();
        string query = "SELECT * from adopter";
        using var cmd = new MySqlCommand(query, conn);
        using var reader = cmd.ExecuteReader();
        while(reader.Read())
        {
            // collect the values of each row

            int id = reader.GetInt32("AdopterID");
            string fname = reader.GetString("Adopter_FName");
            string lname = reader.GetString("Adopter_LName");
            
            int age = reader.GetInt32("Age");
            string status = reader.GetString("AdopterStatus");
            
            string type = reader.GetString("AdopterType"); // tment = temperament
        

            Adopter A = new Adopter(id, fname, lname, age, type, status );

            adopters.Add(A);
        }
        conn.Close();
        return adopters;
    }

    public List<Adopter> SelectWaitListed() // returns all values within the Adopter table
    {
        var adopters = new List<Adopter>();
        using var conn = new MySqlConnection(connStr);
        conn.Open();
        string query = "SELECT * from adopter where AdopterStatus = waitlist";
        using var cmd = new MySqlCommand(query, conn);
        using var reader = cmd.ExecuteReader();
        while(reader.Read())
        {
            // collect the values of each row

            int id = reader.GetInt32("AdopterID");
            string fname = reader.GetString("Adopter_FName");
            string lname = reader.GetString("Adopter_LName");
            
            int age = reader.GetInt32("Age");
            string status = reader.GetString("AdopterStatus");
            
            string type = reader.GetString("AdopterType"); // tment = temperament
        

            Adopter A = new Adopter(id, fname, lname, age, type, status );

            adopters.Add(A);
        }
        conn.Close();
        return adopters;
    }

    public void AddAdopter(Adopter c)
    {
        using var conn = new MySqlConnection(connStr);
        conn.Open();
        string insertion = $"insert into Adopter values ({c.PersonID}, {c.F_Name}, {c.L_Name}, {c.Status}, {c.Type}, {c.Age})";
        try
        {
            using var cmd = new MySqlCommand(insertion, conn);
            cmd.ExecuteNonQuery();
        }
        catch(Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }
}
*/