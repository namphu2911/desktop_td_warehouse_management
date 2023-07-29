using AutoMapper;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TD.WareHouse.DemoApp.Core.Application.Store;
using TD.WareHouse.DemoApp.Core.Application.ViewModels.Seedwork;
using TD.WareHouse.DemoApp.Core.Domain.Dtos.Employees;
using TD.WareHouse.DemoApp.Core.Domain.Exceptions;
using TD.WareHouse.DemoApp.Core.Domain.Services;
using MessageBox = System.Windows.MessageBox;
using DepartmentDto = TD.WareHouse.DemoApp.Core.Domain.Dtos.GoodsIssues.DepartmentDto;
using TD.WareHouse.DemoApp.Core.Domain.Dtos.GoodsIssues;

namespace TD.WareHouse.DemoApp.Core.Application.ViewModels.MiscellaneousData
{
    public class ManageEmployeeViewModel : BaseViewModel
    {
        private readonly IApiService _apiService;
        private readonly IMapper _mapper;
        private readonly EmployeeStore _employeeStore;
        private readonly DepartmentStore _departmentStore;
        public ObservableCollection<string> EmployeeIds => _employeeStore.EmployeeIds;
        public ObservableCollection<string> EmployeeNames => _employeeStore.EmployeeNames;
        public ObservableCollection<string> Names => _departmentStore.Names;
        IDatabaseSynchronizationService _databaseSynchronizationService;

        public string EmployeeId { get; set; } = "";
        public string EmployeeName { get; set; } = ""; 

        private string employeeIdFilter = "";
        private string employeeNameFilter = "";
        public string EmployeeIdFilter
        {
            get
            {
                return employeeIdFilter;
            }
            set
            {
                employeeIdFilter = value;
                if (String.IsNullOrEmpty(value))
                {
                    employeeNameFilter = "";
                    OnPropertyChanged(nameof(EmployeeNameFilter));
                }
                else
                {
                    var employee = _employeeStore.Employees.First(i => i.EmployeeId == employeeIdFilter);
                    employeeNameFilter = employee.EmployeeName;
                    OnPropertyChanged(nameof(EmployeeNameFilter));
                }
            }

        }
        public string EmployeeNameFilter
        {
            get
            {
                return employeeNameFilter;
            }
            set
            {
                employeeNameFilter = value;
                if (String.IsNullOrEmpty(value))
                {
                    employeeNameFilter = "";
                    OnPropertyChanged(nameof(EmployeeIdFilter));
                }
                else
                {
                    var employee = _employeeStore.Employees.First(i => i.EmployeeName == employeeNameFilter);
                    employeeIdFilter = employee.EmployeeId;
                    OnPropertyChanged(nameof(EmployeeIdFilter));
                }
            }
        }

        public string Name { get; set; } = "";
        public string NameFilter { get; set; } = "";
        //
        private List<EmployeeDto> employees = new();
        public ObservableCollection<EmployeeViewModel> Employees { get; set; } = new();
        public ICommand LoadAllEmployeesCommand { get; set; }
        public ICommand FilterEmployeesCommand { get; set; }
        public ICommand CreateEmployeeCommand { get; set; }
        public ICommand LoadBothViewCommand { get; set; }
        //
        private List<DepartmentDto> departments = new();
        public ObservableCollection<DepartmentViewModel> Departments { get; set; } = new();
        public ICommand LoadAllDepartmentsCommand { get; set; }
        public ICommand FilterDepartmentsCommand { get; set; }
        public ICommand CreateDepartmentCommand { get; set; }
        public ManageEmployeeViewModel(IApiService apiService, IMapper mapper, EmployeeStore employeeStore, DepartmentStore departmentStore, IDatabaseSynchronizationService databaseSynchronizationService)
        {
            _apiService = apiService;
            _mapper = mapper;
            _employeeStore = employeeStore;
            _departmentStore = departmentStore;
            _databaseSynchronizationService = databaseSynchronizationService;

            LoadAllEmployeesCommand = new RelayCommand(LoadAllEmployeesAsync);
            FilterEmployeesCommand = new RelayCommand(FilterEmployee);
            CreateEmployeeCommand = new RelayCommand(CreateEmployeeAsync);

            LoadAllDepartmentsCommand = new RelayCommand(LoadAllDepartmentsAsync);
            FilterDepartmentsCommand = new RelayCommand(FilterDepartment);
            CreateDepartmentCommand = new RelayCommand(CreateDepartmentAsync);

            LoadBothViewCommand = new RelayCommand(LoadBothView);
        }

        private async void LoadAllEmployeesAsync()
        {
            employees = (await _apiService.GetAllEmployeesAsync()).ToList();
            var filteredEmployeeDtos = employees.Where(i => i.EmployeeId.Contains(EmployeeIdFilter));
            var filteredEmployees = _mapper.Map<IEnumerable<EmployeeDto>, IEnumerable<EmployeeViewModel>>(filteredEmployeeDtos);

            Employees = new ObservableCollection<EmployeeViewModel>(filteredEmployees);
        }

        private void FilterEmployee()
        {
            var filteredEmployeeDtos = employees.Where(i => i.EmployeeId.Contains(EmployeeIdFilter));
            var filteredEmployees = _mapper.Map<IEnumerable<EmployeeDto>, IEnumerable<EmployeeViewModel>>(filteredEmployeeDtos);

            Employees = new ObservableCollection<EmployeeViewModel>(filteredEmployees);
        }
        private void LoadManageEmployeeView()
        {
            _databaseSynchronizationService.SynchronizeEmployeesData();
            LoadAllEmployeesAsync();
            OnPropertyChanged(nameof(EmployeeIds));
            OnPropertyChanged(nameof(EmployeeNames));
        }

        private async void CreateEmployeeAsync()
        {
            var createEmployeeDto = new CreateEmployeeDto(
                EmployeeId,
                EmployeeName);
            try
            {
                await _apiService.CreateEmployee(createEmployeeDto);
                LoadManageEmployeeView();
            }
            catch (HttpRequestException)
            {
                ShowErrorMessage("Đã có lỗi xảy ra: Mất kết nối với server.");
            }
            catch (DuplicateEntityException)
            {
                ShowErrorMessage("Đã có lỗi xảy ra: Mã vật tư đã tồn tại.");
            }
            catch (Exception)
            {
                ShowErrorMessage("Đã có lỗi xảy ra: Không thể tạo vật tư mới.");
            }
            MessageBox.Show("Đã Cập Nhật", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
            EmployeeId = "";
            EmployeeName = "";
            LoadManageEmployeeView();
        }

        private async void LoadAllDepartmentsAsync()
        {
            departments = (await _apiService.GetAllDepartmentsAsync()).ToList();
            var filteredDepartmentDtos = departments.Where(i => i.Name.Contains(NameFilter));
            var filteredDepartments = _mapper.Map<IEnumerable<DepartmentDto>, IEnumerable<DepartmentViewModel>>(filteredDepartmentDtos);

            Departments = new ObservableCollection<DepartmentViewModel>(filteredDepartments);
        }

        private void FilterDepartment()
        {
            var filteredDepartmentDtos = departments.Where(i => i.Name.Contains(NameFilter));
            var filteredDepartments = _mapper.Map<IEnumerable<DepartmentDto>, IEnumerable<DepartmentViewModel>>(filteredDepartmentDtos);

            Departments = new ObservableCollection<DepartmentViewModel>(filteredDepartments);
        }
        private void LoadManageDepartmentView()
        {
            _databaseSynchronizationService.SynchronizeDepartmentsData();
            LoadAllDepartmentsAsync();
            OnPropertyChanged(nameof(Names));
        }

        private async void CreateDepartmentAsync()
        {
            var createDepartmentDto = new CreateDepartmentDto(Name);
            try
            {
                await _apiService.CreateDepartment(createDepartmentDto);
                LoadManageDepartmentView();
            }
            catch (HttpRequestException)
            {
                ShowErrorMessage("Đã có lỗi xảy ra: Mất kết nối với server.");
            }
            catch (DuplicateEntityException)
            {
                ShowErrorMessage("Đã có lỗi xảy ra: Mã vật tư đã tồn tại.");
            }
            catch (Exception)
            {
                ShowErrorMessage("Đã có lỗi xảy ra: Không thể tạo vật tư mới.");
            }
            MessageBox.Show("Đã Cập Nhật", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
            Name = "";
            LoadManageDepartmentView();
        }

        private void LoadBothView()
        {
            LoadManageEmployeeView();
            LoadManageDepartmentView();
        }
    }
}