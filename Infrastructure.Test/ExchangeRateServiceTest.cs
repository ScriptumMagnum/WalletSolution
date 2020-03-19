using NUnit.Framework;

namespace Infrastructure.Test
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void GetExchangeRate_DoesNotThrowException()
        {
            // arrange
            ExchangeRateService svc = new ExchangeRateService();            

            // act assert
            Assert.DoesNotThrow(() => svc.GetExchangeRate("EUR", "RUB"));
        }

        [Test]
        public void GetExchangeRate_EURToRUB_ReturnsGreaterThanZero()
        {
            // arrange
            ExchangeRateService svc = new ExchangeRateService();

            // act
            var exchangeRate = svc.GetExchangeRate("EUR", "RUB");

            // assert
            Assert.Greater(exchangeRate, 0);
        }
    }
}