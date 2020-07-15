using System;
using System.Collections.Generic;
using System.Text;

namespace ErrorCenter.Persistence.EF.Models {
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Environment { get; set; }

        public IEnumerable<ErrorLog> ErrorLogs { get; set; }
    }
}
