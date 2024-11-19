namespace GameStore.Api.Contracts;

public record class CreateGameContract
(
string name, string genre, decimal price, DateOnly ReleaseDate
);