using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Core.Common
{
    public class PagedResult<T>
    {
        public PagedResult(List<T> items, int totalCount,int pageSize,int pageNumber) { 
            Items = items;
            TotalItemsResults = totalCount;
            TotalPages = (int)  Math.Ceiling( totalCount / (double) pageSize);
            ItemsFrom = pageSize * (pageNumber - 1) + 1 ;
            ItemsTo = ItemsFrom + pageSize - 1;
        }
        public List<T> Items { get; set; }
        public int TotalItemsResults { get; set; }
        public int TotalPages {  get; set; }
        public int ItemsFrom {  get; set; }
        public int ItemsTo { get; set; } 

    }
}
