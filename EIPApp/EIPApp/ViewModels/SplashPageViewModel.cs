using EIPApp.Models;
using EIPApp.Repositories;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace EIPApp.ViewModels
{
    public class SplashPageViewModel : INotifyPropertyChanged, INavigationAware
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public string LoadingMessage { get; set; } = "";
        private APIResult APIResult { get; set; }
        private readonly INavigationService _navigationService;

        public SplashPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;

        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {

        }

        public void OnNavigatingTo(NavigationParameters parameters)
        {

        }

        public async void OnNavigatedTo(NavigationParameters parameters)
        {
            var fooAllSuccess = true;

            if (Plugin.Connectivity.CrossConnectivity.Current.IsConnected == false)
            {
                fooAllSuccess = false;
            }
            else
            {
                LoadingMessage = "正在更新 員工檔/部門檔清單 中";
                var repoDepartment = new DepartmentRepository();
                APIResult = await repoDepartment.GetAsync();
                if (APIResult.Success == false)
                {
                    using (Acr.UserDialogs.UserDialogs.Instance.Toast($"無法進行更新[部門檔]清單 中 ({APIResult.Message})", TimeSpan.FromMilliseconds(1500)))
                    {
                        await Task.Delay(2000);
                    }
                    fooAllSuccess = false;
                }
                else
                {
                    var repoEmployee = new EmployeeRepository();
                    APIResult = await repoEmployee.GetAsync();
                    if (APIResult.Success == false)
                    {
                        using (Acr.UserDialogs.UserDialogs.Instance.Toast($"無法進行更新[員工檔]清單 中 ({APIResult.Message})", TimeSpan.FromMilliseconds(1500)))
                        {
                            await Task.Delay(2000);
                        }
                        fooAllSuccess = false;
                    }                    
                }
            }

            if (fooAllSuccess == true)
            {
                LoadingMessage = "系統資料更新完成";
                var fooSystemStatusRepository = new SystemStatusRepository();
                await fooSystemStatusRepository.ReadAsync();
                if (string.IsNullOrEmpty(fooSystemStatusRepository.Item.AccessToken))
                {
                    await _navigationService.NavigateAsync("xf:///LoginPage");
                }
                else
                {
                    await _navigationService.NavigateAsync("xf:///MDPage/NaviPage/AboutPage");
                }
            }
            else
            {
                var config = new Acr.UserDialogs.ConfirmConfig()
                {
                    Title = "警告",
                    Message = "可能因為網路問題，無法進行系統資料更新，您確定要繼續執行嗎? (強制繼續執行 App，但有可能造成 App 不正常運作)",
                    OkText = "確定",
                    CancelText = "停止"
                };
                await Task.Delay(1000);
                var fooConfirmResult =
                    await Acr.UserDialogs.UserDialogs.Instance.ConfirmAsync(config);

                if (fooConfirmResult == false)
                {
                    LoadingMessage = "請強制關閉 App，檢查網路是否可用，並且重新啟動 App";
                    return;
                }
                else
                {
                    LoadingMessage = "強制繼續執行 App";
                }
            }

            // 切換到首頁或者登入頁面

        }

    }
}
