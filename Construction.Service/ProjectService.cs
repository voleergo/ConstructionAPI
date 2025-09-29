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

        

        public HttpResponses UpdateProject(ProjectModel jsonModel)
        {
            var project = new ProjectModel
            {
                json = jsonModel.json
            };
            ProjectDataService dataService = new ProjectDataService(ConnectionStrings);
            return dataService.UpdateProject(project);
        }
        public HttpResponses DeleteProjects(int id_project)
        {
            ProjectDataService dataService = new ProjectDataService(ConnectionStrings);
            return dataService.DeleteProjects(id_project);
        }

        public List<ProjectUserModel> GetProjectUsers(int projectId)
        {
            ProjectDataService dataService = new ProjectDataService(ConnectionStrings);
            return dataService.GetProjectUsers(projectId);
        }

    }
}