using System;
using RhythmApp.Models;
using System.Linq;

namespace RhythmApp
{
  class Program
  {
    static void Main(string[] args)
    {
      static void PopulateDatabase()
      {
        Console.WriteLine("Welcome to my Record Label!");
        var db = new DatabaseContext();
        if (!db.Bands.Any())
        {
          // if none then add a few
          db.Bands.Add(new Band
          {
            Name = "Dave Matthews Band",
            CountryOfOrigin = "America",
            NumberOfMembers = "6",
            Website = "www.davematthewsband.com",
            Style = "alternative",
            IsSigned = true,
            PersonOfContact = "boyd",
            ContactPhoneNumber = "3212083338",
          });
          db.Bands.Add(new Band
          {
            Name = "N'sync",
            CountryOfOrigin = "America",
            NumberOfMembers = "5",
            Website = "www.nsync.com",
            Style = "pop",
            IsSigned = true,
            PersonOfContact = "Justin",
            ContactPhoneNumber = "5555555555",
          });
          db.Bands.Add(new Band
          {
            Name = "Backstreet Boys",
            CountryOfOrigin = "America",
            NumberOfMembers = "5",
            Website = "www.backstreetboys.com",
            Style = "pop",
            IsSigned = true,
            PersonOfContact = "Nick Carter",
            ContactPhoneNumber = "6666666666",
          });
          db.SaveChanges();
        }
        var newBand = new Band();
        Console.WriteLine("would you like to (sign) a band, (produce) an album, let a band (go), (resign) a band or (view) a band?");
        var input = Console.ReadLine().ToLower();

        if (input == "sign")
        {
          Console.WriteLine($"What would you like to call your band?");
          newBand.Name = Console.ReadLine();
          Console.WriteLine($"Where is your band from?");
          newBand.CountryOfOrigin = Console.ReadLine();
          Console.WriteLine($"How many members are in this band?");
          newBand.NumberOfMembers = Console.ReadLine();
          Console.WriteLine("Does your band have a website? if so, please insert below. if not, type no.");
          newBand.Website = Console.ReadLine();
          Console.WriteLine("What style is your band");
          newBand.Style = Console.ReadLine();
          Console.WriteLine("is your band signed already? for yes type (true) for no type (false).");
          newBand.IsSigned = bool.Parse(Console.ReadLine());
          Console.WriteLine("Who should we contact in regards to this band?");
          newBand.PersonOfContact = Console.ReadLine();
          Console.WriteLine($"what is {newBand.PersonOfContact}'s phone number");
          newBand.ContactPhoneNumber = Console.ReadLine();
          db.Bands.Add(newBand);
          db.SaveChanges();
        }
        else if (input == "go")
        {
          Console.WriteLine("Which Band would you like to let go?");
          var band = Console.ReadLine();

          var bandToUnsign = db.Bands.FirstOrDefault(b => b.Name == band);

          if (bandToUnsign != null)
          {
            bandToUnsign.IsSigned = false;
            Console.WriteLine($"you have let go {band}");
            db.Bands.Update(bandToUnsign);
            db.SaveChanges();
          }
          else
          {
            Console.WriteLine("band not shreddin' at the moment.");
          }
        }
        else if (input == "produce")
        {
          Console.WriteLine("Which band is making an album?");

        }
      }
      PopulateDatabase();
    }
  }
}