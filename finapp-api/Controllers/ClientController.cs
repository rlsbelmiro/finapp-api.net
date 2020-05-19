using FinAppApi.Components.Business;
using FinAppApi.Components.Repositories;
using FinAppApi.Components.ViewModel;
using FinAppApi.Components.ViewModel.ClientViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinAppApi.Controllers
{
    [Route("api")]
    public class ClientController : Controller
    {
        private ClientBusiness _business;
        private UserBusiness _userBusiness;

        public ClientController(ClientBusiness business, UserBusiness userBusiness)
        {
            _business = business;
            _userBusiness = userBusiness;
        }
        [Route("clients")]
        [HttpPost]
        [Authorize]
        public ResultViewModel Post([FromBody] EditorClientViewModel obj)
        {
            var viewModel = _business.Save(obj);
            return !_business.IsValid || viewModel == null ?
                new ResultViewModel()
                {
                    Success = false,
                    Message = viewModel == null ? "Cliente já cadastrado com o mesmo documento" : "Não foi possível cadastrar o cliente",
                    Data = _business.Notifications
                } : new ResultViewModel()
                {
                    Success = true,
                    Message = "Cliente cadastrado com sucesso",
                    Data = obj
                };

        }

        [Route("clients/{id}")]
        [HttpDelete]
        [Authorize]
        public ResultViewModel Delete(int id)
        {
            _business.Delete(id);
            return new ResultViewModel()
            {
                Success = true,
                Message = "Cliente excluído com sucesso",
                Data = null
            };
        }

        [Route("clients/{id}")]
        [HttpPut]
        [Authorize]
        public ResultViewModel Put([FromBody] EditorClientViewModel obj, int id)
        {
            obj.Id = id;
            var viewModel = _business.Save(obj);
            return !_business.IsValid && viewModel.Id > 0 ?
                new ResultViewModel()
                {
                    Success = false,
                    Message = "Não foi possível alterar o cliente",
                    Data = _business.Notifications
                } : new ResultViewModel()
                {
                    Success = true,
                    Message = "Cliente alterado com sucesso",
                    Data = obj
                };
        }

        [Route("clients")]
        [HttpGet]
        [Authorize]
        public ResultViewModel List()
        {
            var list = _business.List();
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
                    Message = "Nenhum cliente encontrado",
                    Data = null
                };
            }
        }

        [Route("clients/{id}")]
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
                    Message = "Nenhum cliente encontrado",
                    Data = null
                };
            }
        }

    }
}
