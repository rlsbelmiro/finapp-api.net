using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Permissions;
using System.Threading.Tasks;

namespace FinAppApi.Components.Entities
{
    public class CreditCardEntity : Identity
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        [Required]
        public int Flag { get; set; }
        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Limit { get; set; }
        [Required]
        public int LastDateInvoice { get; set; }
        [Required]
        public int DueDateInvoice { get; set; }
        [Required]
        public bool Active { get; set; }
        [Required]
        public int WalletId { get; set; }
        [Required]
        public int ClientId { get; set; }
        [ForeignKey("WalletId")]
        public WalletEntity Wallet { get; set; }
        [ForeignKey("ClientId")]
        public ClientEntity Client { get; set; }
    }
}
