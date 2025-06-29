namespace FinancialPortfolioManagementApp.Application.Common
{
    public class Result
    {
        public bool IsSuccess { get; protected set; }
        public bool IsFailure => !IsSuccess;
        public List<string> Errors { get; protected set; } = new();
        public string Message { get; protected set; } = string.Empty;

        protected Result(bool isSuccess, string message = "")
        {
            IsSuccess = isSuccess;
            Message = message;
        }

        protected Result(bool isSuccess, IEnumerable<string> errors, string message = "")
        {
            IsSuccess = isSuccess;
            Errors = errors.ToList();
            Message = message;
        }

        public static Result Success(string message = "") => new(true, message);
        public static Result Failure(string error) => new(false, new[] { error });
        public static Result Failure(IEnumerable<string> errors) => new(false, errors);
        public static Result Failure(string error, string message) => new Result(false, message) { Errors = new List<string> { error } };

        public static Result<T> Success<T>(T data, string message = "") => new(data, true, message);
        public static Result<T> Failure<T>(string error) => new Result<T>(default, false, new[] { error });
        public async static Task<Result<T>> FailureAsync<T>(string error) => new Result<T>(default, false, new[] { error });
        public static Result<T> Failure<T>(IEnumerable<string> errors) => new Result<T>(default, false, errors);
    }

    public class Result<T> : Result
    {
        public T Data { get; private set; }

        internal Result(T data, bool isSuccess, string message = "") : base(isSuccess, message)
        {
            Data = data;
        }

        internal Result(T data, bool isSuccess, IEnumerable<string> errors, string message = "") : base(isSuccess, errors, message)
        {
            Data = data;
        }

        public static implicit operator Result<T>(T data) => Success(data);
    }
}
