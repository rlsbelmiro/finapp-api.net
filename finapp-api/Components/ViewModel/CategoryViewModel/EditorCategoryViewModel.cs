using Flunt.Notifications;
using Flunt.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinAppApi.Components.ViewModel.CategoryViewModel
{
    public class EditorCategoryViewModel : Notifiable, IValidatable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Active { get; set; }
        public int Type { get; set; }
        public string Number { get; set; }
        public bool Analitic { get; set; }
        public int ClientId { get; set; }
        public int CategoryParentId { get; set; }

        public void Validate()
        {
            AddNotifications(new Contract()
                .HasMinLen(Name, 0, "Name", "Informe o nome")
                .HasMaxLen(Name, 100, "Name", "Nome deve ter até 100 caracteres")
                .IsGreaterThan(Type, 0, "Type", "Informe o tipo")
                .IsGreaterThan(ClientId, 0, "ClientId", "Informe o cliente")
                .HasMinLen(Number, 0, "Number", "Informe o numero")
                .HasMaxLen(Number, 30, "Number", "Número deve ter até 30 caracteres"));
        }
    }
}
