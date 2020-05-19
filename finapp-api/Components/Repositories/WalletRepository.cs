using FinAppApi.Components.Conection;
using FinAppApi.Components.Entities;
using FinAppApi.Components.ViewModel.WalletViewModel;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework.Constraints;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static FinAppApi.Components.Utils.Enumerators;

namespace FinAppApi.Components.Repositories
{
    public class WalletRepository : IReposity<WalletEntity, ListWalletViewModel, EditorWalletViewModel>
    {
        private readonly FinAppContext _context;

        public WalletRepository(FinAppContext context)
        {
            _context = context;
        }

        public void Delete(int id)
        {
            var entity = _context.Wallets.Find(id);
            if(entity != null)
            {
                _context.Wallets.Remove(entity);
                _context.SaveChanges();
            }
        }

        public EditorWalletViewModel EntityToViewModel(WalletEntity obj)
        {
            return new EditorWalletViewModel()
            {
                Id = obj.Id,
                Active = obj.Active,
                ClientId = obj.ClientId,
                ClientName = obj.Client != null ? obj.Client.Name : "",
                Name = obj.Name,
                Type = (TypeWallet)obj.Type
            };
        }

        public ListWalletViewModel EntityToViewModelList(WalletEntity obj)
        {
            return new ListWalletViewModel()
            {
                Id = obj.Id,
                Name = obj.Name
            };
        }

        public EditorWalletViewModel Get(int id)
        {
            var entity = _context.Wallets.Find(id);
            return EntityToViewModel(entity);
        }

        public IEnumerable<ListWalletViewModel> GetByClientId(int clientId)
        {
            return _context.Wallets.Where(x => x.ClientId == clientId).AsNoTracking().Select(x => new ListWalletViewModel() { 
                Id = x.Id,
                Name = x.Name
            }).ToList();
        }

        public EditorWalletViewModel Insert(EditorWalletViewModel obj)
        {
            var entity = ViewModelToEntity(obj);
            _context.Wallets.Add(entity);
            _context.SaveChanges();
            obj.Id = entity.Id;
            return obj;
        }

        public IEnumerable<ListWalletViewModel> List()
        {
            return _context.Wallets.Select(x => new ListWalletViewModel()
            {
                Id = x.Id,
                Name = x.Name
            }).ToList();
        }

        public EditorWalletViewModel Update(EditorWalletViewModel obj)
        {
            var entity = _context.Wallets.Find(obj.Id);
            if(entity != null)
            {
                entity.Name = obj.Name;
                entity.Active = obj.Active;
                entity.ClientId = obj.ClientId;
                entity.Type = (int)obj.Type;
                entity.UpdatedDate = DateTime.Now;
                _context.SaveChanges();
                return obj;
            }
            return new EditorWalletViewModel();
        }

        public WalletEntity ViewModelToEntity(EditorWalletViewModel obj)
        {
            return new WalletEntity()
            {
                Active = obj.Active,
                ClientId = obj.ClientId,
                InsertedDate = DateTime.Now,
                Name = obj.Name,
                Type = (int)obj.Type,
                UpdatedDate = DateTime.Now
            };
        }
    }
}
