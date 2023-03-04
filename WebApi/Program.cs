using Application.Interfaces;
using Application.Models;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

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

app.UseExceptionHandler(configure: webApp => webApp.Run(async ctx =>
    {
        await Results.Problem(
                         ctx.Features
                            .Get<IExceptionHandlerFeature>()
                            ?.Error.Message)
                     .ExecuteAsync(ctx);
    })
);

app.UseHttpsRedirection();

var simpleImplementation = app.MapGroup("")
                              .WithTags("simple");

simpleImplementation.MapGet("/total",
                        async (IAsyncCoinJar coinJar) => await coinJar.GetTotalAmountAsync()
                    )
                    .WithDescription("Gets the total value from the jar");

simpleImplementation.MapPost("/add",
                        async (IAsyncCoinJar coinJar, Coin coin) =>
                            await coinJar.AddCoinAsync(coin)
                    )
                    .WithDescription("Adds a coin to the Jar (if possible)");

simpleImplementation.MapPut("/reset",
                        async (IAsyncCoinJar coinJar) =>
                            await coinJar.ResetAsync()
                    )
                    .WithDescription("Empties the jar");

var restfulImplementation = app.MapGroup("/jar")
                               .WithTags("restful-ish");

restfulImplementation.MapGet("",
                         async (IAsyncCoinJar coinJar) =>
                             await coinJar.GetTotalAmountAsync()
                     )
                     .WithDescription("Gets the total value from the jar");

restfulImplementation.MapPost("",
                         async (IAsyncCoinJar coinJar, Coin coin) =>
                             await coinJar.AddCoinAsync(coin)
                     )
                     .WithDescription("Adds a coin to the Jar (if possible)");
restfulImplementation.MapDelete("",
                         async (IAsyncCoinJar coinJar) =>
                             await coinJar.ResetAsync()
                     )
                     .WithDescription(
                         "Empties the jar - No real restful way to 'Reset' so mapped to delete");

app.Run();