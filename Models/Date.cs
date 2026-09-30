using System;
public class Date
{
    //Attributes
    private int day;
    private bool validDay = false; // flags to keep track of format validity so we don't have to recalculate when displaying
    private int month;
    private bool validMonth;
    private int year;
    private bool validYear;

    private bool isDateValid()
    {
        return validDay && validMonth && validYear;
    }

    private int CountDigits(int n) // Method to get number of digits in year
    {
        int numDigits = 0;
        while(n != 0)
        {
            n /= 10;
            numDigits++;
        }
        return numDigits;
    }

    //Constructors
    public Date():this(1, 1, 2026) // Default
    {
    
    }
    public Date(int month, int day, int year)
    {
        this.day = day;
        if(day >= 1 && day <= 31)
        {
            validDay = true;
        }
        else
        {
            Console.WriteLine("Invalid day");
            // We don't need to set the flags to false in the constructor
            // since false is their default value
            
        }
        this.month = month;
        if(month >= 1 && month <= 12)
        {
            validMonth = true;
        }
        else
        {
            Console.WriteLine("Invalid month");
        }

        this.year = year;

        // Check how many digits year has
        int numDigits = CountDigits(year);
        
        if(numDigits == 4)
            validYear = true;
        else
        {
            Console.WriteLine("Year must be 4 digits");
        }
    }

    // Properties
    public int Day
    {
        set
        {
            day = value;
            if(day >= 1 && day <= 31)
            {
                validDay = true;
            }
            else
            {
                Console.WriteLine("Invalid day");
                validDay = false; // setting is false is required here since a valid value could be changed to an invalid one
            }
        }
        get
        {
            return day;
        }
    }
    public int Month
    {
        set
        {
            month = value;
            if(month >= 1 && month <= 12)
            {
                validMonth = true;
            }
            else
            {
                Console.WriteLine("Invalid month");
                validMonth = false;
            }
        }
        get
        {
            return month;
        }
    }
    public int Year
    {
        set
        {
            year = value;
            if(CountDigits(year) == 4)
            {
                validYear = true;
            }
            else
            {
                Console.WriteLine("Year must be 4 digits");
                validYear = false;
            }
        }
        get
        {
            return year;
        }
    }

    // Display Methods
    public string DisplayUSFormat() 
    {
        
        
        if(isDateValid()) // Check if date can be output based off the flags
           return $"{month:00}/{day:00}/{year}";
        
        return "Invalid date";
        
        
    }
    public string DisplayIntlFormat()
    {
        if(isDateValid()) // Check if date can be output 
            return $"{year}-{month:00}-{day:00}";
        
        return "Invalid date";
        
    }

    public static bool operator ==(Date d1, Date d2)
    {
        if(!d1.isDateValid() || !d2.isDateValid())
        {
            return false;
        }
        
        return d1.year == d2.year && d1.month == d2.month && d1.day == d2.day;
    }
    public static bool operator !=(Date d1, Date d2)
    {
        if(!d1.isDateValid() || !d2.isDateValid())
        {
            return false;
        }
        return d1.year != d2.year || d1.month != d2.month ||  d1.day != d2.day;
    }
    public static bool operator <(Date d1, Date d2)
    {
        if(!(d1.isDateValid() && d2.isDateValid())) // check if date is invalid
            return false;
        if(d1.year < d2.year)
            return true;
        if(d1.month < d2.month && d1.year == d2.year) // ensure year isn't bigger so we don't return true on case like 1/27/2009 < 1/27/2008 
            return true;
        if(d1.day < d2.day  && d1.year == d2.year && d1.month == d2.month) //ensure year and month aren't bigger
            return true;
        return false;
        
    }
    public static bool operator >(Date d1, Date d2)
    {
        if(!(d1.isDateValid() && d2.isDateValid()))
            return false;
        if(d1.year > d2.year)
            return true;
        if(d1.month > d2.month && d1.year == d2.year)
            return true;
        if(d1.day > d2.day  && d1.year == d2.year && d1.month == d2.month) //ensure year and month aren't bigger
            return true;
        return false;
    }

    public static bool operator <=(Date d1, Date d2)
    {
        if(!(d1.isDateValid() && d2.isDateValid()))
            return false;
        if(d1.year > d2.year)
            return false;
        if(d1.month > d2.month && d1.year == d2.year)
            return false;
        if(d1.day > d2.day  && d1.year == d2.year && d1.month == d2.month)
            return false;
        return true;
        
    }
    public static bool operator >=(Date d1, Date d2) 
    {
        if(!(d1.isDateValid() && d2.isDateValid()))
            return false;
        if(d1.year < d2.year)
            return false;
        if(d1.month < d2.month && d1.year == d2.year)
            return false;
        if(d1.day < d2.day  && d1.year == d2.year && d1.month == d2.month)
            return false;
        return true;
    }

    public static explicit operator string(Date d)
    {
        return $"{d.month}/{d.day}/{d.year}";
    } 
    public static implicit operator Date(string s)
    {
        //Console.WriteLine("Converting from string to date.");
        Date d = new Date();
        string[] pieces = s.Split('/');
        if(pieces.Length != 3)
        {
            throw new InvalidOperationException("Date must consist of 3 / seperated numbers");
        }
        
        d.month = Convert.ToInt32(pieces[0]);
        d.day = Convert.ToInt32(pieces[1]);
        d.year = Convert.ToInt32(pieces[2]);

        if(!d.isDateValid())
            throw new InvalidOperationException("Invalid date");

        return d;
    }
    
    public override bool Equals(object obj)
    {
        return true;
    }
    
    // override object.GetHashCode
    public override int GetHashCode()
    {
        // TODO: write your implementation of GetHashCode() here
       return 0;
    }

    


}