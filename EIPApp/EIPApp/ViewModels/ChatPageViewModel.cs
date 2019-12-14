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
    public class ChatPageViewModel : BindableBase, INavigatedAware
    {
        #region Repositories (遠端或本地資料存取)
        public SignalRClient SignalRClient = new SignalRClient(MainHelper.SignalRURL);
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
        public ChatPageViewModel(INavigationService navigationService, IEventAggregator eventAggregator, IPageDialogService dialogService)
        {
            #region 相依性服務注入的物件
            _dialogService = dialogService;
            _eventAggregator = eventAggregator;
            _navigationService = navigationService;
            #endregion

            #region 頁面中綁定的命令
            //SignalRClient.OnMessageReceived += _OnMessageReceived;
            //void _OnMessageReceived(object sender,Message e)
            //{
            //    ChatContentCollection.Add(new ChatContent
            //    {
            //        姓名 = e.UserName,
            //        姓名文字顏色 = Color.Blue,
            //        對話人圖片 = Girl,
            //        對話內容 = e.MessageText,
            //        對話類型 = 對話類型.他人,
            //        對話文字顏色 = Color.Green
            //    });
            //}
            //SignalRClient.OnMessageReceived += (Message message) => {
            //    if (message.UserName != UserName)//MainHelper.UserLoginService.Item.MyUser.UserName)
            //        ChatContentCollection.Add(new ChatContent
            //        {
            //            姓名= message.UserName,
            //            姓名文字顏色 = Color.Blue,
            //            對話人圖片 = Girl,
            //            對話內容 = message.MessageText,
            //            對話類型 = 對話類型.他人,
            //            對話文字顏色 = Color.Green
            //        });
            //};

            SignalRClient.OnMessageReceived += (username, message) =>
            {
                if (username != UserName)//MainHelper.UserLoginService.Item.MyUser.UserName)
                    ChatContentCollection.Add(new ChatContent
                    {
                        姓名 = username,
                        姓名文字顏色 = Color.Blue,
                        對話人圖片 = Girl,
                        對話內容 = message,
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
                //SignalRClient.SendMessage(UserName, 送出對話內容);//MainHelper.UserLoginService.Item.MyUser.UserName
                SignalRClient.SendMessage(new Message { UserName = UserName, MessageText = 送出對話內容 });
                //if (送出對話內容 == "1")
                //{
                //    ChatContentCollection.Add(new ChatContent
                //    {
                //        對話人圖片 = Boy,
                //        對話內容 = "妳是小蕙嗎? 好久不見了",
                //        對話類型 = 對話類型.自己,
                //        對話文字顏色 = Color.Purple
                //    });
                //    ChatContentCollection.Add(new ChatContent
                //    {
                //        對話人圖片 = Girl,
                //        對話內容 = "沒想到你還記得我",
                //        對話類型 = 對話類型.他人,
                //        對話文字顏色 = Color.Green
                //    });
                //}
                //else if (送出對話內容 == "2")
                //{
                //    ChatContentCollection.Add(new ChatContent
                //    {
                //        對話人圖片 = Boy,
                //        對話內容 = "明天出來喝咖啡吧",
                //        對話類型 = 對話類型.自己,
                //        對話文字顏色 = Color.Purple
                //    });
                //    ChatContentCollection.Add(new ChatContent
                //    {
                //        對話人圖片 = Girl,
                //        對話內容 = "好呀 ~~",
                //        對話類型 = 對話類型.他人,
                //        對話文字顏色 = Color.Green
                //    });
                //}
                //else
                //{
                //    ChatContentCollection.Add(new ChatContent
                //    {
                //        對話人圖片 = Boy,
                //        對話內容 = 送出對話內容,
                //        對話類型 = 對話類型.自己,
                //        對話文字顏色 = Color.Purple
                //    });

                //}


                送出對話內容 = "";
            });
            #endregion

            #region 事件聚合器訂閱

            #endregion


            //show an error if the connection doesn't succeed for some reason
            SignalRClient.Start().ContinueWith(task => {
                if (task.IsFaulted)
                    //MainPage.DisplayAlert("Error", "An error occurred when trying to connect to SignalR: " + task.Exception.InnerExceptions[0].Message, "OK");
                    Acr.UserDialogs.UserDialogs.Instance.Alert(task.Exception.InnerExceptions[0].Message, "警告", "確定");

            });


            //try to reconnect every 10 seconds, just in case
            Device.StartTimer(TimeSpan.FromSeconds(10), () => {
                if (!SignalRClient.IsConnectedOrConnecting)
                    SignalRClient.Start();

                return true;
            });

        }

        #endregion

        #region Navigation Events (頁面導航事件)
        public void OnNavigatingTo(INavigationParameters parameters)
        {

        }

        public void OnNavigatedFrom(INavigationParameters parameters)
        {

        }

        public async void OnNavigatedTo(INavigationParameters parameters)
        {
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
            //ChatContentCollection.Add(new ChatContent
            //{
            //    對話人圖片 = Girl,
            //    對話內容 = "我最近手機掉了，更換了電話號碼",
            //    對話類型 = 對話類型.他人,
            //    對話文字顏色 = Color.Green
            //});

            //ChatContentCollection.Add(new ChatContent
            //{
            //    對話人圖片 = Girl,
            //    對話內容 = "這是我的新電話號碼 0987654321",
            //    對話類型 = 對話類型.他人,
            //    對話文字顏色 = Color.Green
            //});
            //ChatContentCollection.Add(new ChatContent
            //{
            //    對話人圖片 = Boy,
            //    對話內容 = "妳現在在哪裡呀？",
            //    對話類型 = 對話類型.自己,
            //    對話文字顏色 = Color.Purple
            //});
            var fooMyUser = new LoginRepository();
            await fooMyUser.ReadAsync();
            UserName = fooMyUser.Item.MyUser.UserName;
            await Task.Delay(100);

        }
        #endregion

    }
}
