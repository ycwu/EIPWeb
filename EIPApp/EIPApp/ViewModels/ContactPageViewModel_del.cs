using EIPApp.Repositories;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace EIPApp.ViewModels
{
    public class ContactPageViewModel_del : INotifyPropertyChanged, INavigatedAware  
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private DepartmentRepository repoDepartment = new DepartmentRepository();
        private EmployeeRepository repoEmployee = new EmployeeRepository();
        public ObservableCollection<string> DepartmentSource { get; set; } = new ObservableCollection<string>();
        public ObservableCollection<string> EmployeeSource { get; set; } = new ObservableCollection<string>();
        public string DepartmentSelectedItem { get; set; }
        public string EmployeeSelectedItem { get; set; }
        public string Title { get; set; }

        public DelegateCommand DepartmentSelectedCommand { get; set; }
        private readonly INavigationService _navigationService;

        public ContactPageViewModel_del(INavigationService navigationService)
        {
            _navigationService = navigationService;

            DepartmentSelectedCommand = new DelegateCommand(async () =>
            {
                var fooBackup = EmployeeSelectedItem;
                // 變更到最新的 Picker2 的可選取清單
                await LoadEmployeePickerSource();//repoEmployee.GetAsync(DepartmentSelectedItem);
                if (EmployeeSource.Contains(fooBackup))
                {
                    await Task.Delay(100);
                    EmployeeSelectedItem = EmployeeSource.FirstOrDefault(x => x == fooBackup);
                }
            });
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {

        }

        public void OnNavigatingTo(NavigationParameters parameters)
        {

        }

        public async void OnNavigatedTo(NavigationParameters parameters)
        {
            await LoadDepartmentPickerSource();
            await LoadEmployeePickerSource();
        }

        private async Task LoadDepartmentPickerSource()
        {
            await repoDepartment.ReadAsync();
            DepartmentSource.Clear();
            foreach (var item in repoDepartment.Items)
                DepartmentSource.Add(item.DepartmentName);
        }

        private async Task LoadEmployeePickerSource()
        {
            await repoEmployee.ReadAsync();
            EmployeeSource.Clear();
            foreach (var item in repoEmployee.Items)
                EmployeeSource.Add(item.Name);
        }
    }    
}
