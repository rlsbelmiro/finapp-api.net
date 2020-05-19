using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FinAppApi.Components.Entities
{
    public class Identity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime InsertedDate { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public DateTime UpdatedDate { get; set; }
    }
}
