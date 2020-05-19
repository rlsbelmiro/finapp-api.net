using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FinAppApi.Components.Entities
{
	[Table("user")]
    public class UserEntity : Identity
    {
		[Required]
		[MaxLength(100)]
		public string Name{ get; set; }

		[Required]
		public bool Status{ get; set; }

		[Required]
		[MaxLength(100)]
		public string Login{ get; set; }

		[Required]
		[MaxLength(1000)]
		public string Password{ get; set; }

		[Required]
		[MaxLength(200)]
		public string Email { get; set; }

		[Required]
		public int ClientId { get; set; }

		[ForeignKey("ClientId")]
		public ClientEntity Client { get; set; }
	}
}
