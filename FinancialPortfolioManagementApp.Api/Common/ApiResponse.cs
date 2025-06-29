using FinancialPortfolioManagementApp.Application.Common;

namespace FinancialPortfolioManagementApp.Api.Common
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public T? Data { get; set; }
        public List<string> Errors { get; set; } = new();

        public static ApiResponse<T> FromResult(Result<T> result)
        {
            return new ApiResponse<T>
            {
                Success = result.IsSuccess,
                Message = result.Message,
                Data = result.Data,
                Errors = result.Errors
            };
        }
    }

    public class ApiResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public List<string> Errors { get; set; } = new();

        public static ApiResponse FromResult(Result result)
        {
            return new ApiResponse
            {
                Success = result.IsSuccess,
                Message = result.Message,
                Errors = result.Errors
            };
        }
    }
}
