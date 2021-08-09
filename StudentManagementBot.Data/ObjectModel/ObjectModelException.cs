using System;
using System.Runtime.Serialization;


namespace StudentManagementBot.Data.ObjectModel
{
    [Serializable]
    public class ObjectModelException : Exception
    {
        public ObjectModelException()
        {
        }

        public ObjectModelException(string message) : base(message)
        {
        }

        public ObjectModelException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ObjectModelException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}