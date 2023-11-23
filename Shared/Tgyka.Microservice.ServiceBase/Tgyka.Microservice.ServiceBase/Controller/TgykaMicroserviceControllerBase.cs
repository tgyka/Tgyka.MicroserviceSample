using Microsoft.AspNetCore.Mvc;
using Tgyka.Microservice.Base.Model.ApiResponse;

namespace MssqlRestApi.Base.Controller
{
    public class TgykaMicroserviceControllerBase: ControllerBase
    {
        public IActionResult ApiActionResult<T>(ApiResponse<T> response)
        {
            return new ObjectResult(response)
            {
                StatusCode = response.Code
            };
        }
    }
}
