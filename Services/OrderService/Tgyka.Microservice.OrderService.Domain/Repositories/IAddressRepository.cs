using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tgyka.Microservice.MssqlBase.Data.Repository;
using Tgyka.Microservice.OrderService.Domain.Entities;

namespace Tgyka.Microservice.OrderService.Domain.Repositories
{
    public interface IAddressRepository: IBaseRepository<Address>
    {
    }
}
