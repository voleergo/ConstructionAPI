using System.Collections.Generic;
using Construction.Interface;
using Construction.DataAccess;
using Construction.DomainModel.Project; // Use the actual namespace

namespace Construction.Service
{
    public class ProjectService : IProjectService
    {
        public string? ConnectionStrings { get; set; }

        public List<ProjectModel> GetProject(ProjectModel data)
        {
            ProjectDataService dataService = new ProjectDataService(ConnectionStrings);
            return dataService.GetProject(data);
        }
    }
}