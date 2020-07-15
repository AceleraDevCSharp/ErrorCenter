using System;
using System.Runtime.Serialization;

namespace ErrorCenter.Services.Errors {
    [Serializable]
    public class ErrorLogNotFoundException : Exception {
        public ErrorLogNotFoundException() {
        }

        public ErrorLogNotFoundException(string message) : base(message) {
        }

        public ErrorLogNotFoundException(
          string message,
          Exception innerException
        ) : base(message, innerException) {
        }

        protected ErrorLogNotFoundException(
          SerializationInfo info,
          StreamingContext context
        ) : base(info, context) {
        }
    }    
}