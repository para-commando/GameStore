using System;

namespace GameStore.Api.Entities;

public class Game
{
public int id { get; set; }

public required string name { get; set; }

public int genreId { get; set; }

public Genre? Genre { get; set; }

public decimal price { get; set; }

public DateOnly releaseDate { get; set; }
}
