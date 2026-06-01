namespace SharpStream.Backend.Hubs;

/// <summary>
/// Defines the methods that the signaling server can invoke on the client side.
/// </summary>
public interface ISignalingClient
{
    /// <summary>
    /// Notifies the creator (streamer) that the room was successfully created on the server.
    /// </summary>
    /// <param name="roomId">The unique identifier of the created room.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    Task RoomCreated(string roomId);

    /// <summary>
    /// Notifies room participants (primarily the streamer) that a new viewer has connected.
    /// Used to initiate the WebRTC offer/answer exchange process.
    /// </summary>
    /// <param name="viewerConnectionId">The connection identifier (ConnectionId) of the connected viewer.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    Task ViewerJoined(string viewerConnectionId);

    /// <summary>
    /// Notifies room participants that a viewer has disconnected.
    /// Signals the client to close and clean up the specific WebRTC P2P connection.
    /// </summary>
    /// <param name="viewerConnectionId">The connection identifier (ConnectionId) of the viewer who left.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    Task ViewerLeft(string viewerConnectionId);

    /// <summary>
    /// Sends an error message to the client when an operation on the hub fails
    /// (e.g., passing an empty room ID).
    /// </summary>
    /// <param name="message">The error message text for user display or logging.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    Task Error(string message);
}