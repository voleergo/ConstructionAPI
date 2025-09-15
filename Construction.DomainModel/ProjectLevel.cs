namespace Construction.DomainModel
{
    public class ProjectLevel
    {
        public long ID_ProjectLevel { get; set; }
        public long FK_Project { get; set; }
        public long FK_Level { get; set; }
    }
}
