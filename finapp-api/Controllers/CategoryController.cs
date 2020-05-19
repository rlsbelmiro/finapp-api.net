using FinAppApi.Components.Business;
using FinAppApi.Components.Security;
using FinAppApi.Components.ViewModel;
using FinAppApi.Components.ViewModel.CategoryViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinAppApi.Controllers
{
    [Route("api")]
    public class CategoryController : Controller
    {
        private CategoryBusiness categoryBusiness;

        public CategoryController(CategoryBusiness business)
        {
            categoryBusiness = business;
        }
        [Route("categories")]
        [HttpPost]
        [Authorize]
        public ResultViewModel Post([FromBody] EditorCategoryViewModel obj)
        {
            obj.ClientId = Token.GetClientId(User);
            var viewModel = categoryBusiness.Save(obj);
            return !categoryBusiness.IsValid ?
                new ResultViewModel()
                {
                    Success = false,
                    Message = "Não foi possível cadastrar a categoria",
                    Data = categoryBusiness.Notifications
                } : new ResultViewModel()
                {
                    Success = true,
                    Message = "Categoria cadastrada com sucesso",
                    Data = obj
                };

        }

        [Route("categories/{id}")]
        [HttpDelete]
        [Authorize]
        public ResultViewModel Delete(int id)
        {
            categoryBusiness.Delete(id);
            return new ResultViewModel()
            {
                Success = true,
                Message = "Categoria excluída com sucesso",
                Data = null
            };
        }

        [Route("categories/{id}")]
        [HttpPut]
        [Authorize]
        public ResultViewModel Put([FromBody] EditorCategoryViewModel obj, int id)
        {
            obj.Id = id;
            obj.ClientId = Token.GetClientId(User);
            var viewModel = categoryBusiness.Save(obj);
            return !categoryBusiness.IsValid ?
                new ResultViewModel()
                {
                    Success = false,
                    Message = "Não foi possível alterar a categoria",
                    Data = categoryBusiness.Notifications
                } : new ResultViewModel()
                {
                    Success = true,
                    Message = "Categoria alterada com sucesso",
                    Data = obj
                };
        }

        [Route("categories")]
        [HttpGet]
        [Authorize]
        public ResultViewModel List()
        {
            int clientId = Token.GetClientId(User);
            var list = categoryBusiness.GetByClientId(clientId);
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
                    Message = "Nenhuma categoria encontrada",
                    Data = null
                };
            }
        }

        [Route("categories/{id}")]
        [HttpGet]
        [Authorize]
        public ResultViewModel Get(int id)
        {
            var obj = categoryBusiness.Get(id);
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
                    Message = "Nenhuma categoria encontrada",
                    Data = null
                };
            }
        }
    }
}
