using NUnit.Framework;
using NUnit.Framework.Internal;

namespace DependencyInjection.UnitTests
{
    [TestFixture]
    public class GainDivergenceCheckerTests
    {
        [Test]
        [TestCase(100, 100, 100, 100, true)]
        [TestCase(100, 200, 100, 200, true)]
        [TestCase(50, 100, 50, 100, true)]
        [TestCase(50, 100, 50, 50, false)]
        [TestCase(100, 100, 50, 50, false)]
        public void HasDivergence_ResturnsCorrectResult(decimal accounterSales, decimal accounterReturned, decimal frSales, decimal frReturned, bool expectedResult)
        {
            //Arrange
            IAccounter accounter = new TestableAccounter()
            {
                SalesSum = accounterSales,
                SumOfReturnedTickets = accounterReturned
            };

            IFiscalRegistrator fiscalRegistrator = new TestableFr()
            {
                SalesSum = frSales,
                SumOfReturnedTickets = frReturned
            };

            //Act
            var checker = new GainDivergenceChecker(accounter, fiscalRegistrator);
            bool result = checker.HasDivergence();

            //Assert
            Assert.AreEqual(expectedResult, result);
        }
    }

    public class TestableAccounter : IAccounter
    {
        public decimal SalesSum { get; set; }
        public decimal SumOfReturnedTickets { get; set; }

        public decimal GetSalesSum()
        {
            return SalesSum;
        }

        public decimal GetSumOfReturnedTickets()
        {
            return SumOfReturnedTickets;
        }
    }

    public class TestableFr : IFiscalRegistrator
    {
        public decimal SalesSum { get; set; }
        public decimal SumOfReturnedTickets { get; set; }

        public decimal GetSalesSum()
        {
            return SalesSum;
        }

        public decimal GetSumOfReturnedTickets()
        {
            return SumOfReturnedTickets;
        }
    }
}
