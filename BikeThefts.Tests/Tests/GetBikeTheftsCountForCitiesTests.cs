using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Swapfiets.BikeThefts.Models;
using Swapfiets.BikeThefts.Settings;
using Swapfiets.BikeThefts.Tests.Stubs;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Swapfiets.BikeThefts.Tests.Tests
{
    public class GetBikeTheftsCountForCitiesTests
    {
        [Fact]
        public async Task Ok()
        {
            //Arrange
            const int theftsCount = 1;
            var citiesSettings = new CitiesSettings
            {
                Candidates = new Dictionary<string, CitySettings>
                {
                    ["Milan"] = new CitySettings { Latitude = "1.0", Longitude = "1.0" }
                },
                Current = new Dictionary<string, CitySettings>
                {
                    ["Amsterdam"] = new CitySettings { Latitude = "1.0", Longitude = "1.0" }
                }
            };
            var expectedResponse = new CitiesResponse
            {
                Candidates = new Dictionary<string, object>
                {
                    ["Milan"] = 1
                },
                Current = new Dictionary<string, object>
                {
                    ["Amsterdam"] = 1
                }
            };
            var sut = BikeTheftsControllerStub.Create(theftsCount, citiesSettings);

            //Act
            var objectResult = await sut.GetBikeTheftsCountForCities() as OkObjectResult;

            //Assert
            Assert.NotNull(objectResult);
            Assert.Equal(
                JsonConvert.SerializeObject(expectedResponse), 
                JsonConvert.SerializeObject(objectResult.Value));
        }

        [Fact]
        public async Task Ok_WithCityErrors() 
        {
            //Arrange
            var theftsCountResult = Result.Failure<int>("Not found");
            var citiesSettings = new CitiesSettings
            {
                Candidates = new Dictionary<string, CitySettings>
                {
                    ["Milan"] = new CitySettings { Latitude = "1.0", Longitude = "1.0" }
                },
                Current = new Dictionary<string, CitySettings>
                {
                    ["Amsterdam"] = new CitySettings { Latitude = "1.0", Longitude = "1.0" }
                }
            };
            var expectedResponse = new CitiesResponse
            {
                Candidates = new Dictionary<string, object>
                {
                    ["Milan"] = "Not found"
                },
                Current = new Dictionary<string, object>
                {
                    ["Amsterdam"] = "Not found"
                }
            };
            var sut = BikeTheftsControllerStub.Create(theftsCountResult, citiesSettings);

            //Act
            var objectResult = await sut.GetBikeTheftsCountForCities() as OkObjectResult;

            //Assert
            Assert.NotNull(objectResult);
            Assert.Equal(
                JsonConvert.SerializeObject(expectedResponse),
                JsonConvert.SerializeObject(objectResult.Value));
        }
    }
}
