using System;

namespace ErrorCenter.Services.Errors {
  public class ErrorCenterException : Exception {
    public int StatusCode { get; }

    public ErrorCenterException(string message) : base(message) { }

    public ErrorCenterException(
      string message,
      int statusCode
    ) : base(message) {
      StatusCode = statusCode;
    }
  }
}
