using System;
using Microsoft.AspNet.SignalR.Client;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using EIPApp.Models.Chat;

namespace EIPApp.Helpers
{
    public class SignalRGroupClient : INotifyPropertyChanged
    {
        private HubConnection connection;
        private IHubProxy ChatHubProxy;
        public delegate void MessageReceived(string username, string message);
        public event MessageReceived OnMessageReceived;

        public SignalRGroupClient(string url)
        {
            connection = new HubConnection(url);

            connection.StateChanged += (StateChange obj) => {
                OnPropertyChanged("ConnectionState");
            };

            ChatHubProxy = connection.CreateHubProxy("GroupsHub");
            ChatHubProxy.On<string, string>("MessageReceived", (username, text) => {
                OnMessageReceived?.Invoke(username, text);
            });
        }

        public void CreateRoom(string room)
        {
            ChatHubProxy.Invoke("CreateRoom", room);
        }

        public void JoinRoom(string room)
        {
            ChatHubProxy.Invoke("JoinRoom", room);
        }

        public void SendMessage(string room,string username, string text)
        {
            ChatHubProxy.Invoke("SendMessage", room, username, text);
            //ChatHubProxy.Invoke("SendMessage", username, text);
        }

        public Task Start()
        {
            return connection.Start();
        }

        public bool IsConnectedOrConnecting
        {
            get
            {
                return connection.State != ConnectionState.Disconnected;
            }
        }

        public ConnectionState ConnectionState { get { return connection.State; } }

        public static async Task<SignalRGroupClient> CreateAndStart(string url)
        {
            var client = new SignalRGroupClient(url);
            await client.Start();
            return client;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
