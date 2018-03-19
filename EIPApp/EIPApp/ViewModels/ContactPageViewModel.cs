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
    public class ContactPageViewModel : INotifyPropertyChanged, INavigatedAware  
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

        public ContactPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;

            DepartmentSelectedCommand = new DelegateCommand(async () =>
            {
                var fooBackup = DepartmentSelectedItem;
                // 變更到最新的 Picker2 的可選取清單
                EmployeeSource = repoEmployee.GetAsync(DepartmentSelectedItem);
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
            //DepartmentSource = PickerSource.GetPicker1Source();
            //EmployeeSource = PickerSource.GetPicker1Source();
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

    public static class PickerSource
    {
        public static ObservableCollection<string> GetPicker1Source()
        {
            return new ObservableCollection<string>() { "A", "B" };
        }

        public static ObservableCollection<string> GetPickr2Source(string dependby)
        {
            if (dependby == "A")
            {
                return new ObservableCollection<string> { "1", "2", "3" };
            }
            else
            {
                return new ObservableCollection<string> { "1", "2", "4" };
            }
        }
    }
}
