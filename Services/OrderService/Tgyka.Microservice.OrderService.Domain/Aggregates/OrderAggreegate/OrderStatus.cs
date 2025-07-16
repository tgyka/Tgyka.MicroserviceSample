using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tgyka.Microservice.OrderService.Domain.Aggregates.OrderAggreegate
{
    public enum OrderStatus
    {
        Created,
        Preparing,
        Shipping,
        Delivered,
        Canceled
    }
}
