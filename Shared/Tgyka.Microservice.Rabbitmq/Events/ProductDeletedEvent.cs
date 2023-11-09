using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tgyka.Microservice.Rabbitmq.Events
{
    public class ProductDeletedEvent
    {
        public ProductDeletedEvent(int ıd)
        {
            Id = ıd;
        }

        public int Id { get; set; }
    }
}
