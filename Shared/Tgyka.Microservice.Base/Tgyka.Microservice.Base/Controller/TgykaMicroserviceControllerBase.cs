using FoodDelivery.HelperBase.Model.ApiResponse;
using Microsoft.AspNetCore.Mvc;

namespace MssqlRestApi.Base.Controller
{
    public class TgykaMicroserviceControllerBase: ControllerBase
    {
        public IActionResult ApiJsonResult<T>(ApiResponseDto<T> response) where T : class
        {
            return new ObjectResult(response)
            {
                StatusCode = response.Code
            };
        }
    }
}
