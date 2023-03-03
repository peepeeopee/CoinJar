using Application.Interfaces;
using Domain.Exceptions;

namespace Application.Models;

public class CoinJar : ICoinJar
{
    private readonly List<ICoin> _coins = new();
    private readonly decimal _maxVolume = 42; //fluid ounces

    public void AddCoin(ICoin coin) => InsertCoin(coin);

    public decimal GetTotalAmount() => _coins.Sum(c => c.Amount);

    public void Reset() => _coins.Clear();

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