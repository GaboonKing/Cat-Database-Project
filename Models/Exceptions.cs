using System;

// CONTAINS ALL CUSTOM EXCEPTIONS

class UnavailableCatException : Exception
{
    static string msg = "This cat is not available for adoption";
    public UnavailableCatException():base(msg){}
}
class UnknownIdException : Exception
{
    static string msg = "Unrecognized ID";
    public UnknownIdException():base(msg){}
}
class InvalidAdopterType : Exception
{
    static string msg = "Please enter a valid Adopter type";
    public InvalidAdopterType():base(msg){}
}



