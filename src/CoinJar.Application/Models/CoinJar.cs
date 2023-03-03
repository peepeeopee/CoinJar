using Application.Interfaces;
using Domain.Exceptions;

namespace Application.Models;

public class CoinJar : IAsyncCoinJar
{
    private readonly List<ICoin> _coins = new();
    private readonly decimal _maxVolume = 42; //fluid ounces

    public void AddCoin(ICoin coin) => InsertCoin(coin);
    public Task AddCoinAsync(ICoin coin) => Task.Run(() => AddCoin(coin));

    public decimal GetTotalAmount() => _coins.Sum(c => c.Amount);
    public Task<decimal> GetTotalAmountAsync() => Task.Run(GetTotalAmount);

    public void Reset() => _coins.Clear();
    public Task ResetAsync() => Task.Run(Reset);

    private void InsertCoin(ICoin coin)
    {
        var totalVolume = _coins.Sum(c => c.Volume) + coin.Volume;

        if (totalVolume > _maxVolume)
        {
            throw new CoinJarFullException();
        }

        _coins.Add(coin);
    }
}