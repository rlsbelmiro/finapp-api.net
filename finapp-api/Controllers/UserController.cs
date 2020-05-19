using FinAppApi.Components.Business;
using FinAppApi.Components.Security;
using FinAppApi.Components.ViewModel;
using FinAppApi.Components.ViewModel.UserViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinAppApi.Controllers
{
    [Route("api")]
    public class UserController : Controller
    {
        private UserBusiness _business;

        public UserController(UserBusiness business)
        {
            _business = business;
        }
        [Route("users")]
        [HttpPost]
        [Authorize]
        public ResultViewModel Post([FromBody] EditorUserViewModel obj)
        {
            obj.ClientId = Token.GetClientId(User);
            var viewModel = _business.Save(obj);
            return !_business.IsValid || viewModel == null ?
                new ResultViewModel()
                {
                    Success = false,
                    Message = viewModel == null ? "Usuário já cadastrado com o mesmo login" : "Não foi possível cadastrar o usuário",
                    Data = _business.Notifications
                } : new ResultViewModel()
                {
                    Success = true,
                    Message = "Usuário cadastrado com sucesso",
                    Data = obj
                };

        }

        [Route("users/{id}")]
        [HttpDelete]
        [Authorize]
        public ResultViewModel Delete(int id)
        {
            _business.Delete(id);
            return new ResultViewModel()
            {
                Success = true,
                Message = "Usuário excluído com sucesso",
                Data = null
            };
        }

        [Route("users/{id}")]
        [HttpPut]
        [Authorize]
        public ResultViewModel Put([FromBody] EditorUserViewModel obj, int id)
        {
            obj.Id = id;
            obj.ClientId = Token.GetClientId(User);
            var viewModel = _business.Save(obj);
            return !_business.IsValid && viewModel.Id > 0 ?
                new ResultViewModel()
                {
                    Success = false,
                    Message = "Não foi possível alterar o usuário",
                    Data = _business.Notifications
                } : new ResultViewModel()
                {
                    Success = true,
                    Message = "Usuário alterado com sucesso",
                    Data = obj
                };
        }

        [Route("users")]
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
                    Message = "Nenhum usuário encontrado",
                    Data = null
                };
            }
        }

        [Route("users/{id}")]
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
                    Message = "Nenhum usuário encontrado",
                    Data = null
                };
            }
        }
    }
}
