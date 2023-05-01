using AutoMapper;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TD.WareHouse.DemoApp.Core.Application.ViewModels.Seedwork;
using TD.WareHouse.DemoApp.Core.Domain.Dtos.Items;
using TD.WareHouse.DemoApp.Core.Domain.Services;

namespace TD.WareHouse.DemoApp.Core.Application.ViewModels.Isolation
{
    public class IsolationViewModel : BaseViewModel
    {
        private readonly IApiService _apiService;
        private readonly IMapper _mapper;

        public ObservableCollection<FixIsolationViewModel> IsolationItemLots { get; set; } = new();

        public ICommand LoadIsolationItemLotsCommand { get; set; }

        public IsolationViewModel(IApiService apiService, IMapper mapper)
        {
            _apiService = apiService;
            _mapper = mapper;

            LoadIsolationItemLotsCommand = new RelayCommand(LoadIsolationItemLotsAsync);
        }

        private async void LoadIsolationItemLotsAsync()
        {
            try
            {
                var isolationItemLots = await _apiService.GetUnfixedItemLotsAsync();
                var viewModels = _mapper.Map<IEnumerable<ItemLotDto>, IEnumerable<FixIsolationViewModel>>(isolationItemLots);

                IsolationItemLots = new(viewModels);
                foreach (var viewModel in IsolationItemLots)
                {
                    viewModel.OnRemoved += LoadIsolationItemLotsAsync;
                    viewModel.SetApiService(_apiService);
                }
            }
            catch (HttpRequestException)
            {
                ShowErrorMessage("Đã có lỗi xảy ra: Mất kết nối với server.");
            }
        }

    }
}
