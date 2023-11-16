using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tgyka.Microservice.Base.Model.Token;
using Tgyka.Microservice.MssqlBase.Data;
using Tgyka.Microservice.MssqlBase.Data.Repository;
using Tgyka.Microservice.MssqlBase.Data.UnitOfWork;
using Tgyka.Microservice.OrderService.Domain.Entities;
using Tgyka.Microservice.OrderService.Domain.Repositories;

namespace Tgyka.Microservice.OrderService.Infrastructure.Repositories
{
    public class OrderRepository : BaseRepository<Order>, IOrderRepository
    {
        public OrderRepository(MssqlDbContext dbContext, IUnitOfWork unitofWork, IMapper mapper, TokenUser tokenUser) : base(dbContext, unitofWork, mapper, tokenUser)
        {
        }
    }
}
