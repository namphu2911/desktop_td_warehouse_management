﻿using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TD.WareHouse.DemoApp.Core.Application.ViewModels.Seedwork;
using TD.WareHouse.DemoApp.Core.Domain.Services;
using MessageBox = System.Windows.MessageBox;

namespace TD.WareHouse.DemoApp.Core.Application.ViewModels.Inventory
{
    public class FixLotAdjustmentViewModel : BaseViewModel
    {
        private IApiService _apiService;
        public string LotId { get; set; }
        public string ItemId { get; set; }
        public string ItemName { get; set; }
        public string Unit { get; set; }
        public double BeforeQuantity { get; set; }
        public double AfterQuantity { get; set; }
        public double ChangedQuantity => AfterQuantity - BeforeQuantity;
        public string NewPurchaseOrderNumber { get; set; }
        public string OldPurchaseOrderNumber { get; set; }
        public string EmployeeName { get; set; }
        public string Note { get; set; }
        public ICommand FixCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public event Action? OnRemoved;
        public event Action? OnException;
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public FixLotAdjustmentViewModel()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
            FixCommand = new RelayCommand(FixAsync);
            DeleteCommand = new RelayCommand(DeleteAsync);
        }

        public FixLotAdjustmentViewModel(IApiService apiService, string lotId, string itemId, string itemName, double beforeQuantity, double afterQuantity, string newPurchaseOrderNumber, string oldPurchaseOrderNumber, string unit, string employeeName, string note)
        {
            _apiService = apiService;
            LotId = lotId;
            ItemId = itemId;
            ItemName = itemName;
            BeforeQuantity = beforeQuantity;
            AfterQuantity = afterQuantity;
            NewPurchaseOrderNumber = newPurchaseOrderNumber;
            OldPurchaseOrderNumber = oldPurchaseOrderNumber;
            Unit = unit;
            EmployeeName = employeeName;
            Note = note;

            FixCommand = new RelayCommand(FixAsync);
            DeleteCommand = new RelayCommand(DeleteAsync);
        }

        public void SetApiService(IApiService apiService)
        {
            try
            {
                _apiService = apiService;
            }
            catch (HttpRequestException)
            {
                OnException?.Invoke();
                ShowErrorMessage("Đã có lỗi xảy ra: Mất kết nối với server.");
            }
        }

        private async void FixAsync()
        {
            try
            {
                if (MessageBox.Show("Xác nhận kiểm kê", "Xác nhận", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    await _apiService.FixLotAdjustmentAsync(LotId);
                    OnRemoved?.Invoke();
                    MessageBox.Show("Đã Cập Nhật", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else { }
            }
            catch (HttpRequestException)
            {
                OnException?.Invoke();
                ShowErrorMessage("Đã có lỗi xảy ra: Mất kết nối với server.");
            }

        }

        private async void DeleteAsync()
        {
            try
            {
                if (MessageBox.Show("Xác nhận xóa", "Xác nhận", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    await _apiService.DeleteLotAdjustmentAsync(LotId);
                    OnRemoved?.Invoke();
                    MessageBox.Show("Đã Cập Nhật", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else { }
            }
            catch (HttpRequestException)
            {
                OnException?.Invoke();
                ShowErrorMessage("Đã có lỗi xảy ra: Mất kết nối với server.");
            }
        }
    }
}
