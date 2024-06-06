using Backend.Data;
using Backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChatRoomController : ControllerBase
    {
        private readonly DataContext _context;

        public ChatRoomController(DataContext context)
        {
            _context = context;
        }

        // Create a new chatroom
        public class CreateChatRoomRequest
        {
            public string Name { get; set; }
            public List<string> ParticipantUserIds { get; set; }
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateChatRoom([FromBody] CreateChatRoomRequest request)
        {
            if (request.ParticipantUserIds == null || request.ParticipantUserIds.Count < 2)
            {
                return BadRequest("A chatroom must have at least two participants.");
            }

            // Check if a chat room with the same participants already exists
            var existingChatRoom = await _context.ChatRooms
                .Include(cr => cr.Participants)
                .FirstOrDefaultAsync(cr =>
                    cr.Participants.Count == request.ParticipantUserIds.Count &&
                    cr.Participants.All(p => request.ParticipantUserIds.Contains(p.UserId))
                );

            if (existingChatRoom != null)
            {
                return BadRequest("A chatroom with the same participants already exists.");
            }


            var chatRoom = new ChatRoom
            {
                Name = request.Name,
                CreatedAt = DateTime.UtcNow,
                Participants = request.ParticipantUserIds.Select(id => new ChatRoomParticipant
                {
                    UserId = id // Only set the UserId here
                }).ToList()
            };

            _context.ChatRooms.Add(chatRoom);
            await _context.SaveChangesAsync();

            // Now that chatRoom has been added to the context and saved, you can set the ChatRoomId for each participant
            foreach (var participant in chatRoom.Participants)
            {
                participant.ChatRoomId = chatRoom.ChatRoomId; // Associate participant with the chat room
            }

            // Save changes again to update the participants with the correct ChatRoomId
            await _context.SaveChangesAsync();


            return Ok(chatRoom);
        }

        [HttpPost("chatroom/{chatRoomId}/messages/create")]
        public async Task<IActionResult> CreateMessage(int chatRoomId, [FromBody] CreateMessageRequest request)
        {
            var chatRoom = await _context.ChatRooms.FindAsync(chatRoomId);

            if (chatRoom == null)
            {
                return NotFound("Chat room not found.");
            }

            var message = new ChatMessage
            {
                ChatRoomId = chatRoomId,
                UserId = request.SenderUserId,
                Content = request.Content,
                SentAt = DateTime.UtcNow
            };

            _context.ChatMessages.Add(message);
            await _context.SaveChangesAsync();

            return Ok(message);
        }

        public class CreateMessageRequest
        {
            public string SenderUserId { get; set; }
            public string Content { get; set; }
        }



        // Get all chatrooms
        [HttpGet("all")]
        public async Task<IActionResult> GetAllChatRooms()
        {
            var chatRooms = await _context.ChatRooms.ToListAsync();
            return Ok(chatRooms);
        }

        // Get chatrooms for a specific user
        [HttpGet("user/{userId}/chatrooms")]
        public async Task<IActionResult> GetChatRoomsForUser(string userId)
        {
            var chatRooms = await _context.ChatRooms
                .Where(cr => cr.Participants.Any(p => p.UserId == userId))
                .ToListAsync();
            return Ok(chatRooms);
        }

        // Get users for a specific chatroom
        [HttpGet("chatroom/{chatRoomId}/users")]
        public async Task<IActionResult> GetUsersForChatRoom(int chatRoomId)
        {
            var users = await _context.ChatRoomParticipants
                .Where(crp => crp.ChatRoomId == chatRoomId)
                .Select(crp => crp.User)
                .ToListAsync();
            return Ok(users);
        }

        // Get messages for a specific chatroom
        [HttpGet("chatroom/{chatRoomId}/messages")]
        public async Task<IActionResult> GetMessagesForChatRoom(int chatRoomId)
        {
            var messages = await _context.ChatMessages
                .Where(m => m.ChatRoomId == chatRoomId)
                .ToListAsync();
            return Ok(messages);
        }

        [HttpGet("chatrooms/with-users")]
        public async Task<IActionResult> GetChatRoomsWithUsers([FromQuery] List<string> userIds)
        {
            if (userIds == null || userIds.Count == 0)
            {
                return BadRequest("User IDs are required.");
            }

            var chatRooms = await _context.ChatRooms
                .Where(cr => userIds.All(userId => cr.Participants.Any(p => p.UserId == userId)))
                .ToListAsync();
            return Ok(chatRooms);
        }

        // DELETE endpoint to delete a chatroom by ID
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteChatRoom(int id)
        {
            var chatRoom = await _context.ChatRooms.FindAsync(id);

            if (chatRoom == null)
            {
                return NotFound("Chat room not found.");
            }

            // Delete related chat messages
            var relatedMessages = await _context.ChatMessages
                .Where(m => m.ChatRoomId == id)
                .ToListAsync();

            _context.ChatMessages.RemoveRange(relatedMessages);
            await _context.SaveChangesAsync();

            // Now delete the chat room
            _context.ChatRooms.Remove(chatRoom);
            await _context.SaveChangesAsync();

            return Ok($"Chatroom {id} deleted successfully.");
        }


    }
}
