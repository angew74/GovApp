using System.Collections.Generic;

namespace GovApp.Models
{

    public class PaginationModel
    {
        public string totalRows { get; set; }
        public string currentPage { get; set; }
        public string perPage { get; set; }
        public int[] pageOptions { get; set; }
        public string sortBy { get; set; }
        public bool sortDesc { get; set; }
        public string sortDirection { get; set; }
        public string filter { get; set; }
        public Options[] filterOn { get; set; }
        public InfoModale infoMal { get; set; }
        public Field[] fields { get; set; }
   

    public class InfoModale
    {
        public string id { get; set; }
        public string title { get; set; }
        public string content { get; set; }
    }
    public class Field
        {
            public string key { get; set; }
            public string label { get; set; }
            public string cssclass { get; set; }           
            public string formatter { get; set; }
            public bool sortable { get; set; }
            public bool sortByFormatted { get; set; }
            public bool filterByFormatted { get; set; }

            public string sortDirection { get; set; }
        }
        public class Options
        {
            public string item { get; set; }
            public string name { get; set; }
        }

    }
}