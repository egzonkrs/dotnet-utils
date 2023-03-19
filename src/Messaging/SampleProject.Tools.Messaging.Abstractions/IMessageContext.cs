using System.Threading.Tasks;

namespace SampleProject.Tools.Messaging.Abstractions;

/// <summary>
/// Generic Message Context.
/// </summary>
/// <typeparam name="T">The message type.</typeparam>
public interface IMessageContext<T>
{
    /// <summary>
    /// The Message.
    /// </summary>
    IMessage<T> Message { get; }

    /// <summary>
    /// Manually confirm the message.
    /// </summary>
    /// <returns></returns>
    Task ConfirmAsync();
}