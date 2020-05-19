using FinAppApi.Components.Repositories;
using FinAppApi.Components.ViewModel.CategoryViewModel;
using Flunt.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinAppApi.Components.Business
{
    public class CategoryBusiness
    {
        private CategoryRepository categoryRepository;

        public IReadOnlyCollection<Notification> Notifications { get; set; }
        public bool IsValid { get { return this.Notifications == null || !this.Notifications.Any(); } }

        public CategoryBusiness(CategoryRepository repository)
        {
            categoryRepository = repository;
        }

        public EditorCategoryViewModel Save(EditorCategoryViewModel obj)
        {
            obj.Validate();
            if (obj.Valid)
            {
                if (obj.Id > 0)
                    obj = categoryRepository.Update(obj);
                else
                    obj = categoryRepository.Insert(obj);

                return obj;
            }
            else
            {
                this.Notifications = obj.Notifications;
                return new EditorCategoryViewModel();
            }
        }

        public void Delete(int id)
        {
            categoryRepository.Delete(id);
        }

        public IEnumerable<ListCategoryViewModel> List()
        {
            return categoryRepository.List();
        }

        public EditorCategoryViewModel Get(int id)
        {
            var obj = categoryRepository.Get(id);
            return obj;

        }

        public IEnumerable<ListCategoryViewModel> GetByClientId(int id)
        {
            return categoryRepository.GetByClientId(id);
        }

    }
}
