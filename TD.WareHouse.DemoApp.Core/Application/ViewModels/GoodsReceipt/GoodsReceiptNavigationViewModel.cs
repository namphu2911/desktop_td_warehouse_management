using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TD.WareHouse.DemoApp.Core.Application.ViewModels.GoodsReceipt
{
    public class GoodsReceiptNavigationViewModel
    {
        public GoodsReceiptMaterialsViewModel GoodsReceiptMaterials { get; set; }
        public GoodsReceiptCompletedViewModel GoodsReceiptCompleted { get; set; }
        public CreateGoodsReceiptCompletedViewModel CreateGoodsReceiptCompleted { get; set; }
        public GoodsReceiptNavigationViewModel(GoodsReceiptMaterialsViewModel goodsReceiptMaterials, GoodsReceiptCompletedViewModel goodsReceiptCompleted, CreateGoodsReceiptCompletedViewModel createGoodsReceiptCompleted)
        {
            GoodsReceiptMaterials = goodsReceiptMaterials;
            GoodsReceiptCompleted = goodsReceiptCompleted;
            CreateGoodsReceiptCompleted = createGoodsReceiptCompleted;
        }
    }
}
