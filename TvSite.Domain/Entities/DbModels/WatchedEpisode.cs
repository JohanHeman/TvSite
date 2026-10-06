using System;
using System.Collections.Generic;
using System.Text;

namespace TvSite.Domain.Entities.DbModels
{
    public class WatchedEpisode
    {
        public Guid Id { get; set; }
        public string EpisodeId { get; set; }
        public Guid UserId { get; set; }
        public DateTime DateTime { get; set; }

        public bool IsSoftDeleted { get; set; }
    }
}
