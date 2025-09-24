using System.Collections.Generic;
using Construction.Interface;
using Construction.DataAccess;
using Construction.DomainModel.Item;
using Construction.DomainModel; // Use the actual namespace

namespace Construction.Service
{
    public class ItemService : IItemService
    {
        public string? ConnectionStrings { get; set; }
        public HttpResponses UpdateProjectService(ProjectServiceModel service)
        {

            ItemDataService dataService = new ItemDataService(ConnectionStrings);
            return dataService.UpdateProjectService(service);
        }

        public HttpResponses DeleteProjectService(int idItem)
        {
            ItemDataService dataService = new ItemDataService(ConnectionStrings);
            return dataService.DeleteProjectService(idItem);
        }
        public List<ProjectServiceModel> GetProjectServices(ProjectServiceModel service)
        {
            ItemDataService dataService = new ItemDataService(ConnectionStrings);
            return dataService.GetProjectServices(service);
        }

        // Service Category-------------------------------------

        public List<Item> GetServiceCategory(Item data)
        {
            ItemDataService dataService = new ItemDataService(ConnectionStrings);
            return dataService.GetServiceCategory(data);
        }

        public HttpResponses UpdateServiceCategory(Item service)
        {

            ItemDataService dataService = new ItemDataService(ConnectionStrings);
            return dataService.UpdateServiceCategory(service);
        }



    }
}
