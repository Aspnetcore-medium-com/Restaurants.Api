using AutoMapper;
using Restaurants.Core.Dtos.Restaurants;
using Restaurants.Core.Dtos.Restaurants.Commands.Restaurants.Create;
using Restaurants.Core.Dtos.Restaurants.Commands.Restaurants.Update;
using Restaurants.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Core.MappingProfiles
{
    public class RestaurantProfile : Profile
    {
        public RestaurantProfile() {

            CreateMap<CreateRestaurantsCommand, Restaurant>()
                .ForMember(dest => dest.Address, opt => opt.MapFrom( src => new Address{
                     City = src.City,
                     Street = src.Street,
                     PostalCode = src.PostalCode
                }));

            CreateMap<Restaurant, RestaurantResponseDto>()
                .ForMember(d => d.City, opt => opt.MapFrom(src => src.Address == null ? null : src.Address.City))
                .ForMember(d => d.Street, opt => opt.MapFrom(src => src.Address == null ? null : src.Address.Street))
                .ForMember(d => d.PostalCode, opt => opt.MapFrom(src => src.Address == null ? null : src.Address.PostalCode))
                .ForMember(d => d.Dishes, opt => opt.MapFrom(src => src.Dishes));

            CreateMap<UpdateRestaurantCommand, Restaurant>();
        }
    }
}
