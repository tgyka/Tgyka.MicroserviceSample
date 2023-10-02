using Microsoft.AspNetCore.Mvc;
using Tgyka.Microservice.Base.Model.ApiResponse;

namespace MssqlRestApi.Base.Controller
{
    public class TgykaMicroserviceControllerBase: ControllerBase
    {
        public IActionResult ApiActionResult<T>(ApiResponseDto<T> response) where T : class
        {
            return new ObjectResult(response)
            {
                StatusCode = response.Code
            };
        }
    }
}
