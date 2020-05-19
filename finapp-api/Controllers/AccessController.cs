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
    [Route("api/account")]
    public class AccessController : Controller
    {
        private UserBusiness _business;

        public AccessController(UserBusiness business)
        {
            _business = business;
        }

        [Route("login")]
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<dynamic>> Login([FromBody] AccessViewModel obj)
        {
            obj.Validate();
            if (obj.Valid)
            {
                var user = _business.Login(obj.Login, obj.Password);
                if (user == null)
                    return NotFound(new { message = "Usuário ou senha inválidos" });

                var token = Token.GenerateToken(user);
                user.Password = "";
                user.TokenAcesso = token;
                return new ResultViewModel()
                {
                    Success = true,
                    Message = "",
                    Data = user
                };
            } 
            else
            {
                return new ResultViewModel()
                {
                    Success = false,
                    Message = "Erro ao fazer login",
                    Data = obj.Notifications
                };
            }
        }
    }
}
