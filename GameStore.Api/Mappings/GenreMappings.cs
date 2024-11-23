using System;
using GameStore.Api.Entities;
using GameStore.Api.Contracts;

namespace GameStore.Api.Mappings;

public static class GenreMappings
{
  public static GenreDetailsContract ToContract(this Genre genre)
  {
    return new(genre.Id, genre.Name);
  }

}
