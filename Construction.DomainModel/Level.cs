using System;

namespace Construction.DomainModel
{
    public class Level
    {
        public long ID_Level { get; set; }
        public string LevelCode { get; set; }
        public string LevelName { get; set; }
        public string LevelStatus { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public long? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
    }
}
