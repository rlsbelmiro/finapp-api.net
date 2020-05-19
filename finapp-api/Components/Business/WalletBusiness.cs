using FinAppApi.Components.Repositories;
using FinAppApi.Components.ViewModel.WalletViewModel;
using Flunt.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinAppApi.Components.Business
{
    public class WalletBusiness
    {
        private readonly WalletRepository walletRepository;
        public IReadOnlyCollection<Notification> Notifications { get; set; }
        public bool IsValid { get { return this.Notifications == null || !this.Notifications.Any(); } }

        public WalletBusiness(WalletRepository wallet)
        {
            walletRepository = wallet;
        }

        public EditorWalletViewModel Save(EditorWalletViewModel obj)
        {
            obj.Validate();
            if (obj.Valid)
            {
                if (obj.Id > 0)
                    obj = walletRepository.Update(obj);
                else
                    obj = walletRepository.Insert(obj);

                return obj;
            }
            else
            {
                this.Notifications = obj.Notifications;
                return new EditorWalletViewModel();
            }
        }

        public void Delete(int id)
        {
            walletRepository.Delete(id);
        }

        public IEnumerable<ListWalletViewModel> List()
        {
            return walletRepository.List();
        }

        public EditorWalletViewModel Get(int id)
        {
            return walletRepository.Get(id);
        }

        public IEnumerable<ListWalletViewModel> GetByClientId(int clientId)
        {
            return walletRepository.GetByClientId(clientId);
        }
    }
}
