namespace Swapfiets.BikeThefts.Models
{
    public class CitiesResponse
    {
        public Dictionary<string, object> Current { get; init; }
        public Dictionary<string, object> Candidates { get; init; }
    }
}
