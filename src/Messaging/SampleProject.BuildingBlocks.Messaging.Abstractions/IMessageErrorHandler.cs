using System.Threading;
using System.Threading.Tasks;

namespace SampleProject.BuildingBlocks.Messaging.Abstractions;

/// <summary>
/// Generic Message Handler
/// </summary>
/// <typeparam name="T">The message type.</typeparam>
public interface IMessageErrorHandler<T>
{
    /// <summary>
    /// Handles Error.
    /// </summary>
    /// <param name="messageContext">The Error message context.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/>.</param>
    /// <returns></returns>
    Task HandleAsync(IErrorMessageContext<T> messageContext, CancellationToken cancellationToken);
}
