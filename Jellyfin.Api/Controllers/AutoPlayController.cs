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
public class AutoPlayController : BaseJellyfinApiController
{
    /// <summary>
    /// Open the details page for an item and optionally start playing it.
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
    [HttpGet("d")]
    [ProducesResponseType(StatusCodes.Status302Found)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public Task<ActionResult> PlayItem(
        [FromQuery(Name = "id")] Guid? itemId,
        [FromQuery(Name = "sid")] string? shortItemId,
        [FromQuery(Name = "tag")] string? tag,
        [FromQuery(Name = "ts")] float? startPositionSeconds)
    {
        string redirectUri = "/web/#/details?";

        if (itemId.HasValue)
        {
            redirectUri += $"id={itemId}";
        }
        else if (shortItemId != null)
        {
            redirectUri += $"sid={shortItemId}";
        }
        else if (tag != null)
        {
            redirectUri += $"tag={tag}";
        }
        else
        {
            return Task.FromResult<ActionResult>(BadRequest());
        }

        if (startPositionSeconds.HasValue)
        {
            redirectUri += $"&ts={startPositionSeconds}";
        }

        return Task.FromResult<ActionResult>(Redirect(redirectUri));
    }
}
