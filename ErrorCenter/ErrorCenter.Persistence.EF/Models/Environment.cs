using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace ErrorCenter.Persistence.EF.Models {
  public class Environment : IdentityRole {
    public IEnumerable<ErrorLog> ErrorLogs { get; set; }
  }
}
