using System.Collections.Generic;
using Construction.Interface;
using Construction.DataAccess;
using Construction.DomainModel.Project;
using Construction.DomainModel; // Use the actual namespace

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

        public HttpResponses DeleteProjects(int id_project)
        {
            ProjectDataService dataService = new ProjectDataService(ConnectionStrings);
            return dataService.DeleteProjects(id_project);
        }
    }
}