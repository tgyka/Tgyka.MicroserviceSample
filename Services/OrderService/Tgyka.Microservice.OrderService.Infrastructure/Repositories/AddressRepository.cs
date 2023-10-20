using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tgyka.Microservice.MssqlBase.Data;
using Tgyka.Microservice.MssqlBase.Data.Repository;
using Tgyka.Microservice.MssqlBase.Data.UnitOfWork;
using Tgyka.Microservice.OrderService.Domain.Entities;
using Tgyka.Microservice.OrderService.Domain.Repositories;

namespace Tgyka.Microservice.OrderService.Infrastructure.Repositories
{
    public class AddressRepository : BaseRepository<Address> , IAddressRepository
    {
        public AddressRepository(MssqlDbContext dbContext, IUnitOfWork unitofWork, IMapper mapper) : base(dbContext, unitofWork, mapper)
        {
        }
    }
}
