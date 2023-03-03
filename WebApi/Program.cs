using Application.Interfaces;
using Application.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<ICoinJar, CoinJar>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/total",
    ctx =>
        Task.FromResult(
            ctx.RequestServices.GetRequiredService<ICoinJar>()
               .GetTotalAmount()
        )
);

app.MapPost("/add",
    (HttpContext ctx, ICoin coin) =>
        ctx.RequestServices.GetRequiredService<ICoinJar>()
           .AddCoin(coin)
);

app.MapPut("/reset",
    (HttpContext ctx) =>
        ctx.RequestServices.GetRequiredService<ICoinJar>()
           .Reset()
);

app.Run();