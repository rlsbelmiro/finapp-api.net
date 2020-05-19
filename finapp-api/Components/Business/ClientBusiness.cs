using FinAppApi.Components.Repositories;
using FinAppApi.Components.ViewModel.ClientViewModel;
using Flunt.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinAppApi.Components.Business
{
    public class ClientBusiness
    {
        private ClientRepository _repository;

        public IReadOnlyCollection<Notification> Notifications { get; set; }
        public bool IsValid { get { return this.Notifications == null || !this.Notifications.Any(); } }

        public ClientBusiness(ClientRepository repository)
        {
            _repository = repository;
        }

        public EditorClientViewModel Save(EditorClientViewModel obj)
        {
            obj.Validate();
            if (obj.Valid)
            {
                if (obj.Id > 0)
                    return _repository.Update(obj);
                else
                    return _repository.Insert(obj);
            }
            else
            {
                this.Notifications = obj.Notifications;
                return new EditorClientViewModel();
            }
        }

        public void Delete(int id)
        {
            _repository.Delete(id);
        }

        public EditorClientViewModel Get(int id)
        {
            return _repository.Get(id);
        }

        public IEnumerable<ListClientViewModel> List()
        {
            return _repository.List();
        }
    }
}
