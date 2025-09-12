using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medicare.Repository.Utility
{
    public class PagingResult<T>
    {
        public int Total { get; set; }
        public IEnumerable<T> Records { get; set; } 
    }
}
