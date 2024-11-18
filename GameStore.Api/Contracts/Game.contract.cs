namespace GameStore.Api.Contracts;

// record class mainly used for immutability and equality based on property values rather than reference as in classes
public record class GameContracts(int id, string name, string genre, decimal price, DateOnly ReleaseDate);
