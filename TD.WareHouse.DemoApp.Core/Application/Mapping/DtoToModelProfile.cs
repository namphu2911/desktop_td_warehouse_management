﻿using AutoMapper;
using TD.WareHouse.DemoApp.Core.Application.ViewModels.Alarm;
using TD.WareHouse.DemoApp.Core.Application.ViewModels.GoodsReceipt;
using TD.WareHouse.DemoApp.Core.Application.ViewModels.History;
using TD.WareHouse.DemoApp.Core.Application.ViewModels.Inventory;
using TD.WareHouse.DemoApp.Core.Application.ViewModels.Isolation;
using TD.WareHouse.DemoApp.Core.Application.ViewModels.ShelfManagement;
using TD.WareHouse.DemoApp.Core.Application.ViewModels.StockCard;
using TD.WareHouse.DemoApp.Core.Domain.Dtos.Employees;
using TD.WareHouse.DemoApp.Core.Domain.Dtos.GoodsIssues;
using TD.WareHouse.DemoApp.Core.Domain.Dtos.GoodsReceipts;
using TD.WareHouse.DemoApp.Core.Domain.Dtos.Inventory;
using TD.WareHouse.DemoApp.Core.Domain.Dtos.Items;
using TD.WareHouse.DemoApp.Core.Domain.Dtos.Location;
using TD.WareHouse.DemoApp.Core.Domain.Dtos.LotAdjustment;
using TD.WareHouse.DemoApp.Core.Domain.Dtos.Warehouse;
using TD.WareHouse.DemoApp.Core.Domain.Models.Employees;
using TD.WareHouse.DemoApp.Core.Domain.Models.GoodIssues;
using TD.WareHouse.DemoApp.Core.Domain.Models.GoodsReceipts;
using TD.WareHouse.DemoApp.Core.Domain.Models.Items;
using TD.WareHouse.DemoApp.Core.Domain.Models.Locations;
using TD.WareHouse.DemoApp.Core.Domain.Models.Warehouses;

namespace TD.WareHouse.DemoApp.Core.Application.Mapping
{
    public class DtoToModelProfile : Profile
    {
        public DtoToModelProfile()
        {
            CreateMap<ItemDto, Item>();
            CreateMap<ItemLotDto, ItemLot>();
            CreateMap<WarehouseDto, Warehouse>();
            CreateMap<LocationDto, Location>();
            CreateMap<GoodsReceiptDto, GoodsReceipt>();
            CreateMap<GoodsIssueDto, GoodsIssue>();
            CreateMap<EmployeeDto, Employee>();
            CreateMap<DepartmentDto,Department>();
            //CreateMap<GoodsIssueDto, GoodsIssueByHand>()
            //    .ForMember(i => i.EmployeeName, o => o.MapFrom(dto => dto.Employee.EmployeeName));


            CreateMap<ItemLotDto, EntryForQuantityAlarmViewModel>()
                .ForMember(i => i.ItemId, o => o.MapFrom(dto => dto.Item.ItemId))
                .ForMember(i => i.ItemName, o => o.MapFrom(dto => dto.Item.ItemName))
                .ForMember(i => i.Unit, o => o.MapFrom(dto => dto.Item.Unit))
                .ForMember(i => i.MinimumStockLevel, o => o.MapFrom(dto => dto.Item.MinimumStockLevel))
                .ForMember(i => i.LocationId, o => o.MapFrom(dto => dto.Location.LocationId))
                .ForMember(i => i.ItemClassId, o => o.MapFrom(dto => dto.Item.ItemClassId));
            CreateMap<ItemLotDto, EntryForExpirationDateAlarmViewModel>()
                .ForMember(i => i.ItemId, o => o.MapFrom(dto => dto.Item.ItemId))
                .ForMember(i => i.ItemName, o => o.MapFrom(dto => dto.Item.ItemName))
                .ForMember(i => i.Unit, o => o.MapFrom(dto => dto.Item.Unit))
                .ForMember(i => i.MinimumStockLevel, o => o.MapFrom(dto => dto.Item.MinimumStockLevel))
                .ForMember(i => i.LocationId, o => o.MapFrom(dto => dto.Location.LocationId))
                .ForMember(i => i.ItemClassId, o => o.MapFrom(dto => dto.Item.ItemClassId));

            CreateMap<LotAdjustmentDto, FixLotAdjustmentViewModel>()
                .ForMember(i => i.ItemId, o => o.MapFrom(dto => dto.Item.ItemId))
                .ForMember(i => i.ItemName, o => o.MapFrom(dto => dto.Item.ItemName))
                .ForMember(i => i.Unit, o => o.MapFrom(dto => dto.Item.Unit))
                .ForMember(i => i.EmployeeName, o => o.MapFrom(dto => dto.Employee.EmployeeName));
            CreateMap<LotAdjustmentDto, ConfirmedLotAdjustmentViewModel>()
                .ForMember(i => i.ItemId, o => o.MapFrom(dto => dto.Item.ItemId))
                .ForMember(i => i.ItemName, o => o.MapFrom(dto => dto.Item.ItemName))
                .ForMember(i => i.Unit, o => o.MapFrom(dto => dto.Item.Unit))
                .ForMember(i => i.EmployeeName, o => o.MapFrom(dto => dto.Employee.EmployeeName));

            CreateMap<ItemLotDto, FixIsolationViewModel>()
                .ForMember(i => i.ItemId, o => o.MapFrom(dto => dto.Item.ItemId))
                .ForMember(i => i.ItemName, o => o.MapFrom(dto => dto.Item.ItemName))
                .ForMember(i => i.Unit, o => o.MapFrom(dto => dto.Item.Unit))
                .ForMember(i => i.LocationId, o => o.MapFrom(dto => dto.Location.LocationId));

            CreateMap<ItemLotDto, LocationEntryForShelfManagementViewModel>()
                .ForMember(i => i.ItemId, o => o.MapFrom(dto => dto.Item.ItemId))
                .ForMember(i => i.ItemName, o => o.MapFrom(dto => dto.Item.ItemName))
                .ForMember(i => i.Unit, o => o.MapFrom(dto => dto.Item.Unit));
            CreateMap<ItemLotDto, ItemEntryForShelfManagementViewModel>()
                .ForMember(i => i.Unit, o => o.MapFrom(dto => dto.Item.Unit))
                .ForMember(i => i.LocationId, o => o.MapFrom(dto => dto.Location.LocationId));

            CreateMap<InventoryLogEntryDto, StockCardEntryViewModel>()
                .ForMember(i => i.ItemId, o => o.MapFrom(dto => dto.Item.ItemId))
                .ForMember(i => i.ItemName, o => o.MapFrom(dto => dto.Item.ItemName))
                .ForMember(i => i.Unit, o => o.MapFrom(dto => dto.Item.Unit))
                .ForMember(i => i.LotId, o => o.MapFrom(dto => dto.ItemLotId))
                .ForMember(i => i.MinimumStockLevel, o => o.MapFrom(dto => dto.Item.MinimumStockLevel))
                .ForMember(i => i.ItemClassId, o => o.MapFrom(dto => dto.Item.ItemClassId));
            CreateMap<InventoryLogExtendedEntryDto, StockCardExtendedEntryViewModel>()
                .ForMember(i => i.ItemId, o => o.MapFrom(dto => dto.Item.ItemId))
                .ForMember(i => i.ItemName, o => o.MapFrom(dto => dto.Item.ItemName))
                .ForMember(i => i.Unit, o => o.MapFrom(dto => dto.Item.Unit))
                .ForMember(i => i.MinimumStockLevel, o => o.MapFrom(dto => dto.Item.MinimumStockLevel))
                .ForMember(i => i.ItemClassId, o => o.MapFrom(dto => dto.Item.ItemClassId))
                .ForMember(i => i.ShippedQuantity, o => o.MapFrom(dto => -dto.ShippedQuantity));

        }
    }
}
