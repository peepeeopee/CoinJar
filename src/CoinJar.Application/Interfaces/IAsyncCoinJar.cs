namespace Application.Interfaces;

public interface IAsyncCoinJar : ICoinJar
{
    Task AddCoinAsync(ICoin coin);
    Task<decimal> GetTotalAmountAsync();
    Task ResetAsync();
}