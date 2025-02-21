using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectsMecsaSPA.Services
{
    public class ChatService
    {
        private readonly HubConnection _hubConnection;
        private readonly ConcurrentDictionary<string, HashSet<string>> _rooms = new();

        public ChatService(NavigationManager nav)
        {
            var baseUri = nav.ToAbsoluteUri("/chathub");
            var hubUrl = $"{baseUri.Scheme}://{baseUri.Host}:{baseUri.Port}/chathub";

            _hubConnection = new HubConnectionBuilder()
                .WithUrl(hubUrl)
                .Build();
        }

        public async Task StartAsync()
        {
            await _hubConnection.StartAsync();
        }

        public async Task SendMessage(string roomName, string user, string message)
        {
            await _hubConnection.InvokeAsync("SendMessage", roomName, user, message);
        }

        public async Task JoinRoom(string roomName, string user)
        {
            if (!_rooms.ContainsKey(roomName))
            {
                _rooms[roomName] = new HashSet<string>();
            }
            _rooms[roomName].Add(user);
            await _hubConnection.InvokeAsync("JoinRoom", roomName, user);
        }

        public async Task LeaveRoom(string roomName, string user)
        {
            if (_rooms.ContainsKey(roomName))
            {
                _rooms[roomName].Remove(user);
                if (!_rooms[roomName].Any())
                {
                    _rooms.TryRemove(roomName, out _);
                }
            }
            await _hubConnection.InvokeAsync("LeaveRoom", roomName, user);
        }

        public async Task<IEnumerable<string>> GetActiveRooms()
        {
            return _rooms.Keys.ToList();
        }

        public void OnReceiveMessage(Action<string, string, string> handler)
        {
            _hubConnection.On("ReceiveMessage", handler);
        }

        public void OnUpdateActiveRooms(Action<IEnumerable<string>> handler)
        {
            _hubConnection.On("UpdateActiveRooms", handler);
        }

    }
}


