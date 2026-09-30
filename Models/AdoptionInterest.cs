using CatDataViewer;
using CatDataViewer.Models;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.Configuration;
using MySqlConnector;
using System;

public class AdoptionInterest
{
    AdopterRepo adopterRepo = new AdopterRepo(Program.Configuration.GetConnectionString("Catabase"));
    CatRepository catRepo = new CatRepository(Program.Configuration.GetConnectionString("Catabase"));

    private int adopterID;
    private int catID;

    public AdoptionInterest(){}
    
    public AdoptionInterest(int cID, int aID)
    {
        AdopterID = aID;
        CatID = cID;

        if(AdopterID == aID && CatID == cID && !adopterRepo.DoesInterestExist(aID, cID)) // if the assignment was valid
        {
            using var con = new MySqlConnection(Program.Configuration.GetConnectionString("Catabase"));
            con.Open();
            string query = "INSERT INTO AdoptionInterest (CatID, AdopterID) VALUES (@c, @a)";
            using var cmd = new MySqlCommand(query, con);
            cmd.Parameters.AddWithValue("@c", CatID);
            cmd.Parameters.AddWithValue("@a", AdopterID);
            var reader = cmd.ExecuteNonQuery();
            //Console.WriteLine("Interest expressed");
        }
        
    }
    public int AdopterID
    {
        get => adopterID;
        
        set
        {
            try
            {
            if(adopterRepo.DoesAdopterExist(value))
                adopterID = value;
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            
        }
    }
    public int CatID
    {
        get => catID;
        set
        {
            if(catRepo.DoesCatExist(value))
                catID = value;
            else
            {
                Console.WriteLine("Unknown cat id");
            }
        }
    }

    public void DeleteInterest() // Removes the entry from the database
    {
        if(adopterRepo.DoesInterestExist(AdopterID, CatID))
        {
            using var con = new MySqlConnection(Program.Configuration.GetConnectionString("Catabase"));
            con.Open();
            string query = "DELETE FROM AdoptionInterest WHERE AdopterID = @uID AND CatID = @cID";
            using var cmd = new MySqlCommand(query, con);
            cmd.Parameters.AddWithValue("@uID", AdopterID);
            cmd.Parameters.AddWithValue("@cID", CatID);
            if(cmd.ExecuteNonQuery() == 1)
            {
                Console.WriteLine("Record deleted");
            }

        }
    }

    /*
    public void AddtoDB()
    {
        using var con = new MySqlConnection(Program.Configuration.GetConnectionString("Catabase"));
        string query = "INSERT INTO AdoptionInterest (CatID, AdopterID) VALUES (@c, @a)";
        using var cmd = new MySqlCommand(query, con);
        cmd.Parameters.AddWithValue("@c", CatID);
        cmd.Parameters.AddWithValue("@a", AdoptionID);
        var reader = cmd.ExecuteNonQuery();
        Console.WriteLine("Interest expressed");

    }
    */
}