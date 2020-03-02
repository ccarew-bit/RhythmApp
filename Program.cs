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
        var newAlbum = new Album();
        Console.WriteLine("would you like to (sign) a band, (produce) an album, let a band (go), (resign) a band or (view) a band?");
        var input = Console.ReadLine().ToLower();

        if (input == "sign")
        {
          Console.WriteLine($"What would you like to call your Band?");
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
          Console.WriteLine("would you like to view by ");
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
        else if (input == "resign")
        {
          Console.WriteLine("Which Band would you like to sign?");
          var band = Console.ReadLine();

          var bandToResign = db.Bands.FirstOrDefault(b => b.Name == band);

          if (bandToResign != null)
          {
            bandToResign.IsSigned = true;
            Console.WriteLine($"you have resigned {band}");
            db.Bands.Update(bandToResign);
            db.SaveChanges();
          }
          else
          {
            Console.WriteLine("band not shreddin' at the moment.");
          }
        }
        else if (input == "view")
        {
          Console.WriteLine("Which would you like to view? bands that are (signed), bands that are (not signed), a bands (albums), (songs) on an album or album by (release)?");
          var view = Console.ReadLine().ToLower();
          if (view == "signed")
          {
            var signedBands = db.Bands.Where(b => b.IsSigned == true);
            foreach (var signed in signedBands)
            {
              Console.WriteLine($"{signed.Name} is signed");
            }
          }
          else if (view == "not signed")
          {
            var notSignedBands = db.Bands.Where(b => b.IsSigned == false);
            foreach (var notSigned in notSignedBands)
            {
              Console.WriteLine($"{notSigned.Name} is not signed");
            }
          }
          else if (view == "albums")
          {
            Console.WriteLine($"what band would you like to view albums from?");
            var bands = db.Bands.OrderBy(b => b.Name);
            foreach (var band in bands)
            {
              Console.WriteLine($"{band.Id} : {band.Name}");

            }
            var bandId = int.Parse(Console.ReadLine());
            var albums = db.Albums.Where(a => bandId == a.BandId);
            foreach (var album in albums)
            {
              Console.WriteLine($"{album.Title}");
            }

          }
          else if (view == "songs")
          {
            Console.WriteLine($"what album would you like to view songs from?");
            var albums = db.Albums.OrderBy(b => b.Title);
            foreach (var album in albums)
            {
              Console.WriteLine($"{album.Id} : {album.Title}");
            }
            var albumId = int.Parse(Console.ReadLine());
            var songs = db.Songs.Where(a => albumId == a.AlbumId);
            foreach (var song in songs)
            {
              Console.WriteLine($"{song.Title}");
            }
          }
          else if (view == "release")
          {
            var orderedAlbums = db.Albums.OrderBy(a => a.ReleaseDate);
            foreach (var order in orderedAlbums)
            {
              Console.WriteLine($"{order.Title} was releases {order.ReleaseDate} ");

            }
          }
        }
        else if (input == "produce")
        {
          Console.WriteLine("would you like to (create) a new album or (add) a song to an album?");
          var addOrCreate = Console.ReadLine().ToLower();
          if (addOrCreate == "create")
          {
            Console.WriteLine($"What would you like to call your Album?");
            newAlbum.Title = Console.ReadLine();
            Console.WriteLine($"Is this Album Explicit? type (true) for yes and (false) for no.");
            newAlbum.IsExplicit = bool.Parse(Console.ReadLine());
            Console.WriteLine($"these are the bands that are signed.");
            var band = db.Bands.OrderBy(b => b.Id);
            var signedBands = db.Bands.Where(b => b.IsSigned == true);
            foreach (var signed in signedBands)
            {
              Console.WriteLine($"{signed.Id} : {signed.Name} is signed");
            }
            Console.WriteLine("which band is making this album? ");
            newAlbum.BandId = int.Parse(Console.ReadLine());
            newAlbum.ReleaseDate = DateTime.Now;
            db.Albums.Add(newAlbum);
            db.SaveChanges();
          }
          else if (addOrCreate == "add")
          {
            var newSong = new Song();
            var albumToView = db.Albums.OrderBy(a => a.Title);
            foreach (var album in albumToView)
            {
              Console.WriteLine($"{album.Title}:{album.Id}");
            }
            Console.WriteLine("which album would you like to add a song to?");
            newSong.AlbumId = int.Parse(Console.ReadLine());
            Console.WriteLine($"what are the lyrics in your song?");
            newSong.Lyrics = Console.ReadLine();
            Console.WriteLine($"how long is this song?");
            newSong.Length = Console.ReadLine();
            Console.WriteLine($"what Genre is this song?");
            newSong.Genre = Console.ReadLine();
            db.Songs.Add(newSong);
            db.SaveChanges();

          }
        }
      }
      PopulateDatabase();
    }
  }
}