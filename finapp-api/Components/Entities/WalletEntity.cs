using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FinAppApi.Components.Entities
{
    [Table("wallet")]
    public class WalletEntity : Identity
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        public int Type { get; set; }

        [Required]
        public bool Active { get; set; }

        [Required]
        public int ClientId { get; set; }

        [ForeignKey("ClientId")]
        public ClientEntity Client { get; set; }
    }
}
