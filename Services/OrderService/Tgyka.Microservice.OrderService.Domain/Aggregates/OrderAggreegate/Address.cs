using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tgyka.Microservice.DDDBase;

namespace Tgyka.Microservice.OrderService.Domain.Aggregates.OrderAggreegate
{
    public class Address : ValueObject
    {
        public Address(string street, string district, string province, string zipCode, string fullText)
        {
            Street = street;
            District = district;
            Province = province;
            ZipCode = zipCode;
            FullText = fullText;
        }

        public string Street { get; private set; }
        public string District { get; private set; }
        public string Province { get; private set; }
        public string ZipCode { get; private set; }
        public string FullText { get; private set; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Street;
            yield return District;
            yield return Province;
            yield return ZipCode;
        }
    }
}
