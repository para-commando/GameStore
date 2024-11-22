
using GameStore.Api.Contracts;
using GameStore.Api.Entities;

namespace GameStore.Api.Mappings;

public static class GamesMapping
{
  public static Game ToEntity(this CreateGameContract gameContract)
  {
    return new Game()
    {
      name = gameContract.name,
      genreId = gameContract.genreId,
      price = gameContract.price,
      releaseDate = gameContract.ReleaseDate
    };
  }

  public static GameContracts ToContract(this Game game)
  {
    // the type of the constructor is inferred from th return type of this method
   return new (
      game.id,
      game.name,
      game.genre!.Name,
      game.price,
      game.releaseDate
    );
  }
}
