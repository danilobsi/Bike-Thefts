using CSharpFunctionalExtensions;

namespace Swapfiets.BikeThefts.Models
{
    public class SearchParameters
    {
        public int CityRadiusInMiles { get; init; } = 10;
        public string? City { get; init; }

        public Result IsValid()
        {
            if (string.IsNullOrEmpty(this.City))
            {
                return Result.Failure("City must be provided.");
            }

            return Result.Success();
        }
    }
}
