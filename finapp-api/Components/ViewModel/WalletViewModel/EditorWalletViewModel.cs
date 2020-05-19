using Flunt.Notifications;
using Flunt.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static FinAppApi.Components.Utils.Enumerators;

namespace FinAppApi.Components.ViewModel.WalletViewModel
{
    public class EditorWalletViewModel : Notifiable, IValidatable
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public TypeWallet Type { get; set; }

        public bool Active { get; set; }

        public int ClientId { get; set; }

        public string ClientName { get; set; }

        public void Validate()
        {
            AddNotifications(new Contract()
                .HasMinLen(Name, 0, "Name", "Informe o nome")
                .HasMaxLen(Name, 100, "Name", "Nome deve ter no máximo 100 caracteres")
                .IsGreaterThan(ClientId, 0, "ClientId", "Informe o cliente")
                .IsNotNull(Type, "Type", "Informe o tipo"));
        }
    }
}
