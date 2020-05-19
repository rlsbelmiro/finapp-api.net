using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FinAppApi.Components.Entities
{
    [Table("client")]
    public class ClientEntity : Identity
    {
        [Required]
        [MaxLength(30)]
        public string Document { get; set; }
        [Required]
        [MaxLength(200)]
        public string Name { get;set; }
        [Required]
        [MaxLength(200)]
        public string AliasName { get; set; }

        [Required]
        [MaxLength(10)]
        public string ZipCode { get; set; }

        [Required]
        [MaxLength(200)]
        public string Adress { get; set; }

        [Required]
        [MaxLength(200)]
        public string Email { get; set; }

        [Required]
        public bool IsCompany { get; set; }

        public IEnumerable<UserEntity> Users { get; set; }
    }
}
