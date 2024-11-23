using System.ComponentModel.DataAnnotations;

namespace GameStore.Api.Contracts;

public record class UpdateGameContract(
 [Required][StringLength(50)] string name, int genreId, [Range(0,100)]decimal price, DateOnly ReleaseDate
);
