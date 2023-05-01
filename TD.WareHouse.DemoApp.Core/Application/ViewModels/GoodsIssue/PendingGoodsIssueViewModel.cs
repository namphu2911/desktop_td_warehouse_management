﻿using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TD.WareHouse.DemoApp.Core.Application.ViewModels.Seedwork;
using TD.WareHouse.DemoApp.Core.Domain.Services;

namespace TD.WareHouse.DemoApp.Core.Application.ViewModels.GoodsIssue
{
    public class PendingGoodsIssueViewModel : BaseViewModel
    {
        private readonly IApiService _apiService;
        public string GoodsIssueId { get; set; }
        public DateTime Timestamp { get; set; }
        public string EmployeeName { get; set; }
        public string Receiver { get; set; }
        public PendingGoodsIssueViewModel(IApiService apiService, string goodsIssueId, DateTime timestamp, string employeeName, string receiver)
        {
            _apiService = apiService;
            GoodsIssueId = goodsIssueId;
            Timestamp = timestamp;
            EmployeeName = employeeName;
            Receiver = receiver;
        }
    }
}
