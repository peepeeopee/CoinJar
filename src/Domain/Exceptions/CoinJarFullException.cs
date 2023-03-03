namespace Domain.Exceptions;

public class CoinJarFullException : Exception
{
    public CoinJarFullException() : base("The jar is full and cannot accept anymore coins")
    {
    }
}