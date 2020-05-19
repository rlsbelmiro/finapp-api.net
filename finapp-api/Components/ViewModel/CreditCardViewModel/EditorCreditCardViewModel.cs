using Flunt.Notifications;
using Flunt.Validations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Permissions;
using System.Threading.Tasks;

namespace FinAppApi.Components.ViewModel.CreditCardViewModel
{
    public class EditorCreditCardViewModel : Notifiable, IValidatable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Flag { get; set; }
        public decimal Limit { get; set; }
        public int LastDateInvoice { get; set; }
        public int DueDateInvoice { get; set; }
        public bool Active { get; set; }
        public int WalletId { get; set; }
        public int ClientId { get; set; }

        public void Validate()
        {
            AddNotifications(new Contract()
                .HasMinLen(Name, 0, "Name", "Informe o nome")
                .HasMaxLen(Name, 100, "Name", "Nome deve ter até 100 caracteres")
                .IsGreaterThan(Flag, 0, "Flag", "Informe a bandeira")
                .IsGreaterThan(LastDateInvoice, 0, "LastDateInvoice", "Informe o dia de fechamento da fatura")
                .IsGreaterThan(DueDateInvoice, 0, "DueDateInvoice", "Informe o dia de vencimento da fatura")
                .IsGreaterThan(WalletId, 0, "WalletId", "Informe a carteira")
                .IsGreaterThan(ClientId, 0, "ClientId", "Informe o cliente"));
        }
    }
}
