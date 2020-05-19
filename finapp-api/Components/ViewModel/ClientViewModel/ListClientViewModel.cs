using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FinAppApi.Components.ViewModel.ClientViewModel
{
    public class ListClientViewModel
    {
        public int Id { get; set; }
        public string Document { get; set; }
        public string Name { get;set; }
    }
}
