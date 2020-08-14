using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ErrorCenter.Persistence.EF.Models
{
    public class User : IdentityUser
    {
        public string Avatar { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

    public IEnumerable<ErrorLog> ErrorLogs { get; set; }
  }
}
