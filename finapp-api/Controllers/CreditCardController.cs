using FinAppApi.Components.Business;
using FinAppApi.Components.Security;
using FinAppApi.Components.ViewModel;
using FinAppApi.Components.ViewModel.CreditCardViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinAppApi.Controllers
{
    [Route("api")]
    public class CreditCardController : Controller
    {
        private CreditCardBusiness creditCardBusiness;

        public CreditCardController(CreditCardBusiness business)
        {
            creditCardBusiness = business;
        }
        [Route("creditcards")]
        [HttpPost]
        [Authorize]
        public ResultViewModel Post([FromBody] EditorCreditCardViewModel obj)
        {
            obj.ClientId = Token.GetClientId(User);
            var viewModel = creditCardBusiness.Save(obj);
            return !creditCardBusiness.IsValid ?
                new ResultViewModel()
                {
                    Success = false,
                    Message = "Não foi possível cadastrar o cartão de crédito",
                    Data = creditCardBusiness.Notifications
                } : new ResultViewModel()
                {
                    Success = true,
                    Message = "Cartão de crédito cadastrada com sucesso",
                    Data = obj
                };

        }

        [Route("creditcards/{id}")]
        [HttpDelete]
        [Authorize]
        public ResultViewModel Delete(int id)
        {
            creditCardBusiness.Delete(id);
            return new ResultViewModel()
            {
                Success = true,
                Message = "Cartão de crédito excluído com sucesso",
                Data = null
            };
        }

        [Route("creditcards/{id}")]
        [HttpPut]
        [Authorize]
        public ResultViewModel Put([FromBody] EditorCreditCardViewModel obj, int id)
        {
            obj.Id = id;
            obj.ClientId = Token.GetClientId(User);
            var viewModel = creditCardBusiness.Save(obj);
            return !creditCardBusiness.IsValid ?
                new ResultViewModel()
                {
                    Success = false,
                    Message = "Não foi possível alterar o cartão de crédito",
                    Data = creditCardBusiness.Notifications
                } : new ResultViewModel()
                {
                    Success = true,
                    Message = "Cartão de crédito alterada com sucesso",
                    Data = obj
                };
        }

        [Route("creditcards")]
        [HttpGet]
        [Authorize]
        public ResultViewModel List()
        {
            int clientId = Token.GetClientId(User);
            var list = creditCardBusiness.GetByClientId(clientId);
            if (list != null && list.Any())
            {
                return new ResultViewModel()
                {
                    Success = true,
                    Message = "",
                    Data = list
                };
            }
            else
            {
                return new ResultViewModel()
                {
                    Success = false,
                    Message = "Nenhuma carteira encontrada",
                    Data = null
                };
            }
        }

        [Route("creditcards/{id}")]
        [HttpGet]
        [Authorize]
        public ResultViewModel Get(int id)
        {
            var obj = creditCardBusiness.Get(id);
            if (obj != null)
            {
                return new ResultViewModel()
                {
                    Success = true,
                    Message = "",
                    Data = obj
                };
            }
            else
            {
                return new ResultViewModel()
                {
                    Success = false,
                    Message = "Nenhuma cartão de crédito encontrada",
                    Data = null
                };
            }
        }
    }
}
