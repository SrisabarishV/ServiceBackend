namespace Backend.Common
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public T? Result { get; set; }

        public static ApiResponse<T> SuccessResponse(T data, string message = "Success")
        {
            return new ApiResponse<T>
            {
                Success = true,
                Message = message,
                Result = data
            };
        }

        public static ApiResponse<T> FailResponse(string message)
        {
            return new ApiResponse<T>
            {
                Success = false,
                Message = message,
                Result = default
            };
        }
    }
}
