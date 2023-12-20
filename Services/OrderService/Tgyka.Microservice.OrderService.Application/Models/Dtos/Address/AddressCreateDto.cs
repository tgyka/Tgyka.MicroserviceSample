using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tgyka.Microservice.OrderService.Application.Models.Dtos.Address
{
    public class AddressCreateDto
    {
        public string Street { get; set; }
        public string District { get; set; }
        public string Province { get; set; }
        public string ZipCode { get; set; }
        public string FullText { get; set; }
    }
}
