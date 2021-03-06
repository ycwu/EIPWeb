﻿using System;
using Microsoft.AspNet.SignalR.Client;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using EIPApp.Models;

namespace EIPApp.Helpers
{
    public class SignalRClient : INotifyPropertyChanged
    {
        private HubConnection connection;
        private IHubProxy hubProxy;

        public delegate void MessageReceived(string username, string message);
        //public delegate void MessageReceived(Message message);
        public event MessageReceived OnMessageReceived;
        //public event EventHandler<Message> OnMessageReceived;

        public SignalRClient(string url)
        {
            connection = new HubConnection(url);

            connection.StateChanged += (StateChange obj) => {
                OnPropertyChanged("ConnectionState");
            };

            hubProxy = connection.CreateHubProxy("ChatHub");
            //hubProxy.On("sendToAll", (string name, string text) => OnMessageReceived(this, new Message { UserName = name, MessageText = text }));
            //hubProxy.On<Message>("MessageReceived", (message) =>
            //{
            //        OnMessageReceived?.Invoke(message);
            //});

            //hubProxy.On("sendMessage", (string name, string message) => OnMessageReceived(this, new Message
            //{
            //    UserName = name,
            //    MessageText = message
            //}));
            hubProxy.On<string, string>("MessageReceived", (username, text) =>
            {
                OnMessageReceived?.Invoke(username, text);
            });

        }

        public void SendMessage(string username, string text)
        {
            hubProxy.Invoke("SendMessage", username, text);
        }

        public void SendMessage(Message message)
        {
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

        public static async Task<SignalRClient> CreateAndStart(string url)
        {
            var client = new SignalRClient(url);
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
