using System;

namespace DependencyInjection
{
    public class GainDivergenceChecker
    {
        #region Constructor dependency injection

        //Pros:
        //Protects the invariants

        //Cons:
        //Tends to accumualte many dependencies
        //Smell of Single Responsibility Principle (SRP) violation, consider extracting a class
        //Several dependencies tend to be passed together (consider a container object:

        //class Infrastructure : IInfrastructure
        //{
        //    public Infrastructure(IDependency1 d1, IDependency2 d2, IDependency3 d3, IDependency4 d4)
        //    {

        //    }
        //}

        //class ViewModel
        //{
        //    public ViewModel(IInfrastructure infrastructure)
        //    {

        //    }
        //}

        


        private IAccounter _privateAccounter;
        private IFiscalRegistrator _fr;

        /// <summary>
        /// Constructor dependency injection
        /// </summary>
        /// <param name = "accounter" > Dependency of IAccounter</param>
        /// <param name = "fiscalRegistrator" > Dependency of IFiscalRegistrator</param>
        public GainDivergenceChecker(IAccounter accounter, IFiscalRegistrator fiscalRegistrator)
        {
            _privateAccounter = accounter;
            _fr = fiscalRegistrator;
        }

        public bool HasDivergence()
        {
            decimal salesSum = _privateAccounter.GetSalesSum();
            decimal sumOfReturnedTickets = _privateAccounter.GetSumOfReturnedTickets();

            decimal salesSumByFiscalRegistrator = _fr.GetSalesSum();
            decimal sumOfReturnedTicketsByFiscalRegistrator = _fr.GetSumOfReturnedTickets();

            return salesSum == salesSumByFiscalRegistrator && sumOfReturnedTickets == sumOfReturnedTicketsByFiscalRegistrator;
        }

        #endregion

        #region Property dependency injection : Incapsulation broken as the properties may be null!
        //public IAccounter Accounter { get; set; }
        //public IFiscalRegistrator FiscalRegistrator { get; set; }

        //public bool HasDivergence()
        //{
        //    decimal salesSum = Accounter.GetSalesSum();
        //    decimal sumOfReturnedTickets = Accounter.GetSumOfReturnedTickets();

        //    decimal salesSumByFiscalRegistrator = FiscalRegistrator.GetSalesSum();
        //    decimal sumOfReturnedTicketsByFiscalRegistrator = FiscalRegistrator.GetSumOfreturnedTickets();

        //    return salesSum == salesSumByFiscalRegistrator && sumOfReturnedTickets == sumOfReturnedTicketsByFiscalRegistrator;
        //}
        #endregion

        #region Method dependency injection : Preserves incapsulation
        //public bool HasDivergence(IAccounter accounter, IFiscalRegistrator fr)
        //{
        //    decimal salesSum = accounter.GetSalesSum();
        //    decimal sumOfReturnedTickets = accounter.GetSumOfReturnedTickets();

        //    decimal salesSumByFiscalRegistrator = fr.GetSalesSum();
        //    decimal sumOfReturnedTicketsByFiscalRegistrator = fr.GetSumOfreturnedTickets();

        //    return salesSum == salesSumByFiscalRegistrator && sumOfReturnedTickets == sumOfReturnedTicketsByFiscalRegistrator;
        //}
        #endregion


        private void ValidateDependencies(Accounter accounter, FiscalRegistrator fr)
        {
            if (accounter == null)
                throw new ArgumentNullException("accounter");

            if (fr == null)
                throw new ArgumentNullException("fr");
        }
    }
}
