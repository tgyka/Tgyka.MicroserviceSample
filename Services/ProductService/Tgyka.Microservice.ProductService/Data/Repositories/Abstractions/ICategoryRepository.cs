using FoodDelivery.MssqlBase.Data.Repository;
using FoodDelivery.RestaurantService.Data.Entities;

namespace FoodDelivery.RestaurantService.Data.Repositories.Abstractions
{
    public interface ICategoryRepository: IBaseRepository<Category>
    {
    }
}
