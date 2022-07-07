using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Mvc;
using Swapfiets.BikeThefts.Models;
using Swapfiets.BikeThefts.Tests.Stubs;
using System.Threading.Tasks;
using Xunit;

namespace Swapfiets.BikeThefts.Tests.Tests
{
    public class GetBikeTheftsCountTests
    {
        [Fact]
        public async Task Ok()
        {
            //Arrange
            const int theftsCount = 1;
            var searchParameters = new SearchParameters
            {
                City = "Milan",
            };
            var sut = BikeTheftsControllerStub.Create(theftsCount);

            //Act
            var objectResult = await sut.GetBikeTheftsCount(searchParameters) as OkObjectResult;

            //Assert
            Assert.NotNull(objectResult);
            Assert.Equal($"\"Milan\": 1", objectResult.Value);
        }

        [Fact]
        public async Task WithCityRadiusInMiles_Ok()
        {
            //Arrange
            const int theftsCount = 2;
            var searchParameters = new SearchParameters
            {
                City = "Milan",
                CityRadiusInMiles = 20
            };
            var sut = BikeTheftsControllerStub.Create(theftsCount);

            //Act
            var objectResult = await sut.GetBikeTheftsCount(searchParameters) as OkObjectResult;

            //Assert
            Assert.NotNull(objectResult);
            Assert.Equal("\"Milan\": 2", objectResult.Value);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public async Task WithEmptyCityOnSearchParameters_BadRequest(string emptyCity)
        {
            //Arrange
            const int theftsCount = 1;
            var searchParameters = new SearchParameters
            {
                City = emptyCity,
            };
            var sut = BikeTheftsControllerStub.Create(theftsCount);

            //Act
            var objectResult = await sut.GetBikeTheftsCount(searchParameters) as BadRequestObjectResult;

            //Assert
            Assert.NotNull(objectResult);
            Assert.Equal("City must be provided.", objectResult.Value);
        }

        [Fact]
        public async Task WithNoResultsFound_NotFound()
        {
            //Arrange
            var expectedErrorMessage = "No results found for the location 'Milan'.";
            var theftsCountResult = Result.Failure<int>(expectedErrorMessage);
            var searchParameters = new SearchParameters
            {
                City = "Milan",
            };
            var sut = BikeTheftsControllerStub.Create(theftsCountResult);

            //Act
            var objectResult = await sut.GetBikeTheftsCount(searchParameters) as NotFoundObjectResult;

            //Assert
            Assert.NotNull(objectResult);
            Assert.Equal(expectedErrorMessage, objectResult.Value);
        }
    }
}
