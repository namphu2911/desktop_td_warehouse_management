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

namespace TD.WareHouse.DemoApp.Core.Application.ViewModels.Isolation
{
    public class FixIsolationViewModel : BaseViewModel
    {
        private IApiService _apiService;
        public string ItemId { get; set; }
        public string ItemName { get; set; }
        public string Unit { get; set; }
        public string LotId { get; set; }
        public double Quantity { get; set; }
        public string PurchaseOrderNumber { get; set; }
        public string? LocationId { get; set; }
        public ICommand IssueCommand { get; set; }
        public ICommand ReceiveCommand { get; set; }
        public event Action? OnRemoved;
        public event Action? OnException;
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public FixIsolationViewModel()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
            IssueCommand = new RelayCommand(IssueAsync);
            ReceiveCommand = new RelayCommand(ReceiveAsync);
        }
        public FixIsolationViewModel(IApiService apiService, string itemId, string itemName, string unit, string lotId, double quantity, string purchaseOrderNumber, string? locationId)
        {
            _apiService = apiService;
            ItemId = itemId;
            ItemName = itemName;
            Unit = unit;
            LotId = lotId;
            Quantity = quantity;
            PurchaseOrderNumber = purchaseOrderNumber;
            LocationId = locationId;
            

            IssueCommand = new RelayCommand(IssueAsync);
            ReceiveCommand = new RelayCommand(ReceiveAsync);
        }

        public void SetApiService(IApiService apiService)
        {
            try
            {
                _apiService = apiService;
            }
            catch (HttpRequestException)
            {
                ShowErrorMessage("Đã có lỗi xảy ra: Mất kết nối với server.");
            }
        }

        private async void IssueAsync()
        {
            try
            {
                if (MessageBox.Show("Xác nhận xuất đi", "Xác nhận", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    await _apiService.IssueIsolationItemLotsAsync(LotId);
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

        private async void ReceiveAsync()
        {
            try
            {
                if (MessageBox.Show("Xác nhận nhập lại", "Xác nhận", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    await _apiService.ReceiveIsolationItemLotsAsync(LotId);
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

