using System;
using Microsoft.AspNet.SignalR.Client;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
//using EIPApp.Models;
using EIPApp.Models;

namespace EIPApp.Helpers
{
    public class SignalRGroupClient : INotifyPropertyChanged
    {
        private HubConnection connection;
        private IHubProxy hubProxy;
        public delegate void MessageReceived(Message message);
        //public delegate void MessageReceived(string username, string message, string type);
        public event MessageReceived OnMessageReceived;
        public string UserID { get; set; }
        public SignalRGroupClient(string url,string userID)
        {
            connection = new HubConnection(url);
            UserID = userID;
            connection.StateChanged += (StateChange obj) => {
                OnPropertyChanged("ConnectionState");
            };

            hubProxy = connection.CreateHubProxy("Group");//ChatHub
            hubProxy["userID"] = userID;
            hubProxy.On<Message>("MessageReceived", (message) =>
            {
                OnMessageReceived?.Invoke(message);
            });
            //hubProxy.On<string, string, string>("MessageReceived", (username, message, type) => {
            //    OnMessageReceived?.Invoke(username, message, type);
            //});
        }

        public void CreateRoom(int roomID, string roomName)
        {
            hubProxy.Invoke("CreateRoom", roomID , roomName);
        }

        public void JoinRoom(string room)
        {
            hubProxy.Invoke("JoinRoom", room);
        }

        public void SendMessage(int roomID,string userName, string messageText)
        {
            Message message = new Message();
            //message.MessageID = Guid.NewGuid();
            message.MessageTime = DateTime.Now;
            message.RoomID = roomID;
            message.UserName = userName;
            message.MessageText = messageText;
            //hubProxy.Invoke("SendMessage", message);
            hubProxy.Invoke("SendMessage", roomID, userName, messageText);
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

        public static async Task<SignalRGroupClient> CreateAndStart(string url, string userID)
        {
            var client = new SignalRGroupClient(url, userID);
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
