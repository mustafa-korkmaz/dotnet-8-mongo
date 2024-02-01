using Application.Dtos;
using Application.Dtos.Order;
using Application.Dtos.Product;
using Application.Dtos.User;
using AutoMapper;
using Infrastructure.Services;
using MongoDB.Bson;
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
                    opt.MapFrom(source => DateTime.UtcNow));

            CreateMap<GetTokenViewModel, UserDto>()
                .ForMember(dest => dest.Username, opt =>
                    opt.MapFrom(source => source.EmailOrUsername!.GetNormalized()))
                .ForMember(dest => dest.Email, opt =>
                    opt.MapFrom(source => source.EmailOrUsername!.GetNormalized()));

            CreateMap<UserDto, UserViewModel>();

            CreateMap<AddEditProductViewModel, ProductDto>()
                .ForMember(dest => dest.CreatedAt, opt =>
                    opt.MapFrom(source => DateTime.UtcNow));

            CreateMap<ProductDto, ProductViewModel>();

            CreateMap<ListViewModelRequest, ListDtoRequest>();
            CreateMap(typeof(ListDtoResponse<>), typeof(ListViewModelResponse<>));

            CreateMap<AddEditOrderViewModel, OrderDto>()
                .ForMember(dest => dest.CreatedAt, opt =>
                    opt.MapFrom(source => DateTime.UtcNow));

            CreateMap<AddEditOrderItemViewModel, OrderItemDto>();
            CreateMap<OrderDto, OrderViewModel>();
            CreateMap<OrderItemDto, OrderItemViewModel>();
        }
    }
}