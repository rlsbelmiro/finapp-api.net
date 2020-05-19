using FinAppApi.Components.Business;
using FinAppApi.Components.Security;
using FinAppApi.Components.ViewModel;
using FinAppApi.Components.ViewModel.CategoryViewModel;
using FinAppApi.Components.ViewModel.CreditCardViewModel;
using FinAppApi.Components.ViewModel.DocumentViewModel;
using FinAppApi.Components.ViewModel.WalletViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static FinAppApi.Components.Utils.Enumerators;

namespace FinAppApi.Controllers
{
    [Route("api/documents")]
    public class DocumentController : Controller
    {
        private readonly DocumentBusiness documentBusiness;

        public DocumentController(DocumentBusiness business)
        {
            documentBusiness = business;
            
        }

        [Route("filters")]
        [HttpGet]
        [Authorize]
        public ResultViewModel GetFilters()
        {
            int clientId = Token.GetClientId(User);
            var filter = documentBusiness.GetFilter(clientId);
            
            return new ResultViewModel()
            {
                Success = true,
                Message = "",
                Data = filter
            };

        }
    }
}
