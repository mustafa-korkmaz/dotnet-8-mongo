using Application.Dto;
using Application.Dto.Order;
using Application.Dto.Product;
using Application.Dto.User;
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
                .ForMember(dest => dest.Username, opt =>
                    opt.MapFrom(source => source.Email!.GetNormalized()))
                .ForMember(dest => dest.Email, opt =>
                    opt.MapFrom(source => source.Email!.GetNormalized()))
                .ForMember(dest => dest.CreatedAt, opt =>
                    opt.MapFrom(source => DateTimeOffset.UtcNow));

            CreateMap<GetTokenViewModel, UserDto>()
                .ForMember(dest => dest.Username, opt =>
                    opt.MapFrom(source => source.EmailOrUsername!.GetNormalized()))
                .ForMember(dest => dest.Email, opt =>
                    opt.MapFrom(source => source.EmailOrUsername!.GetNormalized()));

            CreateMap<UserDto, UserViewModel>();

            CreateMap<AddEditProductViewModel, ProductDto>()
                .ForMember(dest => dest.CreatedAt, opt =>
                    opt.MapFrom(source => DateTimeOffset.UtcNow));

            CreateMap<ProductDto, ProductViewModel>();

            CreateMap<ListViewModelRequest, ListDtoRequest>();
            CreateMap(typeof(ListDtoResponse<>), typeof(ListViewModelResponse<>));

            CreateMap<AddEditOrderViewModel, OrderDto>()
                .ForMember(dest => dest.CreatedAt, opt =>
                    opt.MapFrom(source => DateTimeOffset.UtcNow));

            CreateMap<AddEditOrderItemViewModel, OrderItemDto>();
            CreateMap<OrderDto, OrderViewModel>()
                .ForMember(dest => dest.OrderItems, opt =>
                    opt.MapFrom(source => source.Items));
            CreateMap<OrderItemDto, OrderItemViewModel>();
        }
    }
}