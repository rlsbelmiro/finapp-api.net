using FinAppApi.Components.Repositories;
using FinAppApi.Components.Utils;
using FinAppApi.Components.ViewModel.CategoryViewModel;
using FinAppApi.Components.ViewModel.CreditCardViewModel;
using FinAppApi.Components.ViewModel.DocumentViewModel;
using FinAppApi.Components.ViewModel.WalletViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static FinAppApi.Components.Utils.Enumerators;

namespace FinAppApi.Components.Business
{
    public class DocumentBusiness
    {
        private readonly WalletRepository walletRepository;
        private readonly CreditCardRepository creditCardRepository;
        private readonly CategoryRepository categoryRepository;
        
        public DocumentBusiness(WalletRepository wallet, CreditCardRepository creditCard, CategoryRepository category)
        {
            walletRepository = wallet;
            creditCardRepository = creditCard;
            categoryRepository = category;
        }

        /// <summary>
        /// Retonar uma lista de objetos para filtro de documentos
        /// </summary>
        /// <param name="clientId"></param>
        /// <returns></returns>
        public FilterDocumentViewModel GetFilter(int clientId)
        {
            var filter = new FilterDocumentViewModel();
            filter.Types = Utils.Utils.GetListOfEnum(new TypeDocument());
            filter.TypeOfDates = Utils.Utils.GetListOfEnum(new TypeOfDate());
            filter.States = Utils.Utils.GetListOfEnum(new StateDocument());
            filter.Wallets = walletRepository.GetByClientId(clientId).ToList();
            filter.CreditCards = creditCardRepository.GetByClientId(clientId).ToList();
            filter.Categories = categoryRepository.GetByClientId(clientId).ToList();
            return filter;
        }
    }
}
