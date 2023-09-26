namespace Tgyka.Microservice.Base.Model.ApiResponse
{
    public class ApiResponseDto<TResult> where TResult: class
    {
        public ApiResponseDto(int code, object data, string[] errors)
        {
            Code = code;
            Data = data;
            Errors = errors;
        }

        public int Code { get; private set; }
        public object? Data { get; private set; }
        public string[]? Errors { get; private set; }

        public static ApiResponseDto<TResult> Success(int code, object data)
        {
            return new ApiResponseDto<TResult>(code, data, null);
        }

        public static ApiResponseDto<TResult> Error(int code, string[] errors)
        {
            return new ApiResponseDto<TResult>(code, null, errors);
        }
    }
}
