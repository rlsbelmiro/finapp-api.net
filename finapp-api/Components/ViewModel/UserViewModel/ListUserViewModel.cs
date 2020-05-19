using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FinAppApi.Components.ViewModel.UserViewModel
{
    public class ListUserViewModel
	{
		public int Id { get; set; }
		public string Name{ get; set; }

		public bool Status{ get; set; }

		public string Login{ get; set; }

		public string Email { get; set; }

		public int ClientId { get; set; }
	}
}
