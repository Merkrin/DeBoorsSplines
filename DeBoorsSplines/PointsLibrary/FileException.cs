using System;

namespace PointsLibrary
{
    [Serializable]
    public class FileException : Exception
    {
        public FileException() { }
        public FileException(string message) : base(message) { }
        public FileException(string message, Exception inner) : base(message, inner) { }
        protected FileException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
