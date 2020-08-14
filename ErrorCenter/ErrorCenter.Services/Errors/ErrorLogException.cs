using System;
using System.Collections.Generic;
using System.Text;

namespace ErrorCenter.Services.Errors
{
    public class ErrorLogException : ErrorCenterException
    {
        public ErrorLogException(string message, int statusCode) : base(message, statusCode) { }
    }
}
