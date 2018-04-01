using System;
using Microsoft.AspNet.SignalR.Client;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using EIPApp.Models;

namespace EIPApp.Helpers
{
    public class SignalRGroupClient : INotifyPropertyChanged
    {
        private HubConnection connection;
        private IHubProxy hubProxy;
        public delegate void MessageReceived(Message message);
        public event MessageReceived OnMessageReceived;

        public SignalRGroupClient(string url)
        {
            connection = new HubConnection(url);

            connection.StateChanged += (StateChange obj) => {
                OnPropertyChanged("ConnectionState");
            };

            hubProxy = connection.CreateHubProxy("GroupsHub");
            hubProxy.On<Message>("MessageReceived", (message) => {
                OnMessageReceived?.Invoke(message);
            });
        }

        public void CreateRoom(string room)
        {
            hubProxy.Invoke("CreateRoom", room);
        }

        public void JoinRoom(string room)
        {
            hubProxy.Invoke("JoinRoom", room);
        }

        public void SendMessage(string roomID,string userName, string messageText)
        {
            Message message = new Message();
            message.MessageID = Guid.NewGuid();
            message.MessageTime = DateTime.Now;
            message.RoomID = roomID;
            message.UserName = userName;
            message.MessageText = messageText;
            hubProxy.Invoke("SendMessage", message);
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
