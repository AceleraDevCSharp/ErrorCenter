using System;
using System.Collections.Generic;
using System.Text;

namespace ErrorCenter.Services.Errors {
  public class FileUploadException : ErrorCenterException {
    public FileUploadException(string message, int statusCode)
      : base(message, statusCode) { }
  }
}
