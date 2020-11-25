using System;
using BaseRepositories;

namespace WebApiService.Repository
{
    public class ProductDbModel : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}