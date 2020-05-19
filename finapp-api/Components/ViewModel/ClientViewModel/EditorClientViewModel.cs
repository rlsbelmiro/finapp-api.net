using Flunt.Notifications;
using Flunt.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinAppApi.Components.ViewModel.ClientViewModel
{
    public class EditorClientViewModel : Notifiable, IValidatable
    {
        public int Id { get; set; }
        public string Document { get; set; }
        public string Name { get; set; }
        public string AliasName { get; set; }
        public string ZipCode { get; set; }
        public string Adress { get; set; }
        public string Email { get; set; }
        public bool IsCompany { get; set; }

        public void Validate()
        {
            AddNotifications(
                new Contract()
                    .HasMinLen(Document,0,"Document","Por favor informe o documento")
                    .HasMaxLen(Document,20,"Document","O documento deve ter no máximo 20 caracteres")
                    .HasMinLen(Name, 0, "Name", "Por favor informe o nome")
                    .HasMaxLen(Name, 200, "Name", "O nome deve ter no máximo 200 caracteres")
                    .HasMaxLengthIfNotNullOrEmpty(AliasName, 200, "AliasName", "O nome fantasia deve ter no máximo 200 caracteres")
                    .HasMaxLengthIfNotNullOrEmpty(ZipCode, 10, "ZipCode", "O cep deve ter no máximo 10 caracteres")
                    .HasMaxLengthIfNotNullOrEmpty(Adress, 200, "Adress", "O endereço deve ter no máximo 200 caracteres")
                    .HasMaxLengthIfNotNullOrEmpty(Email, 200, "Email", "O e-mail deve ter no máximo 200 caracteres")
            );
        }
    }
}
