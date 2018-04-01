using EIPApp.Helpers;
using EIPApp.Models;
using EIPApp.Repositories;
using EIPApp.Views;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace EIPApp.ViewModels
{
    public class ChatGroupPageViewModel : BindableBase, INavigationAware
    {
        #region Repositories (遠端或本地資料存取)
        public SignalRGroupClient SignalRGroupClient = new SignalRGroupClient(MainHelper.SignalRURL);
        #endregion

        #region ViewModel Property (用於在 View 中作為綁定之用)
        #region ChatContentCollection
        private ObservableCollection<ChatContent> _ChatContentCollection = new ObservableCollection<ChatContent>();
        /// <summary>
        /// ChatContentCollection
        /// </summary>
        public ObservableCollection<ChatContent> ChatContentCollection
        {
            get { return _ChatContentCollection; }
            set { SetProperty(ref _ChatContentCollection, value); }
        }
        #endregion

        #region 送出對話內容
        private string _送出對話內容;
        /// <summary>
        /// 送出對話內容
        /// </summary>
        public string 送出對話內容
        {
            get { return this._送出對話內容; }
            set { this.SetProperty(ref this._送出對話內容, value); }
        }
        #endregion

        #endregion

        #region Field 欄位
        public string Boy { get; set; } = "boy.png";
        public string Girl { get; set; } = "girl.png";
        public string UserName { get; set; } = "";
        public readonly IPageDialogService _dialogService;
        private readonly INavigationService _navigationService;
        private readonly IEventAggregator _eventAggregator;

        public DelegateCommand 送出Command { get; set; }
        #endregion

        #region Constructor 建構式
        public ChatGroupPageViewModel(INavigationService navigationService, IEventAggregator eventAggregator, IPageDialogService dialogService)
        {

            #region 相依性服務注入的物件

            _dialogService = dialogService;
            _eventAggregator = eventAggregator;
            _navigationService = navigationService;
            #endregion

            #region 頁面中綁定的命令
            SignalRGroupClient.OnMessageReceived += (message) => {
                if (message.UserName != UserName)//MainHelper.UserLoginService.Item.MyUser.UserName)
                    ChatContentCollection.Add(new ChatContent
                    {                        
                        姓名 = message.UserName,
                        姓名文字顏色 = Color.Blue,
                        對話人圖片 = Girl,
                        對話內容 = message.MessageText,
                        對話類型 = 對話類型.他人,
                        對話文字顏色 = Color.Green
                    });
            };

            送出Command = new DelegateCommand(() =>
            {
                ChatContentCollection.Add(new ChatContent
                {
                    對話人圖片 = Boy,
                    對話內容 = 送出對話內容,
                    對話類型 = 對話類型.自己,
                    對話文字顏色 = Color.Purple
                });
                SignalRGroupClient.SendMessage("聊天室1", UserName, 送出對話內容);//MainHelper.UserLoginService.Item.MyUser.UserName
                送出對話內容 = "";
            });
            #endregion

            #region 事件聚合器訂閱

            #endregion


            //show an error if the connection doesn't succeed for some reason
            SignalRGroupClient.Start().ContinueWith(task => {
                if (task.IsFaulted)
                    //MainPage.DisplayAlert("Error", "An error occurred when trying to connect to SignalR: " + task.Exception.InnerExceptions[0].Message, "OK");
                    Acr.UserDialogs.UserDialogs.Instance.Alert(task.Exception.InnerExceptions[0].Message, "警告", "確定");
                SignalRGroupClient.JoinRoom("聊天室1");
            });


            //try to reconnect every 10 seconds, just in case
            Device.StartTimer(TimeSpan.FromSeconds(10), () => {
                if (!SignalRGroupClient.IsConnectedOrConnecting)
                    SignalRGroupClient.Start();

                return true;
            });

        }

        #endregion

        #region Navigation Events (頁面導航事件)
        public void OnNavigatingTo(NavigationParameters parameters)
        {

        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {

        }

        public async void OnNavigatedTo(NavigationParameters parameters)
        {
            //SignalRGroupClient.CreateRoom("room1");
            await ViewModelInit();
        }
        #endregion

        #region 設計時期或者執行時期的ViewModel初始化
        #endregion

        #region 相關事件
        #endregion

        #region 相關的Command定義
        #endregion

        #region 其他方法

        /// <summary>
        /// ViewModel 資料初始化
        /// </summary>
        /// <returns></returns>
        private async Task ViewModelInit()
        {
            ChatContentCollection.Clear();
            var fooMyUser = new LoginRepository();
            await fooMyUser.ReadAsync();
            UserName = fooMyUser.Item.MyUser.UserName;
            await Task.Delay(100);

        }
        #endregion

    }
}
