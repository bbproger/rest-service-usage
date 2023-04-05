using System;

namespace Ui
{
    public class Result<T>
    {
        public T Value { get; private set; }
        public Exception Exception { get; private set; }
        public bool Success => Exception == null;

        private Result()
        {
            
        }

        public static Result<T> Ok(T value)
        {
            return new Result<T>
            {
                Value = value,
                Exception = null
            };
        }

        public static Result<T> Fail(Exception exception)
        {
            return new Result<T>
            {
                Value = default,
                Exception = exception
            };
        }
    }
}