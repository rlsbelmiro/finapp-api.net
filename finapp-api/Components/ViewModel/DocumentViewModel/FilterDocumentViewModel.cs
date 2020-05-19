using FinAppApi.Components.Utils;
using FinAppApi.Components.ViewModel.CategoryViewModel;
using FinAppApi.Components.ViewModel.CreditCardViewModel;
using FinAppApi.Components.ViewModel.WalletViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static FinAppApi.Components.Utils.Enumerators;

namespace FinAppApi.Components.ViewModel.DocumentViewModel
{
    public class FilterDocumentViewModel
    {
        public List<EnumViewModel> Types { get; set; }
        public List<EnumViewModel> States { get; set; }
        public List<EnumViewModel> TypeOfDates { get; set; }
        public List<ListWalletViewModel> Wallets { get; set; }
        public List<ListCreditCardViewModel> CreditCards { get; set; }
        public List<ListCategoryViewModel> Categories { get; set; }


    }
}
