# Bike-Thefts
A simple API that returns the amount of bike thefts in a given city.

## Getting started

### Running the application with Visual Studio
- Clone the repository to your machine
- Open the solution file located in the root of the repository called **Swapfiets.BikeThefts.sln**
- Select **IIS Express**
- Start it by pressing Ctrl + F5, or Debug/Start
- The main swagger page should open, select an endpoint and try it out :)
> The solutions is configured to run with SSL, so please allow the certificates to be installed in your machine

### Endpoints
There are two endpoints available:
- [GET] **host**/bikethefts/Count: Retrieves the count of bike thefts in the given city.
- [GET] **host**/bikethefts/CurrentAndCandidateCities: Retrieves the information for the current cities in operation and the candidate cities, with their bike theft's count.