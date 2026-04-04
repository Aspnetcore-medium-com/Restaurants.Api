using AutoMapper;
using Restaurants.Core.Dishes.Dtos;
using Restaurants.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Core.MappingProfiles
{
    public class DishesProfile : Profile
    {
        public DishesProfile() {
            CreateMap<Dish, DishResponseDto>();
        }
    }
}
