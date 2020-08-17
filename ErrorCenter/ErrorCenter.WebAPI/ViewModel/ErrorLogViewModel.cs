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
        public int Id { get; set; }
        public string Environment { get; set; }
        public string Level { get; set; }
        public string Title { get; set; }
        public string Details { get; set; }
        public string Origin { get; set; }
        public string Email { get; set; }
        public int Quantity { get; set; }

    }

}
