using Application.Interfaces;
using Application.Models;
using Domain.Exceptions;
using FluentAssertions;
using Moq;

namespace Application.UnitTests
{
    public class CoinJarTests
    {
        [Fact]
        public void GivenEmptyJar_TotalAmountRetrieved_ZeroAmountReturned()
        {
            var coinJar = new CoinJar();

            var amount = coinJar.GetTotalAmount();

            amount.Should()
                  .Be(0, "the jar is empty");
        }

        [Fact]
        public void GivenEmptyJar_CoinAdded_ValueOfCoinAmountReturned()
        {
            var coinJar = new CoinJar();

            var coinMock = new Mock<ICoin>();

            coinMock.Setup(x => x.Amount)
                    .Returns(42m);
            coinMock.Setup(x => x.Volume)
                    .Returns(0.5m);

            coinJar.AddCoin(coinMock.Object);

            var amount = coinJar.GetTotalAmount();

            amount.Should()
                  .Be(coinMock.Object.Amount, "there is only one coin in the jar");
        }

        [Fact]
        public void GivenEmptyJar_CoinsAddedThenReset_ZeroAmountReturned()
        {
            var coinJar = new CoinJar();

            var coinMock = new Mock<ICoin>();

            coinMock.Setup(x => x.Amount)
                    .Returns(42m);
            coinMock.Setup(x => x.Volume)
                    .Returns(0.5m);

            coinJar.AddCoin(coinMock.Object);
            coinJar.AddCoin(coinMock.Object);
            coinJar.AddCoin(coinMock.Object);
            coinJar.AddCoin(coinMock.Object);

            var amount = coinJar.GetTotalAmount();

            //check the jar isn't empty
            amount.Should()
                  .NotBe(0, "the jar shouldn't be empty");

            coinJar.Reset();

            var newAmount = coinJar.GetTotalAmount();

            newAmount.Should()
                     .Be(0, "the jar should be empty");
        }

        [Fact]
        public void GivenAlmostFullJar_CoinAddedOverJarThreshold_JarFullExceptionThrown()
        {
            var coinJar = new CoinJar();

            var bigCoinMock = new Mock<ICoin>();

            bigCoinMock.Setup(x => x.Amount)
                       .Returns(42m);
            bigCoinMock.Setup(x => x.Volume)
                       .Returns(41.5m);


            var smallCoinMock = new Mock<ICoin>();

            smallCoinMock.Setup(x => x.Amount)
                         .Returns(42m);
            smallCoinMock.Setup(x => x.Volume)
                         .Returns(1m);


            coinJar.AddCoin(bigCoinMock.Object);

            coinJar.Invoking(c => c.AddCoin(smallCoinMock.Object))
                   .Should()
                   .Throw<CoinJarFullException>();
        }
    }
}