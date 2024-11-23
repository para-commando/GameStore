namespace GameStore.Api.Contracts;

// record class mainly used for immutability and equality based on property values rather than reference as in classes
public record class GameDetailsContracts(int id, string name, int genreId, decimal price, DateOnly ReleaseDate);
