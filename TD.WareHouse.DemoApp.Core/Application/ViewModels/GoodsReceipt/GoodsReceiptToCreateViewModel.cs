using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TD.WareHouse.DemoApp.Core.Application.ViewModels.Seedwork;
using TD.WareHouse.DemoApp.Core.Domain.Dtos.GoodsReceipts;
using TD.WareHouse.DemoApp.Core.Domain.Exceptions;
using TD.WareHouse.DemoApp.Core.Domain.Models.GoodsReceipts;
using TD.WareHouse.DemoApp.Core.Domain.Services;
using MessageBox = System.Windows.MessageBox;

namespace TD.WareHouse.DemoApp.Core.Application.ViewModels.GoodsReceipt
{
    public class GoodsReceiptToCreateViewModel : BaseViewModel
    {
        private readonly IApiService _apiService;
        public string GoodsReceiptId { get; set; }
        public string EmployeeId { get; set; }
        public string? Supplier { get; set; }
        public List<GoodsReceiptLot> Lots { get; set; }

        public ICommand CreateCommand { get; set; }
        public ICommand DeleteCommand { get; set; }

        public event EventHandler? GoodsReceiptCreated;
        public event EventHandler? GoodsReceiptDeleted;
        public GoodsReceiptToCreateViewModel(IApiService apiService, string goodsReceiptId, string employeeId, string? supplier, List<GoodsReceiptLot> lots)
        {
            _apiService = apiService;
            GoodsReceiptId = goodsReceiptId;
            EmployeeId = employeeId;
            Supplier = supplier;
            Lots = lots;

            CreateCommand = new RelayCommand(ConfirmAsync);
            DeleteCommand = new RelayCommand(DeleteAsync);
        }

        private async void ConfirmAsync()
        {
            var createDto = new CreateGoodsReceiptDto(
                GoodsReceiptId,
                DateTime.Now,
                Supplier,
                EmployeeId,
                Lots.Select(x => new CreateGoodsReceiptLotDto(
                    x.GoodsReceiptLotId,
                    x.Quantity,
                    x.Unit,
                    sublotSize: null,
                    "string",
                    x.ItemId,
                    x.PurchaseOrderNumber,
                    "NV01",
                    note: null)).ToList());
            try
            {
                await _apiService.CreateGoodsReceiptsAsync(createDto);
                MessageBox.Show("Đã Lưu Đơn Mới", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                GoodsReceiptCreated?.Invoke(this, EventArgs.Empty);
            }
            catch (HttpRequestException)
            {
                MessageBox.Show("Đã có lỗi xảy ra: Mất kết nối với server.");
            }
            catch (DuplicateEntityException)
            {
                MessageBox.Show("Đã có lỗi xảy ra: Phiếu xuất kho đã tồn tại.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã có lỗi xảy ra: " + ex.Message);
            }
            

        }
        private void DeleteAsync()
        {
            if (MessageBox.Show("Xác nhận xóa đơn", "Xác nhận", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                GoodsReceiptDeleted?.Invoke(this, EventArgs.Empty);
            }
            else { }

        }

    }
}

