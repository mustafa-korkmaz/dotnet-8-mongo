using Application.Dto;
using Application.Dto.Order;
using Application.Dto.Product;
using Application.Dto.User;
using AutoMapper;
using Domain.Aggregates;
using Domain.Aggregates.Order;
using Domain.Aggregates.Product;
using Domain.Aggregates.User;

namespace Application
{
    public class ApplicationMappingProfile : Profile
    {
        public ApplicationMappingProfile()
        {
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>()
                .ConvertUsing(src => new User(src.Id, src.Username, src.Email, src.NameSurname, src.IsEmailConfirmed, src.CreatedAt));

            CreateMap<Product, ProductDto>();
            CreateMap<ProductDto, Product>()
                .ConvertUsing(src => new Product(src.Id, src.Sku, src.Name, src.UnitPrice, src.StockQuantity, src.CreatedAt));

            CreateMap<OrderItem, OrderItemDto>();
            CreateMap<Order, OrderDto>();
            CreateMap<OrderDto, Order>()
                .ConvertUsing((src, _) =>
                {
                    var order = new Order(src.Id, src.UserId, src.CreatedAt);

                    foreach (var item in src.Items)
                    {
                        order.AddItem(item.ProductId, item.UnitPrice, item.Quantity, src.CreatedAt);
                    }

                    return order;
                });

            CreateMap<ListDtoRequest, ListDocumentRequest>();
            CreateMap(typeof(ListDocumentResponse<>), typeof(ListDtoResponse<>));
        }
    }
}