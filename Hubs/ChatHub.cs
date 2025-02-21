using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace ProjectsMecsaSPA.Hubs
{
    public class ChatHub : Hub
    {
        private static readonly Dictionary<string, HashSet<string>> Rooms = new();

        public async Task SendMessage(string roomName, string user, string message)
        {
            if (Rooms.ContainsKey(roomName))
            {
                await Clients.Group(roomName).SendAsync("ReceiveMessage", user, roomName, message);
            }
        }

        public async Task JoinRoom(string roomName, string user)
        {
            if (!Rooms.ContainsKey(roomName))
            {
                Rooms[roomName] = new HashSet<string>();
            }
            Rooms[roomName].Add(user);
            await Groups.AddToGroupAsync(Context.ConnectionId, roomName);
        }

        public async Task LeaveRoom(string roomName, string user)
        {
            if (Rooms.ContainsKey(roomName))
            {
                Rooms[roomName].Remove(user);
                if (Rooms[roomName].Count == 0)
                {
                    Rooms.Remove(roomName);
                }
            }
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, roomName);
        }

        public Task<IEnumerable<string>> GetActiveRooms()
        {
            return Task.FromResult<IEnumerable<string>>(Rooms.Keys.ToList());
        }
    }
}

