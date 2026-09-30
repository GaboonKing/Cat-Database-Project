using MySqlConnector;
using System;
using CatDataViewer.Models;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Avalonia.Data;
using System.Text;
using System.Reflection.Metadata;

class AdopterRepo
{
    private string connStr;

    public AdopterRepo(string s)
    {
        connStr = s;
    }

    List<Adopter> RunAdopterQuery(MySqlDataReader reader)
    {
        var adopters = new List<Adopter>();
        while(reader.Read())
        {
            int id = reader["AdopterID"] == DBNull.Value ? 0: reader.GetInt32("AdopterID");
            string fname = reader["Adopter_FName"] == DBNull.Value ? "" : reader.GetString("Adopter_FName");
            string lname = reader["Adopter_LName"] == DBNull.Value ? "" : reader.GetString("Adopter_LName");
            string status = reader["AdopterStatus"] == DBNull.Value ? "" : reader.GetString("AdopterStatus");
            string type = reader["AdopterType"] == DBNull.Value ? "" : reader.GetString("AdopterType");
            int age = reader["age"] == DBNull.Value ? 0 : reader.GetInt32("Age");
            string p = reader.GetString("pword");
            string e = reader.GetString("email");

            Adopter a = new Adopter(id, fname, lname, age, type, status, e, p);
            adopters.Add(a);
        }
        return adopters;
    }
    Adopter RunSingleAdopterQuery(MySqlDataReader reader) // method for queries that should only return one row
    {
        Adopter a = new Adopter();
        if(reader.Read())
        {
            int id = reader.GetInt32("AdopterID");
            string fname = reader["Adopter_FName"] == DBNull.Value ? "" : reader.GetString("Adopter_FName");
            string lname = reader["Adopter_LName"] == DBNull.Value ? "" : reader.GetString("Adopter_LName");
            string status = reader["AdopterStatus"] == DBNull.Value ? "" : reader.GetString("AdopterStatus");
            string type = reader["AdopterType"] == DBNull.Value ? "" : reader.GetString("AdopterType");
            int age = reader["age"] == DBNull.Value ? 0 : reader.GetInt32("Age");
            string p = reader.GetString("pword");
            string e = reader.GetString("email");
            Console.WriteLine(id);

            a = new Adopter(id, fname, lname, age, type, status, e, p);
            return a;
           
        }
        return null;
    }

    public List<Adopter> SelectAll()
    {
        using var conn = new MySqlConnection(connStr);
        conn.Open();
        string query = "SELECT * FROM ADOPTER";
        using var cmd = new MySqlCommand(query, conn);
        using var reader = cmd.ExecuteReader();
        var adopters = RunAdopterQuery(reader);
        return adopters;
    }

    public Adopter FindAdopter(string email, string pword)
    {

        try
        {
            using var conn = new MySqlConnection(connStr);
            conn.Open();
            string query = "SELECT * FROM ADOPTER WHERE email = @email AND pword = @pword";
            using var cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@email", email);
            cmd.Parameters.AddWithValue("@pword", pword);
            using var reader = cmd.ExecuteReader();
            var adopter = RunSingleAdopterQuery(reader);

            if(adopter == null)
                throw new UnknownIdException();

            return adopter;
        }
        
        catch(MySqlException)
        {
            throw new UnknownIdException();
        }
        
        
        
    }
    public Adopter FindAdopter(int id)
    {
        try
        {
            using var conn = new MySqlConnection(connStr);
            conn.Open();
            string query = "SELECT * FROM ADOPTER WHERE AdopterID = @AdopterID";
            using var cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@AdopterID", id);
            using var reader = cmd.ExecuteReader();
            var adopter = RunSingleAdopterQuery(reader);

            return adopter;

        }
        catch(MySqlException)
        {
            throw new UnknownIdException();
        }
    }
    public bool IsEmailTaken(string email) // checks if an account with a certain email exists
    {
        try
        {
            using var conn = new MySqlConnection(connStr);
            conn.Open();
            string query = "SELECT email FROM ADOPTER WHERE email = @email";
            using var cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@email", email);
            var reader = cmd.ExecuteScalar();
            if(reader != null && reader != DBNull.Value)
            {
                return true;
            }
            return false;
        }
        catch(MySqlException e)
        {
            Console.WriteLine($"Error: {e.Message}");
        }
        return true;
    }
    public bool DoesAdopterExist(int id)
    {
        using var conn = new MySqlConnection(connStr);
        conn.Open();
        string query = "SELECT AdopterID FROM ADOPTER WHERE AdopterID = @id";
        using var cmd = new MySqlCommand(query, conn);
        cmd.Parameters.AddWithValue("@id", id);
        var reader = cmd.ExecuteScalar();
        if(reader != null && reader != DBNull.Value)
            return true;
        return false;
    }
    


    // INSERTION QUERIES
    public void CreateAdopter(string email, string password) // Base creation query that runs on initial profile creation, only adding the required columns
    {
        try
        {
            using var conn = new MySqlConnection(connStr);
            conn.Open();
            string query = "INSERT INTO ADOPTER (email, pword) VALUES (@email, @password)";
            using var cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@email", email);
            cmd.Parameters.AddWithValue("@password", password);
            var n = cmd.ExecuteNonQuery();
            Console.WriteLine($"Adopter succesfully created, {n}");
        }
        catch(MySqlException e)
        {
            Console.WriteLine(e.Message);
        }
    }
    public void UpdatePersonalDetails(string fname, string lname, string type, string email, int id)
    {
        Console.WriteLine("UPD called");
        string query = "UPDATE ADOPTER SET Adopter_FName = @fname, Adopter_LName = @lname, AdopterType = @type, Email = @email WHERE AdopterID = @id";
        try
        {
            using var conn = new MySqlConnection(connStr);
            conn.Open();
            using (MySqlCommand cmd = new MySqlCommand(query, conn))
            {
                cmd.Parameters.AddRange(new MySqlParameter[]
                {
                    new MySqlParameter("@fname", fname),
                    new MySqlParameter("@lname", lname),
                    new MySqlParameter("@type", type),
                    new MySqlParameter("@email", email),
                    new MySqlParameter("@id", id)

                });

                var n = cmd.ExecuteNonQuery();
                Console.WriteLine($"Adopter updated,{n}");

            };

            
            
        }
        catch(MySqlException e)
        {
            Console.WriteLine(e.Message);
        }
    }

    // INTERESTS
    public List<AdoptionInterest> GetInterests(int UserID)
    {
        List<AdoptionInterest> list = new List<AdoptionInterest>();
        using var con = new MySqlConnection(connStr);
        con.Open();
        string query = "SELECT * from AdoptionInterest where AdopterID = @id";
        using var cmd = new MySqlCommand(query, con);
        cmd.Parameters.AddWithValue("@id", UserID);
        var reader = cmd.ExecuteReader();
        while(reader.Read())
        {
            AdoptionInterest ar = new AdoptionInterest();
            ar.AdopterID = reader.GetInt32("AdopterID");
            ar.CatID = reader.GetInt32("CatID");
            list.Add(ar);
        }
        return list;
    }
    public bool DoesInterestExist(int UserID, int CatID)
    {
        using var con = new MySqlConnection(connStr);
        con.Open();
        string query = "SELECT * from AdoptionInterest where AdopterID = @uID AND CatID = @cID";
        using var cmd = new MySqlCommand(query, con);
        cmd.Parameters.AddWithValue("@uID", UserID);
        cmd.Parameters.AddWithValue("@cID", CatID);
        var reader = cmd.ExecuteScalar();
        if(reader != null && reader != DBNull.Value)
            return true;
        return false;

    }

    

    /* UNIVERSAL QUERY BUILDER WIP - NEEDLESSY COMPLICATED FOR WHATS NEEDED
    public void UpdateAdopter(Dictionary<string,string> attributes, Adopter a)
    {
        StringBuilder query = new StringBuilder("UPDATE ADOPTER SET");
        int remaining = attributes.Count; // tracks how many pairs we've looked at
        foreach(var kvp in attributes)
        {
            remaining--;
            if(remaining == 0) // if we are on the last pair, we forgoe adding the comma
                query.AppendJoin("=", kvp);
            else
            {
                query.AppendJoin("=", kvp);
                query.Append(", ");

            }
        }
        query.Append($" WHERE AdopterID = {a.PersonID}");

        
        string q = query.ToString(); // Convert from string builder to string
        try
        {
            using var conn = new MySqlConnection(connStr);
            conn.Open();
            using var cmd = new MySqlCommand(q, conn);
            var n = cmd.ExecuteNonQuery();
            Console.WriteLine($"Adopter succesfully updated, {n}");
        }
        catch(MySqlException e)
        {
            Console.WriteLine(e.Message);
        }

    }
    */
}