using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using Backend.Data;
using Backend.Models;
using System.Linq;
using Backend.Models.Chat;
using Task = System.Threading.Tasks.Task;

public class ChatHub : Hub
{
    private readonly DataContext _context;

    public ChatHub(DataContext context)
    {
        _context = context;
    }

    public async Task JoinRoom(string roomId)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, roomId);
    }

    public async Task SendMessage(string roomId, string user, string message)
    {
        var chatMessage = new ChatMessage
        {
            UserId = user,
            Message = message,
            Timestamp = DateTime.Now,
            ChatRoomId = int.Parse(roomId)
        };

        _context.ChatMessages.Add(chatMessage);
        await _context.SaveChangesAsync();

        await Clients.Group(roomId).SendAsync("ReceiveMessage", chatMessage);
    }
}
