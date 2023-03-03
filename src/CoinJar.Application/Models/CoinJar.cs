using Application.Interfaces;

namespace Application.Models;

public class CoinJar : ICoinJar
{
    private List<ICoin> _coins = new List<ICoin>();
    private decimal _maxVolume = 42; //fluid ounces

    public void AddCoin(ICoin coin) => _coins.Add(coin);

    public decimal GetTotalAmount() => _coins.Sum(c => c.Amount);

    public void Reset() => _coins.Clear();
}