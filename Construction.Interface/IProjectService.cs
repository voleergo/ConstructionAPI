using Construction.DomainModel;
using System.Collections.Generic;
using Construction.DomainModel.Project; // Use the actual namespace

namespace Construction.Interface
{
    public interface IProjectService
    {
        public string? ConnectionStrings { get; set; }
        List<ProjectModel> GetProject(ProjectModel data);

        HttpResponses DeleteProjects(int id_project);

    }
}