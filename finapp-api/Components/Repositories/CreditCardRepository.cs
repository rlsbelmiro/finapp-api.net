using FinAppApi.Components.Conection;
using FinAppApi.Components.Entities;
using FinAppApi.Components.ViewModel.CreditCardViewModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinAppApi.Components.Repositories
{
    public class CreditCardRepository : IReposity<CreditCardEntity, ListCreditCardViewModel, EditorCreditCardViewModel>
    {
        private readonly FinAppContext _context;

        public CreditCardRepository(FinAppContext context)
        {
            _context = context;
        }
        public void Delete(int id)
        {
            var entity = _context.CreditCards.Find(id);
            if (entity != null)
            {
                _context.CreditCards.Remove(entity);
                _context.SaveChanges();
            }
        }

        public EditorCreditCardViewModel EntityToViewModel(CreditCardEntity obj)
        {
            return new EditorCreditCardViewModel()
            {
                Id = obj.Id,
                Active = obj.Active,
                ClientId = obj.ClientId,
                DueDateInvoice = obj.DueDateInvoice,
                Flag = obj.Flag,
                LastDateInvoice = obj.LastDateInvoice,
                Limit = obj.Limit,
                Name = obj.Name,
                WalletId = obj.WalletId
            };
        }

        public ListCreditCardViewModel EntityToViewModelList(CreditCardEntity obj)
        {
            return new ListCreditCardViewModel()
            {
                Id = obj.Id,
                Name = obj.Name
            };
        }

        public EditorCreditCardViewModel Get(int id)
        {
            var entity = _context.CreditCards.Find(id);
            if (entity != null)
                return EntityToViewModel(entity);

            return null;
        }

        public IEnumerable<ListCreditCardViewModel> GetByClientId(int clientId)
        {
            return _context.CreditCards.Where(x => x.ClientId == clientId).AsNoTracking()
                .Select(x => new ListCreditCardViewModel()
                {
                    Id = x.Id,
                    Name = x.Name
                }).ToList();
        }

        public EditorCreditCardViewModel Insert(EditorCreditCardViewModel obj)
        {
            var entity = ViewModelToEntity(obj);
            _context.CreditCards.Add(entity);
            _context.SaveChanges();
            obj.Id = entity.Id;
            return obj;
        }

        public IEnumerable<ListCreditCardViewModel> List()
        {
            return _context.CreditCards.AsNoTracking()
                .Select(x => new ListCreditCardViewModel()
                {
                    Id = x.Id,
                    Name = x.Name
                });
        }

        public EditorCreditCardViewModel Update(EditorCreditCardViewModel obj)
        {
            var entity = _context.CreditCards.Find(obj.Id);
            if(entity != null)
            {
                entity.Active = obj.Active;
                entity.ClientId = obj.ClientId;
                entity.DueDateInvoice = obj.DueDateInvoice;
                entity.Flag = obj.Flag;
                entity.LastDateInvoice = obj.LastDateInvoice;
                entity.Limit = obj.Limit;
                entity.Name = obj.Name;
                entity.WalletId = obj.WalletId;
                entity.UpdatedDate = DateTime.Now;
                _context.SaveChanges();
            }
            return null;
        }

        public CreditCardEntity ViewModelToEntity(EditorCreditCardViewModel obj)
        {
            return new CreditCardEntity()
            {
                Active = obj.Active,
                ClientId = obj.ClientId,
                DueDateInvoice = obj.DueDateInvoice,
                Flag = obj.Flag,
                InsertedDate = DateTime.Now,
                LastDateInvoice = obj.LastDateInvoice,
                Limit = obj.Limit,
                Name = obj.Name,
                UpdatedDate = DateTime.Now,
                WalletId = obj.WalletId
            };
        }
    }
}
