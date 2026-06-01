using Microsoft.AspNetCore.SignalR;

namespace SharpStreamBackend.Hubs;

/// <summary>
/// Signaling hub for managing rooms and coordinating WebRTC connections.
/// </summary>
public class SignalingHub : Hub<ISignalingClient>
{
    /// <summary>
    /// Creates a new streaming room and adds the caller (streamer) to the corresponding group.
    /// </summary>
    /// <param name="roomId">The unique identifier of the room to be created.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    public async Task CreateRoom(string roomId)
    {
        if (string.IsNullOrWhiteSpace(roomId))
        {
            await Clients.Caller.Error("Room ID cannot be empty!");
            return;
        }

        await Groups.AddToGroupAsync(Context.ConnectionId, roomId);

        await Clients.Caller.RoomCreated(roomId);

        Console.WriteLine($"[ROOM CREATED] Streamer {Context.ConnectionId} created room with ID: {roomId}");
    }

    /// <summary>
    /// Connects a viewer to an existing room and notifies other participants in the group.
    /// </summary>
    /// <param name="roomId">The unique identifier of the room to join.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    public async Task JoinRoom(string roomId)
    {
        if (string.IsNullOrWhiteSpace(roomId))
        {
            await Clients.Caller.Error("Room ID cannot be empty!");
            return;
        }

        await Groups.AddToGroupAsync(Context.ConnectionId, roomId);

        await Clients.OthersInGroup(roomId).ViewerJoined(Context.ConnectionId);

        Console.WriteLine($"[USER JOINED] Viewer {Context.ConnectionId} joined room with ID: {roomId}");
    }

    /// <summary>
    /// Automatically invoked when a client disconnects from the hub (e.g., closes the application).
    /// </summary>
    /// <param name="exception">The exception that caused the disconnection, if any; otherwise, null.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        Console.WriteLine($"[DISCONNECTED] Client disconnected: {Context.ConnectionId}");

        await base.OnDisconnectedAsync(exception);
    }
}