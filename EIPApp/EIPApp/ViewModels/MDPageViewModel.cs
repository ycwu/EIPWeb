using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using Acr.UserDialogs;
using EIPApp.Helpers;
using EIPApp.Repositories;
using Prism.Navigation;
using Prism.Services;
using System.ComponentModel;

namespace EIPApp.ViewModels
{
    public class MDPageViewModel : INotifyPropertyChanged, INavigatedAware
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public string UserName { get; set; }
        public DelegateCommand<string> MenuCommand { get; set; }
        public readonly IPageDialogService _dialogService;
        private readonly INavigationService _navigationService;

        public MDPageViewModel(INavigationService navigationService, IPageDialogService dialogService)
        {
            _dialogService = dialogService;
            _navigationService = navigationService;

            MenuCommand = new DelegateCommand<string>(async (x) =>
            {
                switch (x)
                {
                    case "群組即時訊息Tab":
                        await _navigationService.NavigateAsync("xf:///MDPage/NaviPage/ChatTabPage");
                        break;
                    case "群組即時訊息":
                        await _navigationService.NavigateAsync("xf:///MDPage/NaviPage/ChatGroupPage");
                        break;
                    case "即時訊息":
                        await _navigationService.NavigateAsync("xf:///MDPage/NaviPage/ChatPage");
                        break;
                    case "通訊錄":
                        await _navigationService.NavigateAsync("xf:///MDPage/NaviPage/ContactPage");
                        break;
                    case "工作日誌":
                        await _navigationService.NavigateAsync("xf:///MDPage/NaviPage/WorkingLogPage");
                        break;
                    case "請假單":
                        await _navigationService.NavigateAsync("xf:///MDPage/NaviPage/LeaveAppFormPage");
                        break;
                    case "請假單審核":
                        await _navigationService.NavigateAsync("xf:///MDPage/NaviPage/LeaveAppFormManagerPage");
                        break;
                    case "緊急電話清單":
                        await _navigationService.NavigateAsync("xf:///MDPage/NaviPage/OnCallPage");
                        break;
                    case "關  於":
                        await _navigationService.NavigateAsync("xf:///MDPage/NaviPage/AboutPage");
                        break;
                    #region 登  出
                    case "登  出":
                        var fooResult = await _dialogService.DisplayAlertAsync("提醒",
                            "您確定要進行登出作業嗎?", "是", "取消");
                        if (fooResult == true)
                        {
                            #region 要進行登出，所以，清空本機快取資料
                            var fooProgressDialogConfig = new ProgressDialogConfig()
                            {
                                MaskType = MaskType.Black,
                                Title = "請稍後，正在進行登出中..."
                            };
                            using (Acr.UserDialogs.UserDialogs.Instance.Progress(fooProgressDialogConfig))
                            {
                                await MainHelper.CleanRepositories();
                            }
                            await _navigationService.NavigateAsync("xf:///LoginPage");
                            #endregion
                        }
                        break;
                    #endregion
                    default:
                        break;
                }
            });
        }


        public void OnNavigatedFrom(INavigationParameters parameters)
        {

        }

        public void OnNavigatingTo(INavigationParameters parameters)
        {

        }

        public async void OnNavigatedTo(INavigationParameters parameters)
        {
            var fooMyUser = new LoginRepository();
            await fooMyUser.ReadAsync();
            UserName = fooMyUser.Item.MyUser.UserName;
            //IsManager = fooMyUser.Item.MyUser.IsManager;
        }

    }
}
