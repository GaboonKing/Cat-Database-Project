using System.Collections.Generic;
using System.Dynamic;
using System.Runtime.CompilerServices;
using Avalonia.Platform;
using CatDataViewer.Models;
using Microsoft.VisualBasic;
using MySqlConnector;
using Tmds.DBus.Protocol;

public sealed class ZoneRepo
{
    static string connStr; // Connection string
    static ZoneRepo instance = null;

    public static ZoneRepo getInstance(string conn)
    {
        if(instance == null)
        {
            instance = new ZoneRepo(conn);
            return instance;
        }
        else
            return instance;
    }
    ZoneRepo(string s)
    {
        connStr = s;
    }
    static string getConn()
    {
        return connStr;
    }

    /*
    public struct Zone
    {
        
        int id; // ZoneID is only needed for identification purposes, the user will never see it
        string name;
        Zone(int id, string name)
        {
            this.id = id;
            this.name = name;
        }
    }
    */
    public List<Zone> SelectAll()
    {
        List<Zone> output = new List<Zone>();
        using var con = new MySqlConnection(connStr);
        con.Open();
        string query = "SELECT * FROM ZONE WHERE ImagePath IS NOT NULL";
        using var cmd = new MySqlCommand(query, con);
        using var reader = cmd.ExecuteReader();
        while(reader.Read())
        {
            int Id = reader.GetInt32("ZoneID");
            string Name = reader.GetString("ZoneName");
            string RawImagePath = reader.GetString("ImagePath");

            Zone z = new Zone(Id, Name, RawImagePath);
            output.Add(z);
        }
        return output;
    }
    public List<int> GetZoneIDs() // Only returns names
    {
        List<int> output = new List<int>();
        using var con = new MySqlConnection(connStr);
        con.Open();
        string query = "SELECT ZoneID from Zone";
        using var cmd = new MySqlCommand(query, con);
        using var reader = cmd.ExecuteReader();
        while(reader.Read())
            output.Add(reader.GetInt32("ZoneID"));
        con.Close();
        return output;
        

    }
    public List<string> SelectAllNames() // Only returns names
    {
        List<string> output = new List<string>();
        using var con = new MySqlConnection(connStr);
        con.Open();
        string query = "SELECT ZoneName from Zone";
        using var cmd = new MySqlCommand(query, con);
        using var reader = cmd.ExecuteReader();
        while(reader.Read())
            output.Add(reader.GetString("ZoneName"));
        con.Close();
        return output;
        

    }
    public static string GetName(int n)
    {
        string output = "";
        using var con = new MySqlConnection(connStr);
        con.Open();
        string query = $"SELECT ZoneName from Zone WHERE ZoneID = {n}";
        using var cmd = new MySqlCommand(query, con);
        using var reader = cmd.ExecuteReader();
        if(reader.Read())
            output = reader.GetString("ZoneName");
        con.Close();
        return output;
        
    }
    

    
}