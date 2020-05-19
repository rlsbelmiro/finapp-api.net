using Flunt.Notifications;
using Flunt.Validations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinAppApi.Components.ViewModel.UserViewModel
{
    public class AccessViewModel : Notifiable, IValidatable
    {
        public string Login { get; set; }
        public string Password { get; set; }

        public void Validate()
        {
            AddNotifications(new Contract()
                .HasMinLen(Login, 0, "Login", "Informe o login")
                .HasMaxLen(Login, 100, "Login", "Login deve ter no máximo 100 caracteres")
                .HasMinLen(Password, 0, "Password", "Informe o nome"));
        }
    }
}
