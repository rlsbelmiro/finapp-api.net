using FinAppApi.Components.Conection;
using FinAppApi.Components.Entities;
using FinAppApi.Components.ViewModel.CategoryViewModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinAppApi.Components.Repositories
{
    public class CategoryRepository : IReposity<CategoryEntity, ListCategoryViewModel, EditorCategoryViewModel>
    {
        private readonly FinAppContext _context;

        public CategoryRepository(FinAppContext context)
        {
            _context = context;
        }
        public void Delete(int id)
        {
            var entity = _context.Categories.Find(id);
            if(entity != null)
            {
                _context.Categories.Remove(entity);
                _context.SaveChanges();
            }
        }

        public EditorCategoryViewModel EntityToViewModel(CategoryEntity obj)
        {

            return new EditorCategoryViewModel()
            {
                Id = obj.Id,
                Active = obj.Active,
                Analitic = obj.Analitic,
                CategoryParentId = obj.CategoryParentId.GetValueOrDefault(),
                ClientId = obj.ClientId,
                Name = obj.Name,
                Number = obj.Number,
                Type = obj.Type
            };
        }

        public ListCategoryViewModel EntityToViewModelList(CategoryEntity obj)
        {
            return new ListCategoryViewModel()
            {
                Id = obj.Id,
                Name = obj.Name,
                Active = obj.Active,
                Analitic = obj.Analitic,
                CategoryParentId = obj.CategoryParentId.GetValueOrDefault()
            };
        }

        public EditorCategoryViewModel Get(int id)
        {
            var entity = _context.Categories.Find(id);
            if (entity != null)
                return EntityToViewModel(entity);

            return null;
        }

        public IEnumerable<ListCategoryViewModel> GetByClientId(int clientId)
        {
            return _context.Categories
                .Where(x => x.ClientId == clientId).AsNoTracking()
                .Select(x => new ListCategoryViewModel() { 
                    Id = x.Id,
                    Name = x.Name,
                    Active = x.Active,
                    Analitic = x.Analitic,
                    CategoryParentId = x.CategoryParentId.GetValueOrDefault()
                }).ToList();
        }

        public EditorCategoryViewModel Insert(EditorCategoryViewModel obj)
        {
            var entity = ViewModelToEntity(obj);
            _context.Categories.Add(entity);
            _context.SaveChanges();
            obj.Id = entity.Id;
            return obj;
        }

        public IEnumerable<ListCategoryViewModel> List()
        {
            return _context.Categories
                .Select(x => new ListCategoryViewModel()
                {
                    Id = x.Id,
                    Name = x.Name
                }).ToList();
        }

        public EditorCategoryViewModel Update(EditorCategoryViewModel obj)
        {
            var entity = _context.Categories.Find(obj.Id);
            if(entity != null)
            {
                entity.Active = obj.Active;
                entity.Analitic = obj.Analitic;
                entity.CategoryParentId = obj.CategoryParentId;
                entity.ClientId = obj.ClientId;
                entity.UpdatedDate = DateTime.Now;
                entity.Name = obj.Name;
                entity.Number = obj.Number;
                entity.Type = obj.Type;
                return obj;
            }
            return null;
        }

        public CategoryEntity ViewModelToEntity(EditorCategoryViewModel obj)
        {
            return new CategoryEntity()
            {
                Id = obj.Id,
                Active = obj.Active,
                Analitic = obj.Analitic,
                CategoryParentId = obj.CategoryParentId > 0 ? obj.CategoryParentId : new Nullable<int>(),
                ClientId = obj.ClientId,
                Name = obj.Name,
                Number = obj.Number,
                Type = obj.Type,
                InsertedDate = DateTime.Now,
                UpdatedDate = DateTime.Now
            };
        }
    }
}
