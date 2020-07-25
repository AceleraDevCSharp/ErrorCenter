using System;
using System.Collections.Generic;
using System.Text;

namespace ErrorCenter.Services.Errors {
  public class UserException : ErrorCenterException {
    public UserException(string message, int statusCode) : base(message, statusCode) { }
  }
}
