using Acr.UserDialogs;
using EIPApp.Models;
using EIPApp.Repositories;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace EIPApp.ViewModels
{
    public class LoginPageViewModel : INotifyPropertyChanged, INavigationAware
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public string Account { get; set; } = "";
        public string Password { get; set; } = "";
        public bool UsingHttpGet { get; set; } = true;

        public DelegateCommand LoginCommand { get; set; }

        private readonly INavigationService _navigationService;

        public LoginPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;

            LoginCommand = new DelegateCommand(async () =>
            {
                var repoLogin = new LoginRepository();

                var pdcMessage = new ProgressDialogConfig()
                {
                    MaskType = MaskType.Black,
                    Title = "請稍後，正在身分驗證中..."
                };
                using (Acr.UserDialogs.UserDialogs.Instance.Progress(pdcMessage))
                {
                    APIResult apiResult;
                    if (UsingHttpGet == true)
                        apiResult = await repoLogin.GetAsync(Account, Password);
                    else
                        apiResult = await repoLogin.PostAsync(Account, Password);
                    if (apiResult.Success == false)
                    {
                        var config = new Acr.UserDialogs.AlertConfig()
                        {
                            Title = "警告",
                            Message = $"進行使用者身分驗證失敗，原因：{Environment.NewLine}{apiResult.Message}",
                            OkText = "確定",
                        };

                        await Acr.UserDialogs.UserDialogs.Instance.AlertAsync(config);
                    }
                    else
                    {
                        var repoSystemStatus = new SystemStatusRepository();
                        await repoSystemStatus.ReadAsync();
                        repoSystemStatus.Item.LoginMethodAction = UsingHttpGet;
                        repoSystemStatus.Item.AccessToken = repoLogin.Item.AccessToken;
                        await repoSystemStatus.SaveAsync();
                        await _navigationService.NavigateAsync("xf:///MDPage/NaviPage/AboutPage");
                    }
                }
            });
#if DEBUG
            Account = "ycwu";
            Password = "m46772";
#endif
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {

        }

        public void OnNavigatingTo(NavigationParameters parameters)
        {

        }

        public async void OnNavigatedTo(NavigationParameters parameters)
        {
            var repoSystemStatus = new SystemStatusRepository();
            await repoSystemStatus.ReadAsync();
            UsingHttpGet = repoSystemStatus.Item.LoginMethodAction;
        }

    }
}
