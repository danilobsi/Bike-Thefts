using CSharpFunctionalExtensions;
using Swapfiets.BikeThefts.Models;
using System.Web;

namespace Swapfiets.BikeThefts.Infrastructure
{
    public class BikeIndexClient : IBikeTheftClient
    {
        readonly ILogger<BikeIndexClient> _logger;
        readonly Uri _baseUri;

        static readonly HttpClient _client = new();

        public BikeIndexClient(ILogger<BikeIndexClient> logger, IConfiguration config)
        {
            _logger = logger;
            _baseUri = new Uri(config.GetValue<string>("BikeIndexUrl"));
        }

        public async Task<Result<int>> GetBikeTheftsCount(string location, int cityRadiusInMiles)
        {
            try
            {
                var encodedLocation = HttpUtility.UrlEncode(location);

                var requestUri = new Uri(_baseUri, $"search/count?location={encodedLocation}&distance={cityRadiusInMiles}");
                var requestResult = await _client.GetFromJsonAsync<BikeIndexSearchCountResponse>(requestUri);
                if (requestResult == null)
                {
                    return Result.Failure<int>($"No results found for the location '{location}'.");
                }

                return requestResult.Proximity;
            }
            catch (Exception ex)
            {
                const string errorMessage = "An error has ocurred while trying to retrieve the number of bike thefts.";
                _logger.LogError(ex, errorMessage);

                return Result.Failure<int>(errorMessage);
            }
        }
    }
}
