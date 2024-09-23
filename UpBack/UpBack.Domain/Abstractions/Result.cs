using System.Diagnostics.CodeAnalysis;

namespace UpBack.Domain.Abstractions
{
    public class Result
    {
        protected internal Result(bool isSuccess, Error error)
        {
            if (isSuccess && error != Error.None)
            {
                throw new InvalidOperationException();
            }

            if (!isSuccess && error == Error.None)
            {
                throw new InvalidOperationException();
            }

            IsSuccess = isSuccess;
            Error = error;
        }

        public bool IsSuccess { get; }
        public bool IsFailure => !IsSuccess;
        public Error Error { get; }
        public static Result Success() => new(true, Error.None);
        public static Result Failure(Error error) => new(false, error);
        public static Result<TValue> Success<TValue>(TValue value)
            => new(value, true, Error.None);
        public static Result<TValue> Failure<TValue>(Error error)
            => new(default, false, error);
        public static Result<TValue> Create<TValue>(TValue value)
            => value is not null
            ? Success(value)
            : Failure<TValue>(Error.NullValue);
    }

    // Cuando se trate de evaluar un Result y sus posibles errores para clases genéricas
    public class Result<TValue> : Result
    {
        private readonly TValue? _value;

        protected internal Result(TValue? value, bool isSuccess, Error error) : base(isSuccess, error)
        {
            _value = value;
        }

        [NotNull]
        // Evaluar TValue
        public TValue Value => IsSuccess
            ? _value!
            : throw new InvalidOperationException("The error value result is not acceptable.");

        public static implicit operator Result<TValue>(TValue value) => Create(value);
    }
}
