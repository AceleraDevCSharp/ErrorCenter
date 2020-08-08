using System;
using System.Collections.Generic;
using System.Text;

namespace ErrorCenter.Services.Errors {
  public class AuthenticationException : ErrorCenterException {
    public AuthenticationException(string message, int statusCode) : base(message, statusCode) { }
  }
}
