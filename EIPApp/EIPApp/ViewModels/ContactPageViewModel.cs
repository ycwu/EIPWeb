using Acr.UserDialogs;
using EIPApp.Helpers;
using EIPApp.Models;
using EIPApp.Repositories;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EIPApp.ViewModels
{
    public class ContactPageViewModel : INotifyPropertyChanged, INavigatedAware
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public Department DepartmentSelectedItem { get; set; }
        public ObservableCollection<Department> DepartmentList { get; set; } = new ObservableCollection<Department>();
        public bool IsRefreshing { get; set; } = false;
        private DepartmentRepository repoDepartment = new DepartmentRepository();
        //public DelegateCommand DoRefreshCommand { get; set; }
        public DelegateCommand AddCommand { get; set; }
        public DelegateCommand ItemTappedCommand { get; set; }
        private readonly INavigationService _navigationService;

        public ContactPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            AddCommand = new DelegateCommand(async () =>
            {
                NavigationParameters fooPara = new NavigationParameters();
                foreach (var item in DepartmentList)
                    if (item.IsSelected == true)
                        fooPara.Add("DepartmentID", item.DepartmentID);                
                await _navigationService.NavigateAsync("ContactDetailPage", fooPara);
            });
            ItemTappedCommand = new DelegateCommand(() =>
            {
                //NavigationParameters fooPara = new NavigationParameters();
                //fooPara.Add("DepartmentID", DepartmentSelectedItem);
                //await _navigationService.NavigateAsync("ContactDetailPage", fooPara);
                DepartmentSelectedItem.IsSelected = !DepartmentSelectedItem.IsSelected;
            });
            //DoRefreshCommand = new DelegateCommand(async () =>
            //{
                //await RetriveRecords();
            //});
        }

        public void OnNavigatedFrom(INavigationParameters parameters)
        {

        }

        public void OnNavigatingTo(INavigationParameters parameters)
        {

        }

        public async void OnNavigatedTo(INavigationParameters parameters)
        {
            await GetDepartmentList();
            /*
            #region 檢查與確認，該頁面是第一次顯示，還是由上一頁面返回到這個頁面
            if (parameters.InternalParameters.ContainsKey(MainHelper.Prism__NavigationMode) == true)
            {
                var fooNaviModeValue = parameters.InternalParameters[MainHelper.Prism__NavigationMode].ToString();
                NavigationMode fooNaviMode = (NavigationMode)Enum.Parse(typeof(NavigationMode), fooNaviModeValue);
                if (fooNaviMode == NavigationMode.New)
                {
                    #region 第一次顯示這個頁面
                    await RefreshCache();
                    #endregion
                }
                else
                {
                    #region 從別的頁面回報這個頁面
                    if (parameters.ContainsKey(MainHelper.CRUDFromDetailKeyName))
                    {
                        var fooItem = parameters[MainHelper.CRUDItemKeyName] as LeaveAppForm;
                        var fooAction = parameters[MainHelper.CRUDFromDetailKeyName] as string;
                        if (fooAction == MainHelper.CRUD_Create)
                        {
                            await CreateRecord(fooItem);
                        }
                        else if (fooAction == MainHelper.CRUD_Delete)
                        {
                            await DeleteRecord(fooItem);
                        }
                        else if (fooAction == MainHelper.CRUD_Update)
                        {
                            await UpdateRecord(fooItem);
                        }
                    }
                    #endregion
                }
            }
            else
            {
            }
            #endregion
            */
        }

        private async Task GetDepartmentList()
        {
            var repoDepartmentRepository = new DepartmentRepository();
            await repoDepartmentRepository.ReadAsync();
            DepartmentList.Clear();
            foreach (var item in repoDepartmentRepository.Items)
            {
                DepartmentList.Add(item);
            }
        }

        /*
        public async Task RetriveRecords()
        {
            #region 進行請假單清單更新
            APIResult fooResult;
            IsRefreshing = true;
            var fooProgressDialogConfig = new ProgressDialogConfig()
            {
                Title = "請稍後，正在進行請假單清單更新中...",
                IsDeterministic = false,
            };
            using (Acr.UserDialogs.UserDialogs.Instance.Progress(fooProgressDialogConfig))
            {
                var fooLoginRepository = new LoginRepository();
                await fooLoginRepository.ReadAsync();
                var fooLeaveAppFormRepository = new LeaveAppFormRepository();
                fooResult = await fooLeaveAppFormRepository.PostByUserIDAsync(new LeaveAppFormByUserModel()
                {
                    Account = fooLoginRepository.Item.MyUser.EmployeeID,
                    Mode = MainHelper.LeaveAppFormUserMode
                });

                if (fooResult.Success == false)
                {
                    if (await MainHelper.CheckAccessToken(fooResult) == false)
                    {
                        IsRefreshing = false;
                        return;
                    }

                    try
                    {
                        var fooAlertConfig = new AlertConfig()
                        {
                            Title = "警告",
                            Message = $"更新資料發生了錯誤 {Environment.NewLine}{fooResult.Message}",
                            OkText = "確定"
                        };
                        CancellationTokenSource fooCancelSrc = new CancellationTokenSource(10000);
                        await Acr.UserDialogs.UserDialogs.Instance.AlertAsync(fooAlertConfig, fooCancelSrc.Token);
                    }
                    catch (OperationCanceledException)
                    {
                    }
                }
            }
            if (fooResult.Success == true)
            {
                await RefreshCache();
            }
            IsRefreshing = false;
            #endregion
        }
        */

        /*
        public async Task CreateRecord(LeaveAppForm leaveAppForm)
        {
            #region 進行請假單新增
            APIResult fooResult;
            IsRefreshing = true;
            var fooProgressDialogConfig = new ProgressDialogConfig()
            {
                Title = "請稍後，正在進行請假單清單新增中...",
                IsDeterministic = false,
            };
            using (Acr.UserDialogs.UserDialogs.Instance.Progress(fooProgressDialogConfig))
            {
                var fooLeaveAppFormRepository = new LeaveAppFormRepository();
                fooResult = await fooLeaveAppFormRepository.PostAsync(leaveAppForm);
                if (fooResult.Success == false)
                {
                    if (await MainHelper.CheckAccessToken(fooResult) == false)
                    {
                        return;
                    }

                    try
                    {
                        var fooAlertConfig = new AlertConfig()
                        {
                            Title = "警告",
                            Message = $"新增資料發生了錯誤 {Environment.NewLine}{fooResult.Message}",
                            OkText = "確定"
                        };
                        CancellationTokenSource fooCancelSrc = new CancellationTokenSource(10000);
                        await Acr.UserDialogs.UserDialogs.Instance.AlertAsync(fooAlertConfig, fooCancelSrc.Token);
                    }
                    catch (OperationCanceledException)
                    {
                    }
                }
            }
            if (fooResult.Success == true)
            {
                await RetriveRecords();
            }
            IsRefreshing = false;
            #endregion
        }

        public async Task DeleteRecord(LeaveAppForm leaveAppForm)
        {
            #region 進行請假單刪除
            APIResult fooResult;
            IsRefreshing = true;
            var fooProgressDialogConfig = new ProgressDialogConfig()
            {
                Title = "請稍後，正在進行請假單清單刪除中...",
                IsDeterministic = false,
            };
            using (Acr.UserDialogs.UserDialogs.Instance.Progress(fooProgressDialogConfig))
            {
                var fooLeaveAppFormRepository = new LeaveAppFormRepository();
                fooResult = await fooLeaveAppFormRepository.DeleteAsync(leaveAppForm);
                if (fooResult.Success == false)
                {
                    if (await MainHelper.CheckAccessToken(fooResult) == false)
                    {
                        return;
                    }

                    try
                    {
                        var fooAlertConfig = new AlertConfig()
                        {
                            Title = "警告",
                            Message = $"刪除資料發生了錯誤 {Environment.NewLine}{fooResult.Message}",
                            OkText = "確定"
                        };
                        CancellationTokenSource fooCancelSrc = new CancellationTokenSource(10000);
                        await Acr.UserDialogs.UserDialogs.Instance.AlertAsync(fooAlertConfig, fooCancelSrc.Token);
                    }
                    catch (OperationCanceledException)
                    {
                    }
                }
            }
            if (fooResult.Success == true)
            {
                await RetriveRecords();
            }
            IsRefreshing = false;
            #endregion
        }

        public async Task UpdateRecord(LeaveAppForm leaveAppForm)
        {
            #region 進行請假單清單更新
            APIResult fooResult;
            IsRefreshing = true;
            var fooProgressDialogConfig = new ProgressDialogConfig()
            {
                Title = "請稍後，正在進行請假單清單更新中...",
                IsDeterministic = false,
            };
            using (Acr.UserDialogs.UserDialogs.Instance.Progress(fooProgressDialogConfig))
            {
                var fooLeaveAppFormRepository = new LeaveAppFormRepository();
                fooResult = await fooLeaveAppFormRepository.PutAsync(leaveAppForm);
                if (fooResult.Success == false)
                {
                    if (await MainHelper.CheckAccessToken(fooResult) == false)
                    {
                        return;
                    }

                    try
                    {
                        var fooAlertConfig = new AlertConfig()
                        {
                            Title = "警告",
                            Message = $"更新資料發生了錯誤 {Environment.NewLine}{fooResult.Message}",
                            OkText = "確定"
                        };
                        CancellationTokenSource fooCancelSrc = new CancellationTokenSource(10000);
                        await Acr.UserDialogs.UserDialogs.Instance.AlertAsync(fooAlertConfig, fooCancelSrc.Token);
                    }
                    catch (OperationCanceledException)
                    {
                    }
                }
            }
            if (fooResult.Success == true)
            {
                await RetriveRecords();
            }
            IsRefreshing = false;
            #endregion
        }
        */
    }
}
