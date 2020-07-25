using System;
using System.Runtime.Serialization;

namespace ErrorCenter.Services.Errors
{
    [Serializable]
    public class ErrorLogArchivedException : Exception
    {
        public ErrorLogArchivedException()
        {
        }

        public ErrorLogArchivedException(string message) : base(message)
        {
        }

        public ErrorLogArchivedException(
          string message,
          Exception innerException
        ) : base(message, innerException)
        {
        }

        protected ErrorLogArchivedException(
          SerializationInfo info,
          StreamingContext context
        ) : base(info, context)
        {
        }
    }
}