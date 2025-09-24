using System.Collections.Generic;
using Construction.DomainModel.Item; // Use the actual namespace

namespace Construction.Interface
{
    public interface IItemService
    {
        public string? ConnectionStrings { get; set; }
    }
}
