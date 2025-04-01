namespace SharedModels.Response
{
    public class ApiResponse<T>
    {
        public T? Data { get; set; }
        public required string Message { get; set; }
        public bool Success { get; set; }

        // Success response
        public static ApiResponse<T> SuccessResponse(T data, string? message = null)
        {
            return new ApiResponse<T>
            {
                Success = true,
                Data = data,
                Message = message ?? "Operation completed successfully"
            };
        }

        // Error response
        public static ApiResponse<T> ErrorResponse(string message)
        {
            return new ApiResponse<T>
            {
                Success = false,                
                Message = message
            };
        }
    }
}
