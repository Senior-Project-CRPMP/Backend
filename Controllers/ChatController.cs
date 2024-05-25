using Backend.Data;
using Backend.Models.Chat;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChatController : ControllerBase
    {
        private readonly DataContext _context;

        public ChatController(DataContext context)
        {
            _context = context;
        }

        [HttpGet("rooms")]
        public async Task<ActionResult<IEnumerable<ChatRoom>>> GetChatRooms()
        {
            return  await _context.ChatRooms.ToListAsync();
        }

        [HttpPost("rooms")]
        public async Task<ActionResult<ChatRoom>> CreateChatRoom([FromBody] ChatRoom chatRoom)
        {
            _context.ChatRooms.Add(chatRoom);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetChatRoom), new { id = chatRoom.Id }, chatRoom);
        }

        [HttpGet("rooms/{id}")]
        public async Task<ActionResult<ChatRoom>> GetChatRoom(int id)
        {
            var chatRoom = await _context.ChatRooms
                                          .Include(r => r.Participants)
                                          .Include(r => r.Messages)
                                          .FirstOrDefaultAsync(r => r.Id == id);

            if (chatRoom == null)
            {
                return NotFound();
            }

            return chatRoom;
        }


        [HttpGet("rooms/{roomId}/messages")]
        public async Task<ActionResult<IEnumerable<ChatMessage>>> GetMessages(int roomId)
        {
            return await _context.ChatMessages.Where(m => m.ChatRoomId == roomId).ToListAsync();
        }
    }

}
