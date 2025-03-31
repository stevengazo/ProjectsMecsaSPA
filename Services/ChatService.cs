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
            // Notificar sobre la lista de usuarios actualizada
            await NotifyUserListUpdated(roomName);
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
            // Notificar sobre la lista de usuarios actualizada
            await NotifyUserListUpdated(roomName);
        }

        // Método para notificar a los clientes sobre la lista de usuarios actualizada
        private async Task NotifyUserListUpdated(string roomName)
        {
            if (_rooms.ContainsKey(roomName))
            {
                var users = _rooms[roomName].ToList();
                await _hubConnection.SendAsync("UpdateActiveUsers", roomName, users);
            }
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

        // Recibir la lista de usuarios activos para cada sala
        public void OnUpdateActiveUsers(Action<string, List<string>> handler)
        {
            _hubConnection.On("UpdateActiveUsers", handler);
        }
    }
}
