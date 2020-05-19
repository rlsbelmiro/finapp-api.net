using FinAppApi.Components.Business;
using FinAppApi.Components.Security;
using FinAppApi.Components.ViewModel;
using FinAppApi.Components.ViewModel.WalletViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinAppApi.Controllers
{
    [Route("api")]
    public class WalletController : Controller
    {
        private WalletBusiness _business;

        public WalletController(WalletBusiness business)
        {
            _business = business;
        }
        [Route("wallets")]
        [HttpPost]
        [Authorize]
        public ResultViewModel Post([FromBody] EditorWalletViewModel obj)
        {
            obj.ClientId = Token.GetClientId(User);
            var viewModel = _business.Save(obj);
            return !_business.IsValid ?
                new ResultViewModel()
                {
                    Success = false,
                    Message = "Não foi possível cadastrar a carteira",
                    Data = _business.Notifications
                } : new ResultViewModel()
                {
                    Success = true,
                    Message = "Carteira cadastrada com sucesso",
                    Data = obj
                };

        }

        [Route("wallets/{id}")]
        [HttpDelete]
        [Authorize]
        public ResultViewModel Delete(int id)
        {
            _business.Delete(id);
            return new ResultViewModel()
            {
                Success = true,
                Message = "Carteira excluída com sucesso",
                Data = null
            };
        }

        [Route("wallets/{id}")]
        [HttpPut]
        [Authorize]
        public ResultViewModel Put([FromBody] EditorWalletViewModel obj, int id)
        {
            obj.Id = id;
            obj.ClientId = Token.GetClientId(User);
            var viewModel = _business.Save(obj);
            return !_business.IsValid ?
                new ResultViewModel()
                {
                    Success = false,
                    Message = "Não foi possível alterar a carteira",
                    Data = _business.Notifications
                } : new ResultViewModel()
                {
                    Success = true,
                    Message = "Carteira alterada com sucesso",
                    Data = obj
                };
        }

        [Route("wallets")]
        [HttpGet]
        [Authorize]
        public ResultViewModel List()
        {
            int clientId = Token.GetClientId(User);
            var list = _business.GetByClientId(clientId);
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

        [Route("wallets/{id}")]
        [HttpGet]
        [Authorize]
        public ResultViewModel Get(int id)
        {
            var obj = _business.Get(id);
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
                    Message = "Nenhuma carteira encontrada",
                    Data = null
                };
            }
        }
    }
}
