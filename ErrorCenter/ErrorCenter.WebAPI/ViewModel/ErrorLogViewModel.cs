using ErrorCenter.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ErrorCenter.WebAPI.ViewModel
{
    public class ErrorLogViewModel
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(11, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 3)]
        public string Environment { get; set; }

        public string Level { get; set; }
        public string Title { get; set; }
        public string Details { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? ArquivedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public string Origin { get; set; }
        public int IdUser { get; set; }
        public int Quantity { get; set; }
    }
}
