
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

  public static GameSummaryContracts ToEntity(this Game game)
  {
    // the type of the constructor is inferred from th return type of this method
    // even though the type of genre in Game entity is of Genre and we in the below code assigning it game.genre!.Name and it is working because since Name is a property of Genre object. see this js example
    //     const genre = {
    //   id: 1,
    //   name: "Adventure"
    // };

    // const game = {
    //   id: 1,
    //   name: "Halo",
    //   genre: genre // genre is an object
    // };

    // console.log(game.genre.name); // Accessing the "name" property of the "genre" object

    return new(
       game.id,
       game.name,
       game.genre!.Name,
       game.price,
       game.releaseDate
     );
  }

  // extension method on the Game entity defined
  public static GameDetailsContracts ToCGameDetailsContracts(this Game game)
  {
    // the type of the constructor is inferred from th return type of this method
    return new(
       game.id,
       game.name,
       game.genreId,
       game.price,
       game.releaseDate
     );
  }

  public static Game ToUpdateGameDetailsContracts(this UpdateGameContract game, int id)
  {
    // the constructor for Game Entity must be created like this.
    return new Game() {
      id = id,
     name=  game.name,
     genreId =  game.genreId,
     price =  game.price,
     releaseDate = game.ReleaseDate
    };
  }
}
