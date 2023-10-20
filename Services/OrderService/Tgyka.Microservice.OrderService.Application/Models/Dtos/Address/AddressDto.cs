using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tgyka.Microservice.MssqlBase.Model.Dtos;

namespace Tgyka.Microservice.OrderService.Application.Models.Dtos.Address
{
    public class AddressDto: GetDto
    {
        public string Street { get; set; }
        public string District { get; set; }
        public string Province { get; set; }
        public string ZipCode { get; set; }
        public string FullText { get; set; }
        public int OrderId { get; set; }
    }
}
