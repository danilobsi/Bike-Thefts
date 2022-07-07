using CSharpFunctionalExtensions;
using Microsoft.Extensions.Options;
using Swapfiets.BikeThefts.Infrastructure;
using Swapfiets.BikeThefts.Models;
using Swapfiets.BikeThefts.Settings;

namespace Swapfiets.BikeThefts.Services
{
    public class BikeTheftsService
    {
        readonly IBikeTheftClient _bikeTheftsProvider;
        readonly CitiesSettings _citiesSettings;

        public BikeTheftsService(IBikeTheftClient bikeTheftsProvider, IOptions<CitiesSettings> config)
        {
            _bikeTheftsProvider = bikeTheftsProvider;
            _citiesSettings = config.Value;
        }

        /// <summary>
        /// Retrieve the count of bike thefts in the provided city.
        /// </summary>
        /// <param name="city">The city to find the bike theft count</param>
        /// <param name="cityRadiusInMiles">The radius distance in miles to cover the bike theft's count, starting on the city center.</param>
        /// <returns>Bike theft's count</returns>
        public async Task<Result<string>> GetBikeTheftsCount(string city, int cityRadiusInMiles)
        {
            var countResult = await _bikeTheftsProvider.GetBikeTheftsCount(city, cityRadiusInMiles);
            if (countResult.IsFailure)
            {
                return Result.Failure<string>(countResult.Error);
            }

            return $"\"{city}\": {countResult.Value}"; 
        }

        /// <summary>
        /// Retrieve the information for the current cities in operation and the candidate cities, with their bike theft's count.
        /// </summary>
        /// <returns>Cities in operation and Candidate cities</returns>
        public async Task<CitiesResponse> GetBikeTheftsCountForCities()
        {
            var currentCities = GetBikeTheftsCountPerCities(_citiesSettings.Current);
            var candidateCities = GetBikeTheftsCountPerCities(_citiesSettings.Candidates);

            return new CitiesResponse
            {
                Current = await currentCities,
                Candidates = await candidateCities
            };
        }

        private async Task<Dictionary<string, object>> GetBikeTheftsCountPerCities(Dictionary<string, CitySettings> citiesDictionary)
        {
            const int defaultCityRadiusInMiles = 10;
            var currentCities = new Dictionary<string, object>();
            foreach (var city in citiesDictionary)
            {
                var latitude = city.Value.Latitude;
                var longitude = city.Value.Longitude;

                var countResult = await _bikeTheftsProvider.GetBikeTheftsCount($"{latitude},{longitude}", defaultCityRadiusInMiles);
                if (countResult.IsFailure)
                {
                    currentCities.Add(city.Key, countResult.Error);
                    continue;
                }

                currentCities.Add(city.Key, countResult.Value);
            }

            return currentCities;
        }
    }
}
