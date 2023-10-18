using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tgyka.Microservice.OrderService.Domain.Core;

namespace Tgyka.Microservice.OrderService.Domain.Aggregates.OrderAggregate
{
    public class Address : ValueObject
    {
        public string Street { get; set; }
        public string District { get; set; }
        public string Province { get; set; }
        public string ZipCode { get; set; }
        public string FullText { get; set; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Street;
            yield return District;
            yield return Province;
            yield return ZipCode;
            yield return FullText;
        }
    }
}
