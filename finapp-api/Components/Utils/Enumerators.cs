using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinAppApi.Components.Utils
{
    public class Enumerators
    {
        public enum TypeDocument
        {
            CREDIT = 1,
            DEBIT = 2
        }

        public enum StateDocument
        {
            Pending = 1,
            Paid = 2,
            ParcialPaid = 3,
            Canceled = 4
        }

        public enum TypeOfDate 
        { 
            LastThirtyDays = 1,
            ThisMonth = 2,
            LastMonth = 3,
            NextMonth = 4,
            ThisYear = 5,
            LastYear = 6,
            SelectedDates = 7
        }

        public enum TypeWallet
        {
            Banco = 1,
            Caixa = 2,
            Investimento = 3
        }
    }
}
