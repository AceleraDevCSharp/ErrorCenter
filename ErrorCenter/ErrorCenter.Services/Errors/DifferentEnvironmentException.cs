using System;
using System.Runtime.Serialization;

namespace ErrorCenter.Services.Errors {
    [Serializable]
    public class DifferentEnvironmentException : Exception {
        public DifferentEnvironmentException() {
        }

        public DifferentEnvironmentException(string message) : base(message) {
        }

        public DifferentEnvironmentException(
          string message,
          Exception innerException
        ) : base(message, innerException) {
        }

        protected DifferentEnvironmentException(
          SerializationInfo info,
          StreamingContext context
        ) : base(info, context) {
        }
    }    
}