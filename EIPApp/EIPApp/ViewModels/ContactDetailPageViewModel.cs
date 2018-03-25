using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using Prism.Navigation;
using EIPApp.Models;
using EIPApp.Repositories;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using EIPApp.Helpers;

namespace EIPApp.ViewModels
{
    public class ContactDetailPageViewModel : INotifyPropertyChanged, INavigationAware
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public Employee EmployeeSelectedItem { get; set; }
        private EmployeeRepository repoEmployeeRepository = new EmployeeRepository();
        public ObservableCollection<Employee> EmployeeList { get; set; } = new ObservableCollection<Employee>();
        //public bool IsRefreshing { get; set; } = false;
        //public DelegateCommand DoRefreshCommand { get; set; }
        //public DelegateCommand AddCommand { get; set; }
        public DelegateCommand ItemTappedCommand { get; set; }

        private readonly INavigationService _navigationService;

        public ContactDetailPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;

            ItemTappedCommand = new DelegateCommand(async () =>
            {
                NavigationParameters fooPara = new NavigationParameters();
                fooPara.Add("Users", EmployeeSelectedItem);
                await _navigationService.NavigateAsync("ChatPage", fooPara);
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
            var department = parameters["DepartmentID"] as Department;
            await LoadEmployee(department);            
        }

        private async Task LoadEmployee(Department department)
        {
            await repoEmployeeRepository.GetAsync(department);
            EmployeeList.Clear();
            foreach (var item in repoEmployeeRepository.Items)
            {
                EmployeeList.Add(item);
            }
        }
    }
}
