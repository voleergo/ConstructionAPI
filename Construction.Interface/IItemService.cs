using System.Collections.Generic;
using Construction.DomainModel;
using Construction.DomainModel.Item; // Use the actual namespace

namespace Construction.Interface
{
    public interface IItemService
    {
        public string? ConnectionStrings { get; set; }
        HttpResponses UpdateProjectService(ProjectServiceModel service);
        HttpResponses DeleteProjectService(int idItem);
        List<ProjectServiceModel> GetProjectServices(ProjectServiceModel service);

        // Service Category------------------------------------------
        List<Item> GetServiceCategory(Item item);
        HttpResponses UpdateServiceCategory(Item service);

        List<SupplierModel> GetSuppliers(SupplierModel supplier);
        HttpResponses AddSupplier(AddSupplierModel model);
    }
}
