namespace Swapfiets.BikeThefts.Settings
{
    public class CitiesSettings
    {
        public Dictionary<string, CitySettings> Current { get; init; }
        public Dictionary<string, CitySettings> Candidates { get; init; }
    }
}
