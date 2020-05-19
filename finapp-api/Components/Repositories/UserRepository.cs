using FinAppApi.Components.Conection;
using FinAppApi.Components.Entities;
using FinAppApi.Components.ViewModel.UserViewModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace FinAppApi.Components.Repositories
{
    public class UserRepository : IReposity<UserEntity, ListUserViewModel, EditorUserViewModel>
    {
        private readonly FinAppContext _context;

        public UserRepository(FinAppContext context)
        {
            _context = context;
        }

        public void Delete(int id)
        {
            var entity = _context.Users.Find(id);
            if (entity != null && entity.Id > 0)
            {
                _context.Users.Remove(entity);
                _context.SaveChanges();
            }
        }

        public EditorUserViewModel EntityToViewModel(UserEntity obj)
        {
            EditorUserViewModel model = new EditorUserViewModel();
            model.ClientId = obj.ClientId;
            model.ClientName = obj.Client != null ? obj.Client.Name : "";
            model.Email = obj.Email;
            model.Id = obj.Id;
            model.Login = obj.Login;
            model.Name = obj.Name;
            model.Password = obj.Password;
            model.Status = obj.Status;
            return model;

        }

        public ListUserViewModel EntityToViewModelList(UserEntity obj)
        {
            ListUserViewModel list = new ListUserViewModel();
            list.Id = obj.Id;
            list.Name = obj.Name;
            list.Status = obj.Status;
            list.Email = obj.Email;
            list.ClientId = obj.ClientId;
            return list;

        }

        public EditorUserViewModel Get(int id)
        {
            var entity = _context.Users.Find(id);
            if (entity != null && entity.Id > 0)
                return EntityToViewModel(entity);

            return new EditorUserViewModel();
        }

        public EditorUserViewModel Login(string login, string password)
        {
            var entity = _context.Users.Where(x => x.Login.Equals(login)).AsNoTracking().FirstOrDefault();
            if(entity != null && entity.Id > 0)
            {
                if(entity.Password.Equals(password))
                {
                    var obj = EntityToViewModel(entity);
                    obj.Password = "";
                    var clientName = _context.Clients.Where(x => x.Id == obj.ClientId).Select(o => o.Name).FirstOrDefault();
                    if(!string.IsNullOrEmpty(clientName))
                    {
                        obj.ClientName = clientName;
                    }
                    return obj;
                }
            }
            return null;
        }

        public IEnumerable<ListUserViewModel> GetByClientId(int clientId)
        {
            return _context.Users
                .Include(x => x.Client)
                .Where(x => x.Client.Id == clientId).AsNoTracking()
                .Select(x => new ListUserViewModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Login = x.Login,
                    Email = x.Email,
                    ClientId = x.ClientId
                }).ToList();
        }

        public EditorUserViewModel Insert(EditorUserViewModel obj)
        {
            var entity = ViewModelToEntity(obj);
            bool isLogin = this.LoginExists(obj.Login);
            if (!isLogin)
            {
                _context.Users.Add(entity);
                _context.SaveChanges();
                obj.Id = entity.Id;
                return obj;
            }
            return null;
            
        }

        public IEnumerable<ListUserViewModel> List()
        {
            return _context.Users.Select(x => new ListUserViewModel()
            {
                Id = x.Id,
                Name = x.Name,
                Login = x.Login,
                Email = x.Email,
                ClientId = x.ClientId
            }).ToList();
        }

        public EditorUserViewModel Update(EditorUserViewModel obj)
        {
            var entity = _context.Users.Find(obj.Id);
            if (entity != null && entity.Id > 0)
            {
                entity.Login = obj.Login;
                entity.Name = obj.Name;
                entity.Status = obj.Status;
                entity.UpdatedDate = DateTime.Now;
                entity.Email = obj.Email;
                entity.ClientId = obj.ClientId;

                _context.SaveChanges();

                return obj;
            }

            return new EditorUserViewModel();
        }

        public UserEntity ViewModelToEntity(EditorUserViewModel obj)
        {
            UserEntity entity = new UserEntity();
            entity.Id = obj.Id;
            entity.ClientId = obj.ClientId;
            entity.Email = obj.Email;
            entity.Login = obj.Login;
            entity.Name = obj.Name;
            entity.Password = obj.Password;
            entity.Status = obj.Status;
            entity.InsertedDate = DateTime.Now;
            entity.UpdatedDate = DateTime.Now;
            return entity;
        }

        private bool LoginExists(string login)
        {
            var entity = _context.Users.Where(x => x.Login.Equals(login)).FirstOrDefault();
            return entity != null && entity.Id > 0;
        }
    }
}
