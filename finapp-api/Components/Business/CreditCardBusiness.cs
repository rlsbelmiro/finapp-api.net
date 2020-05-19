using FinAppApi.Components.Repositories;
using FinAppApi.Components.ViewModel.CreditCardViewModel;
using Flunt.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinAppApi.Components.Business
{
    public class CreditCardBusiness
    {
        private CreditCardRepository creditCardRepository;

        public IReadOnlyCollection<Notification> Notifications { get; set; }
        public bool IsValid { get { return this.Notifications == null || !this.Notifications.Any(); } }

        public CreditCardBusiness(CreditCardRepository repository)
        {
            creditCardRepository = repository;
        }

        public EditorCreditCardViewModel Save(EditorCreditCardViewModel obj)
        {
            obj.Validate();
            if (obj.Valid)
            {
                if (obj.Id > 0)
                    obj = creditCardRepository.Update(obj);
                else
                    obj = creditCardRepository.Insert(obj);

                return obj;
            }
            else
            {
                this.Notifications = obj.Notifications;
                return new EditorCreditCardViewModel();
            }
        }

        public void Delete(int id)
        {
            creditCardRepository.Delete(id);
        }

        public IEnumerable<ListCreditCardViewModel> List()
        {
            return creditCardRepository.List();
        }

        public EditorCreditCardViewModel Get(int id)
        {
            var obj = creditCardRepository.Get(id);
            return obj;

        }

        public IEnumerable<ListCreditCardViewModel> GetByClientId(int id)
        {
            return creditCardRepository.GetByClientId(id);
        }

    }
}
