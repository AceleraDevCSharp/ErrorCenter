using System;
using System.Collections.Generic;
using System.Text;

namespace ErrorCenter.Services.Errors {
  public class EnvironmentException : ErrorCenterException {
    public EnvironmentException(string message, int statusCode) :
      base(message, statusCode) { }
  }
}
