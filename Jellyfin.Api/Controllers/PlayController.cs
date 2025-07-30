using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Jellyfin.Api.Controllers;

/// <summary>
/// (Auto) play controller.
/// </summary>
[Route("")]
public class PlayController : BaseJellyfinApiController
{
    /// <summary>
    /// Auto play an item.
    /// </summary>
    /// <param name="itemId">The item id.</param>
    /// <param name="startPositionSeconds">At what position to start playing the item, in seconds.</param>
    /// <returns>
    /// <response code="302">Redirected to play page.</response>
    /// A <see cref="Task" /> that represents the asynchronous operation to play the item.
    /// The task result contains an <see cref="NoContentResult"/>.
    /// </returns>
    [HttpGet("Play/{itemId}")]
    [ProducesResponseType(StatusCodes.Status302Found)]
    public Task<RedirectResult> PlayItem(
        [FromRoute, Required] Guid itemId,
        [FromQuery(Name = "ts")] float? startPositionSeconds = 0.0f)
    {
        return Task.FromResult(Redirect($"/web/#/details?id={itemId}&ts={startPositionSeconds}"));
    }
}
