using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinAppApi.Components.ViewModel.CategoryViewModel
{
    public class ListCategoryViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Active { get; set; }
        public bool Analitic { get; set; }
        public int CategoryParentId { get; set; }
    }
}
