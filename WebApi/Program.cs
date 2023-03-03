using Application.Interfaces;
using Application.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IAsyncCoinJar, CoinJar>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var simpleImplementation = app.MapGroup("").WithTags("simple");

simpleImplementation.MapGet("/total",
    async (IAsyncCoinJar coinJar) => await coinJar.GetTotalAmountAsync());

simpleImplementation.MapPost("/add",
    async (IAsyncCoinJar coinJar, Coin coin) =>
        await coinJar.AddCoinAsync(coin)
);

simpleImplementation.MapPut("/reset",
    async (IAsyncCoinJar coinJar) =>
        await coinJar.ResetAsync()
);

var restfulImplementation = app.MapGroup("/jar").WithTags("restful");

restfulImplementation.MapGet("",
    async (IAsyncCoinJar coinJar) =>
        await coinJar.GetTotalAmountAsync()
);

restfulImplementation.MapPost("",
    async (IAsyncCoinJar coinJar, Coin coin) =>
        await coinJar.AddCoinAsync(coin)
);
restfulImplementation.MapDelete("",
    async (IAsyncCoinJar coinJar) =>
        await coinJar.ResetAsync()
);

app.Run();