using Application.Interfaces;

namespace Application.Models;

public class Coin : ICoin
{
    public decimal Amount { get; set; }
    public decimal Volume { get; set; }
}