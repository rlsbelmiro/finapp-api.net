using FinAppApi.Components.Conection;
using FinAppApi.Components.Entities;
using FinAppApi.Components.ViewModel.ClientViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinAppApi.Components.Utils;

namespace FinAppApi.Components.Repositories
{
    public class ClientRepository : IReposity<ClientEntity, ListClientViewModel, EditorClientViewModel>
    {
        private readonly FinAppContext _context;

        public ClientRepository(FinAppContext context)
        {
            _context = context;
        }

        public void Delete(int id)
        {
            var entity = _context.Clients.Find(id);
            if(entity != null && entity.Id > 0)
            {
                _context.Remove(entity);
                _context.SaveChanges();
            }
        }

        public EditorClientViewModel EntityToViewModel(ClientEntity obj)
        {
            var r = new EditorClientViewModel();
            r.Adress = obj.Adress;
            r.AliasName = obj.AliasName;
            r.Document = obj.Document;
            r.Email = obj.Email;
            r.Id = obj.Id;
            r.IsCompany = obj.IsCompany;
            r.Name = obj.Name;
            r.ZipCode = obj.ZipCode;

            return r;
        }

        public ListClientViewModel EntityToViewModelList(ClientEntity obj)
        {
            return new ListClientViewModel() { 
                Id = obj.Id,
                Document = obj.Document,
                Name = obj.Name
            };
        }

        public EditorClientViewModel Get(int id)
        {
            var entity = _context.Clients.Find(id);
            return EntityToViewModel(entity);
        }

        public IEnumerable<ListClientViewModel> GetByClientId(int clientId)
        {
            return null;
        }

        public EditorClientViewModel Insert(EditorClientViewModel obj)
        {
            var entity = ViewModelToEntity(obj);

            var client = _context.Clients.Where(x => x.Document.Equals(entity.Document)).FirstOrDefault();
            if (client != null && client.Id > 0)
                return null;
            _context.Clients.Add(entity);
            _context.SaveChanges();
            obj.Id = entity.Id;
            return obj;
        }

        public IEnumerable<ListClientViewModel> List()
        {
            return _context.Clients.Select(x => new ListClientViewModel()
            {
                Id = x.Id,
                Name = x.Name,
                Document = x.Document
            }).ToList();
        }

        public EditorClientViewModel Update(EditorClientViewModel obj)
        {
            if(obj.Id > 0)
            {
                var entity = _context.Clients.Find(obj.Id);
                entity.Adress = obj.Adress;
                entity.AliasName = obj.AliasName;
                entity.Document = obj.Document.OnlyNumbers();
                entity.Email = obj.Email;
                entity.UpdatedDate = DateTime.Now;
                entity.IsCompany = obj.IsCompany;
                entity.Name = obj.Name;
                entity.ZipCode = obj.ZipCode;

                _context.SaveChanges();

                return obj;
            }

            return new EditorClientViewModel();
        }

        public ClientEntity ViewModelToEntity(EditorClientViewModel obj)
        {
            ClientEntity entity = new ClientEntity();
            entity.Adress = obj.Adress;
            entity.AliasName = obj.AliasName;
            entity.Document = obj.Document.OnlyNumbers();
            entity.Email = obj.Email;
            entity.Name = obj.Name;
            entity.InsertedDate = DateTime.Now;
            entity.UpdatedDate = DateTime.Now;
            entity.IsCompany = obj.IsCompany;
            entity.ZipCode = obj.ZipCode;
            return entity;
        }
    }
}
