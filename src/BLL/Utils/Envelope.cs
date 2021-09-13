using System;
using BLL.Utils.Extensions;

namespace BLL.Utils
{
    public class Envelope<T>
    {
        protected internal Envelope(T body, string errorMessage)
        {
            Body = body;
            ErrorMessage = errorMessage;
            TimeGenerated = DateTime.UtcNow;
            IsSuccess = errorMessage.HasNoValue();
        }

        public T Body { get; }
        public string ErrorMessage { get; }
        public DateTime TimeGenerated { get; }
        public bool IsSuccess { get; }
    }

    public class Envelope : Envelope<string>
    {
        private Envelope(string errorMessage) : base(errorMessage.HasValue() ? null : "success", errorMessage)
        {
        }

        public static Envelope<T> Ok<T>(T result)
        {
            return new Envelope<T>(result, null);
        }

        public static Envelope Ok()
        {
            return new Envelope(null);
        }

        public static Envelope Error(string errorMessage)
        {
            return new Envelope(errorMessage);
        }

        public static Envelope<T> Error<T>(T error, string errorMessage)
        {
            return new Envelope<T>(error, errorMessage);
        }
    }
}