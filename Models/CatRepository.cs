using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Threading;



namespace CatDataViewer.Models;
public class CatRepository // SHOULD MAKE THIS A SINGLETON
{
    private readonly string connStr; // connection string
    public CatRepository(string connStr)
    {
        this.connStr = connStr;
    }

    List<Cat> RunQuery(MySqlDataReader reader)
    {
        var cats = new List<Cat>();
        while(reader.Read())
        {
            int id = reader.GetInt32("CatID");
            string name = reader.GetString("CatName");
            char gender = reader.GetChar("Gender");
            //int age = reader.GetInt32("Age");
            string status = reader.GetString("CatStatus");
            string bio = reader["Bio"] as string ?? "This cat doesn't have bio yet!";
            bool f = reader.GetBoolean("IsFixed");
            string tment = reader.GetString("Temperament"); // tment = temperament
            int zone = reader.GetInt32("ZoneID");
            string img_path = reader["ImagePath"] as string ?? "Assets/CatPhotos/test.jpg";
            
            

            Cat c = new Cat(id, name, gender, 0, status, f, tment, bio, zone, img_path);

            cats.Add(c);
        }
        return cats;
    }

    // Selection Queries
    public List<Cat> SelectAll() // returns all values within the cat table
    {
        var cats = new List<Cat>();
        using var conn = new MySqlConnection(connStr);
        conn.Open();
        string query = "SELECT * from cat";
        using var cmd = new MySqlCommand(query, conn);
        using var reader = cmd.ExecuteReader();
        cats = RunQuery(reader);
        return cats;
    }

    public List<Cat> GetFeral() // same logic as the SelectAll method, different query
    {
        var cats = new List<Cat>();
        using var conn = new MySqlConnection(connStr);
        conn.Open();
        string query = "SELECT * FROM cat WHERE CatStatus = 'Feral'";
        using var cmd = new MySqlCommand(query, conn);
        using var reader = cmd.ExecuteReader();
        cats = RunQuery(reader);
        return cats;
    }
    public List<Cat> GetAdopted() // same logic as the SelectAll method, different query
    {
        var cats = new List<Cat>();
        using var conn = new MySqlConnection(connStr);
        conn.Open();
        string query = "SELECT * FROM cat WHERE CatStatus = 'Adopted'";
        using var cmd = new MySqlCommand(query, conn);
        using var reader = cmd.ExecuteReader();
        while(reader.Read())
        {
            int id = reader.GetInt32("CatID");
            string name = reader.GetString("CatName");
            char gender = reader.GetChar("Gender");
            int age = reader.GetInt32("Age");
            string status = reader.GetString("CatStatus");
            string bio = reader.GetString("Bio");
            bool f = reader.GetBoolean("IsFixed");
            string tment = reader.GetString("Temperament"); // tment = temperament
            int zone = reader.GetInt32("ZoneID");
            string img_path = reader["ImagePath"] as string ?? "Assets/CatPhotos/Test.jpg";

            Cat c = new Cat(id, name, gender, age, status, f, tment, bio, zone, img_path);

            cats.Add(c);
        }

        return cats;
    }
    public List<Cat> GetCatsByZone(int zID) // same logic as the SelectAll method, different query
    {
        var cats = new List<Cat>();
        using var conn = new MySqlConnection(connStr);
        conn.Open();
        string query = "SELECT * FROM cat WHERE ZoneID = @zID";
        using var cmd = new MySqlCommand(query, conn);
        cmd.Parameters.AddWithValue("@zID", zID);
        using var reader = cmd.ExecuteReader();
        cats = RunQuery(reader);
        return cats;
    }
    public List<string> GetImages()
    {
        var Images = new List<string>();
        using var con = new MySqlConnection(connStr);
        con.Open();
        string query = "SELECT ImagePath from cat";
        using var cmd = new MySqlCommand(query, con);
        var reader = cmd.ExecuteReader();
        while(reader.Read())
        {
            string img_path = reader["ImagePath"] as string ?? "Assets/CatPhotos/Test.jpg";
            Images.Add($"avares://CatDataViewer/{img_path}");
            Console.WriteLine(img_path);
        }
        return Images;
    }
    public Cat GetCat(int id)
    {
        string id_str = id.ToString();
        using var conn = new MySqlConnection(connStr);
        conn.Open();
        string query = $"SELECT * FROM cat where CatID = {id_str}";
        using var cmd = new MySqlCommand(query, conn);
        using var reader = cmd.ExecuteReader();
        if(reader.Read())
        {
            string name = reader.GetString("CatName");
            char gender = reader.GetChar("Gender");
            string status = reader.GetString("CatStatus");
            string bio = reader.GetString("Bio");
            bool f = reader.GetBoolean("IsFixed");
            string tment = reader.GetString("Temperament"); // tment = temperament
            int zone = reader.GetInt32("ZoneID");
            string img_path = reader["ImagePath"] as string ?? "Assets/CatPhotos/Test.jpg";

            Cat c = new Cat(id, name, gender, 0, status, f, tment, bio, zone, img_path);
            return c;
        }
        else
        {
            throw new UnknownIdException();
        }

    }
    public bool DoesCatExist(int id)
    {
        string id_str = id.ToString();
        using var conn = new MySqlConnection(connStr);
        conn.Open();
        string query = $"SELECT * FROM cat where CatID = {id_str}";
        using var cmd = new MySqlCommand(query, conn);
        using var reader = cmd.ExecuteReader();
        if(reader.Read())
        {
            return true;
        }
        return false;
    }

    public void AddCat(Cat c)
    {
        using var conn = new MySqlConnection(connStr);
        conn.Open();
        string insertion = $"insert into cat values ({c.Id}, {c.Name}, {c.Gender}, {c.Age}, {c.Status}, {c.IsFixed}, {c.Temperament}, {c.z_ID})";
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