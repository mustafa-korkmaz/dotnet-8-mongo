using Application.Dtos;
using Application.Dtos.Order;
using Application.Dtos.Product;
using Application.Dtos.User;
using AutoMapper;
using Infrastructure.Services;
using Presentation.ViewModels;
using Presentation.ViewModels.Identity;
using Presentation.ViewModels.Order;
using Presentation.ViewModels.Product;

namespace Presentation
{
    internal class PresentationMappingProfile : Profile
    {
        public PresentationMappingProfile()
        {
            CreateMap<AddUserViewModel, UserDto>()
                .ConvertUsing((src, _) =>
                    new UserDto(src.Email!.GetNormalized(), src.Email!.GetNormalized(), src.NameSurname, false, null, DateTime.UtcNow));

            CreateMap<GetTokenViewModel, UserDto>()
                .ConvertUsing((src, _) =>
                    new UserDto(src.EmailOrUsername!.GetNormalized(), src.EmailOrUsername!.GetNormalized(), null, false, null, DateTime.UtcNow));

            CreateMap<UserDto, UserViewModel>();

            CreateMap<AddEditProductViewModel, ProductDto>()
                .ConvertUsing((src, _) =>
              new ProductDto(src.Sku!, src.Name!, src.UnitPrice!.Value, src.StockQuantity!.Value, DateTime.UtcNow));

            CreateMap<ProductDto, ProductViewModel>();

            CreateMap<ListViewModelRequest, ListDtoRequest>();
            CreateMap(typeof(ListDtoResponse<>), typeof(ListViewModelResponse<>));

            CreateMap<AddEditOrderViewModel, OrderDto>()
                .ConvertUsing((src, _) =>
                {
                    var items = new List<OrderItemDto>();
                
                    var order = new OrderDto(items,DateTime.UtcNow);

                    foreach (var item in src.Items!)
                    {
                       items.Add(new OrderItemDto(item.ProductId!, item.UnitPrice!.Value, item.Quantity!.Value));
                    }

                    return order;
                });

            CreateMap<AddEditOrderItemViewModel, OrderItemDto>();

            CreateMap<OrderDto, OrderViewModel>();

            CreateMap<OrderItemDto, OrderItemViewModel>();
        }
    }
}