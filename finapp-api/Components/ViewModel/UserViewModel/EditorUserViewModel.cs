using Flunt.Notifications;
using Flunt.Validations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FinAppApi.Components.ViewModel.UserViewModel
{
    public class EditorUserViewModel : Notifiable, IValidatable
    {
		public int Id { get; set; }
		public string Name{ get; set; }

		public bool Status{ get; set; }

		public string Login{ get; set; }

		public string Password{ get; set; }

		public string Email { get; set; }

		public int ClientId { get; set; }

		public string ClientName { get; set; }

		public string TokenAcesso { get; set; }

		public void Validate()
		{
			AddNotifications(new Contract()
				.HasMinLen(Name, 0, "Name", "Informe o nome")
				.HasMaxLen(Name, 100, "Name", "Nome deve ter no máximo 100 caracteres")
				.HasMinLen(Login, 0, "Login", "Informe o login")
				.HasMaxLen(Login, 100, "Login", "Login deve ter no máximo 100 caracteres")
				.HasMinLen(Password, 0, "Password", "Informe o nome")
				.HasMinLen(Email, 0, "Email", "Informe o e-mail")
				.HasMaxLen(Email, 100, "Email", "Email deve ter no máximo 100 caracteres")
				.IsGreaterThan(ClientId,0,"ClienteId","ClienteId é obrigatório"));
		}
	}
}
