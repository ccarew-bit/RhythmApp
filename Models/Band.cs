using System;
using System.Collections.Generic;

namespace RhythmApp.Models
{
  public class Band
  {
    public int Id { get; set; }

    public string Name { get; set; }

    public string CountryOfOrigin { get; set; }

    public string NumberOfMembers { get; set; }
    public string Website { get; set; }

    public string Style { get; set; }

    public bool IsSigned { get; set; } = true;

    public string PersonOfContact { get; set; }

    public string ContactPhoneNumber { get; set; }

    public List<Album> Albums { get; set; } = new List<Album>();
  }
}