using System;
using System.Collections.Generic;
using System.Text;

namespace TvSite.Domain.Entities
{
    // API FILTER ENTITY
    public class Media
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string[] Directors { get; set; }
        public string[] Actors { get; set; }
        public DateOnly AirDate { get; set; }
        public string MediaImage { get; set; }
    }
}
