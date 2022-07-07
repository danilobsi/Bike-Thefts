using Swapfiets.BikeThefts.Infrastructure;
using Swapfiets.BikeThefts.Services;
using Swapfiets.BikeThefts.Settings;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Dependency Injection
builder.Services.AddSingleton<IBikeTheftClient, BikeIndexClient>();
builder.Services.AddSingleton<BikeTheftsService>();

//Configuration Session
builder.Services.Configure<CitiesSettings>(builder.Configuration.GetSection("Cities"));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
