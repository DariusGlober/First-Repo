using APIGenerator.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIGenerator.DTOs
{
    public class AutoMapperConfiguration : Profile
    {
        public AutoMapperConfiguration()
        {
            CreateMap<MemesImages, ImageDTO>()
                   .ReverseMap();
        }

        public static void Configure()
        {

            var config = new MapperConfiguration(cfg => {
                cfg.AddProfile<AutoMapperConfiguration>();
            });

            var mapper = config.CreateMapper();
        }
    }
}
