

using System;
using CatDataViewer;
using CatDataViewer.Models;
using Microsoft.Extensions.Configuration;


class Adoption
{
    // Attributes
    int adoptionID;
    public int CatID 
    {
        get{return CatID;}
        
        set
        {
            string cs = Program.Configuration.GetConnectionString("Catabase"); 
            var repo = new CatRepository(cs); 
            try
            {
                var c = repo.GetCat(value);
                if(c.Status == "Looking for a home")
                {
                    CatID = value;
                }
                else
                {
                    throw new UnavailableCatException();
                }
            }
            catch(UnknownIdException e)
            {
                Console.WriteLine(e.Message);
            }
            
            

        }
    }
   
    
    public int Fee {get;set;}
    public int AdopterID {get;set;}
    public required Date AdoptionDate{get;set;}
}