using System;
using AutoMapper;
using MovieMaster.Data.Dto;
using MovieMaster.Service.Model;

namespace MovieMaster.Service
{
    public sealed class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<MovieDto, Movie>();
            CreateMap<Movie, MovieDto>();
        }
    }
}