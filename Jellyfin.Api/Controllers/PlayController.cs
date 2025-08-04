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
    /// <param name="shortItemId">Short id of the item.</param>
    /// <param name="tag">Unique tag identifying the item.</param>
    /// <param name="startPositionSeconds">At what position to start playing the item, in seconds.</param>
    /// <returns>
    /// <response code="302">Redirected to play page.</response>
    /// <response code="400">Bad request.</response>
    /// A <see cref="Task" /> that represents the asynchronous operation to play the item.
    /// The task result contains an <see cref="NoContentResult"/>.
    /// </returns>
    [HttpGet("Play")]
    [ProducesResponseType(StatusCodes.Status302Found)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public Task<ActionResult> PlayItem(
        [FromQuery(Name = "id")] Guid? itemId,
        [FromQuery(Name = "sid")] string? shortItemId,
        [FromQuery(Name = "tag")] string? tag,
        [FromQuery(Name = "ts")] float? startPositionSeconds = 0.0f)
    {
        if (itemId.HasValue)
        {
            return Task.FromResult<ActionResult>(Redirect($"/web/#/details?id={itemId}&ts={startPositionSeconds}"));
        }
        else if (shortItemId != null)
        {
            return Task.FromResult<ActionResult>(Redirect($"/web/#/details?sid={shortItemId}&ts={startPositionSeconds}"));
        }
        else if (tag != null)
        {
            return Task.FromResult<ActionResult>(Redirect($"/web/#/details?tag={tag}&ts={startPositionSeconds}"));
        }
        else
        {
            return Task.FromResult<ActionResult>(BadRequest());
        }
    }
}
