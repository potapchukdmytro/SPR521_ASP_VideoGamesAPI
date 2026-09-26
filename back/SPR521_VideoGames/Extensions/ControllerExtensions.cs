using Microsoft.AspNetCore.Mvc;
using SPR521_VideoGames.BLL.Dtos;

namespace SPR521_VideoGames.Extensions
{
    public static class ControllerExtensions
    {
        public static IActionResult GetHttpResponse(this ControllerBase controller, ResponseDto response)
        {
            if(response.IsSuccess)
            {
                return controller.Ok(response);
            }

            return controller.BadRequest(response);
        }
    }
}
