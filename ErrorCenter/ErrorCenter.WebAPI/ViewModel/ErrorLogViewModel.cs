using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ErrorCenter.WebAPI.ViewModel
{
    public class ErrorLogViewModel
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(30, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 3)]
        public string Environment { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(30, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 3)]
        public string Level { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(500, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 10)]
        public string Title { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(1500, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 10)]
        public string Details { get; set; }

        [JsonIgnore]
        public DateTime CreatedAt { get; set; }

        [JsonIgnore]
        public DateTime? ArquivedAt { get; set; }

        [JsonIgnore]
        public DateTime? DeletedAt { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(100, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 3)]
        public string Origin { get; set; }
        public string UserEmail { get; set; }
        public int Quantity { get; set; }
    }
}
