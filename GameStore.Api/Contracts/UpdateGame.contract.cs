namespace GameStore.Api.Contracts;

public record class UpdateGameContract(
 string name, string genre, decimal price, DateOnly ReleaseDate
);
