using FinAppApi.Components.Repositories;
using FinAppApi.Components.ViewModel.UserViewModel;
using Flunt.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinAppApi.Components.Business
{
    public class UserBusiness
    {
        private UserRepository _repository;

        public IReadOnlyCollection<Notification> Notifications { get; set; }
        public bool IsValid { get { return this.Notifications == null || !this.Notifications.Any(); } }

        public UserBusiness(UserRepository repository)
        {
            _repository = repository;
        }

        public EditorUserViewModel Save(EditorUserViewModel obj)
        {
            obj.Validate();
            if (obj.Valid)
            {
                if (obj.Id > 0)
                    obj = _repository.Update(obj);
                else
                    obj = _repository.Insert(obj);

                if(obj != null)
                    obj.Password = "";

                return obj;
            }
            else
            {
                this.Notifications = obj.Notifications;
                return new EditorUserViewModel();
            }
        }

        public void Delete(int id)
        {
            _repository.Delete(id);
        }

        public IEnumerable<ListUserViewModel> List()
        {
            return _repository.List();
        }

        public EditorUserViewModel Get(int id)
        {
            var obj = _repository.Get(id);
            if (obj != null)
                obj.Password = "";
            return obj;

        }

        public IEnumerable<ListUserViewModel> GetByClientId(int id)
        {
            return _repository.GetByClientId(id);
        }

        public EditorUserViewModel Login(string login, string password)
        {
            return _repository.Login(login, password);
        }
    }
}
