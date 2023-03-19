using System;
using Microsoft.AspNetCore.Mvc;
using SampleProject.Tools.Models;

namespace SampleProject.Tools.Web.AspNetCore.Extensions;

/// <summary>
/// The <see cref="ControllerBase"/> Extensions.
/// </summary>
public static class ControllerBaseExtensions
{
    /// <summary>
    /// Creates an HTTP result based on the <see cref="Result"/>.
    /// </summary>
    /// <param name="controller">The <see cref="ControllerBase"/>.</param>
    /// <param name="result">The <see cref="Result"/>.</param>
    /// <param name="onSuccess">The callback called when result is successful.</param>
    public static ActionResult ResponseResult(this ControllerBase controller,
        Result result, Func<ActionResult> onSuccess)
    {
        if (result.IsForbidden()) return controller.Forbid();

        if (result.IsNotFound()) return controller.NotFound();

        if (result.IsConflict()) return controller.Conflict();

        if (result.IsError) return controller.BadRequest(result.ErrorCodes);

        return onSuccess();
    }
}