namespace TD.WareHouse.DemoApp.Core.Domain.Repositories;
public interface IRepository
{
    IUnitOfWork UnitOfWork { get; }
}
