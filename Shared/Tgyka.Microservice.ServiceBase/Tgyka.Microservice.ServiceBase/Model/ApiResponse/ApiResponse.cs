namespace Tgyka.Microservice.Base.Model.ApiResponse
{
    public class ApiResponse<TResult>
    {
        public ApiResponse(int code, object data, string[] errors)
        {
            Code = code;
            Data = data;
            Errors = errors;
        }

        public int Code { get; private set; }
        public object? Data { get; private set; }
        public string[]? Errors { get; private set; }

        public static ApiResponse<TResult> Success(int code, TResult data)
        {
            return new ApiResponse<TResult>(code, data, null);
        }

        public static ApiResponse<TResult> Error(int code, string[] errors)
        {
            return new ApiResponse<TResult>(code, null, errors);
        }

        public static ApiResponse<TResult> Error(int code, string error)
        {
            return new ApiResponse<TResult>(code, null,new string[] { error });
        }
    }
}
