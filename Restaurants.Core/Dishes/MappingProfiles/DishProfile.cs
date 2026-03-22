using AutoMapper;
using Restaurants.Core.Dishes.Commands.Create;
using Restaurants.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Core.Dishes.MappingProfiles
{
    public class DishProfile : Profile
    {
        public DishProfile() {
            CreateMap<CreateDishCommand, Dish>()
                .ForMember(dest => dest.Restaurant, opt => opt.Ignore());
        }
    }
}
