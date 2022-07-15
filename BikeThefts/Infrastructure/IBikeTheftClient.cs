using CSharpFunctionalExtensions;

namespace Swapfiets.BikeThefts.Infrastructure
{
    public interface IBikeTheftClient
    {
        Task<Result<int>> GetBikeTheftsCount(string location, int cityRadiusInMiles);
    }
}
