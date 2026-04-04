using AutoMapper;
using Restaurants.Core.Dtos.Restaurants.Commands.Restaurants.Update;
using Restaurants.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Core.MappingProfiles
{
    public class UpdateRestaurantProfile : Profile
    {
        public UpdateRestaurantProfile()
        {
            CreateMap<UpdateRestaurantCommand, Restaurant>();
        }
    }
}
