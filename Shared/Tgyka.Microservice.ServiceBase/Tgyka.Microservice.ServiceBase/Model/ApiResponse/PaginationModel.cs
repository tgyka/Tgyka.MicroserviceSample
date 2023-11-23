using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tgyka.Microservice.MssqlBase.Model.RepositoryDtos
{
    public class PaginationModel<T>
    {
        public PaginationModel(IEnumerable<T> dataList, int count, int page, int size)
        {
            DataList = dataList;
            Count = count;
            Page = page;
            Size = size;
        }

        public IEnumerable<T> DataList { get; set; }
        public int Count { get; set; }
        public int Page { get; set; }
        public int Size { get; set; }
    }
}
