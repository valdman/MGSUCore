using System;

namespace UserManagment
{
    internal class PolicyException : Exception
    {
        public PolicyException()
        {
        }

        public PolicyException(string message) : base(message)
        {
        }

        public PolicyException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}