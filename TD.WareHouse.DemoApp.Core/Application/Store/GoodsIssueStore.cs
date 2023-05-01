﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TD.WareHouse.DemoApp.Core.Domain.Models.GoodIssues;

namespace TD.WareHouse.DemoApp.Core.Application.Store
{
    public class GoodsIssueStore
    {
        public List<GoodsIssue> GoodsIssues { get; private set; }
        public ObservableCollection<string> Receivers { get; private set; }
        public GoodsIssueStore()
        {
            GoodsIssues = new List<GoodsIssue>();
            Receivers = new ObservableCollection<string>();
        }

        public void SetGoodsIssues(IEnumerable<GoodsIssue> goodsIssues)
        {
            GoodsIssues = goodsIssues.ToList();
            Receivers = new ObservableCollection<string>(goodsIssues.Select(e => e.Receiver).OrderBy(s => s));
        }
    }
}
