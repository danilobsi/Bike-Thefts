using CSharpFunctionalExtensions;
using Microsoft.Extensions.Options;
using Moq;
using Swapfiets.BikeThefts.Controllers;
using Swapfiets.BikeThefts.Infrastructure;
using Swapfiets.BikeThefts.Services;
using Swapfiets.BikeThefts.Settings;

namespace Swapfiets.BikeThefts.Tests.Stubs
{
    public static class BikeTheftsControllerStub
    {
        public static BikeTheftsController Create(Result<int> theftsCountResult, CitiesSettings? citiesSettings = null)
        {
            var bikeTheftsProvider = new Mock<IBikeTheftClient>();
            var config = new Mock<IOptions<CitiesSettings>>();

            bikeTheftsProvider
                .Setup(s => s.GetBikeTheftsCount(It.IsAny<string>(), It.IsAny<int>()))
                .ReturnsAsync(theftsCountResult);

            if (citiesSettings != null)
            {
                config.Setup(s => s.Value).Returns(citiesSettings);
            }

            return new BikeTheftsController(new BikeTheftsService(bikeTheftsProvider.Object, config.Object));
        }
    }
}
