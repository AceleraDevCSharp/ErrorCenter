using System;
using System.Collections.Generic;

using Flunt.Notifications;

namespace ErrorCenter.Services.Errors {
  public class ViewModelException : ErrorCenterException {
    public IReadOnlyCollection<Notification> Details { get; }

    public ViewModelException(
      string message,
      int statusCode,
      IReadOnlyCollection<Notification> details
    ) : base(message, statusCode) {
      Details = details;
    }
  }
}
