using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GovApp.Models
{
    public class SortingModel
    {
        public int currentPage { get; set; }
        public string filter { get; set; }
        public int perPage { get; set; }
        public string sortBy { get; set; }
        public bool sortDesc { get; set; }
    }
}
