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

      
    }
}
