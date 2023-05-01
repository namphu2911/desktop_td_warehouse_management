﻿namespace TD.WareHouse.DemoApp.Core.Domain.Services;
public interface IDatabaseSynchronizationService
{
    Task SynchronizeItemsData();
    Task SynchronizeWarehousesData();
    Task SynchronizeItemLotsData();
    Task SynchronizeLocationsData();
    Task SynchronizeGoodReceiptsData();
    Task SynchronizeGoodIssuesData();

}
