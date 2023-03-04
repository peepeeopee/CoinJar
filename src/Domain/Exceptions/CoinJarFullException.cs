namespace Domain.Exceptions;

public class CoinJarFullException : Exception
{
    public CoinJarFullException() : base("The jar cannot accept this coin as it will exceed the capacity")
    {
    }
}