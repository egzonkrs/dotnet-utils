using System.Threading;
using System.Threading.Tasks;

namespace SampleProject.Tools.Messaging.Abstractions;

/// <summary>
/// Generic Message Handler
/// </summary>
/// <typeparam name="T">The message type.</typeparam>
public interface IMessageHandler<T>
{
    /// <summary>
    /// Handles a Message.
    /// </summary>
    /// <param name="messageContext">The Message.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/>.</param>
    /// <returns></returns>
    Task HandleAsync(IMessageContext<T> messageContext, CancellationToken cancellationToken);
}