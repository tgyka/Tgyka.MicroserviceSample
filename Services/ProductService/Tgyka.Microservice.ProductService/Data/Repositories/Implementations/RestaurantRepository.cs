using FoodDelivery.MssqlBase.Data;
using FoodDelivery.MssqlBase.Data.Repository;
using FoodDelivery.RestaurantService.Data.Entities;
using FoodDelivery.RestaurantService.Data.Repositories.Abstractions;

namespace FoodDelivery.RestaurantService.Data.Repositories.Implementations
{
    public class RestaurantRepository : BaseRepository<Restaurant>, IRestaurantRepository
    {
        public RestaurantRepository(MssqlDbContext dbContext) : base(dbContext)
        {
        }
    }
}
