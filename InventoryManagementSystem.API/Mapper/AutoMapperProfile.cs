using AutoMapper;
using InventoryManagementSystem.API.DTO;
using InventoryManagementSystem.Data.Entities;

namespace InventoryManagementSystem.API.Mapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Product, ProductDTO>();
            CreateMap<ProductDTO, Product>();
        }
    }
}
