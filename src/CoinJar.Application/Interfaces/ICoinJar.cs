namespace CoinJar.Application.Interfaces;

public interface ICoinJar
{
    void AddCoin(ICoin coin);
    decimal GetTotalAmount();
    void Reset();
}