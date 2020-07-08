﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ErrorCenter.Services.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Environment { get; set; }
        public List<ErrorLog> ErrorLogs { get; set; }
    }
}
