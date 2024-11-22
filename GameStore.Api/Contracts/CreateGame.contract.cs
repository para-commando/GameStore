using System.ComponentModel.DataAnnotations;

namespace GameStore.Api.Contracts;

// we will need endpoint filters to validate the incoming request properly especially on the below added data annotations, since in the older asp.net core versions the below annotations would work but for our minimal apis we will need support hence need to install
// $ dotnet add package MinimalApis.Extensions --version 0.11.0
// adding "WithParameterValidation()" at the end of a minimal api will trigger this validation, ofcourse there is a manual way but this is more convenient with packages

// note that this can even be added on group name initialization.
public record class CreateGameContract
(
[Required][StringLength(50)] string name, int genreId, [Required] string genre, [Range(0,100)]decimal price, DateOnly ReleaseDate
);