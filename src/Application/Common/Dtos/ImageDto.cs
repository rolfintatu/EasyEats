using Application.Common.Mapping;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using Entities = Domain.Entities;

namespace Application.Common.Dtos
{
    public class ImageDto : IMapFrom<Entities.Image>
    {
        public int Id { get; set; }
        public string Url { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Entities.Image, ImageDto>();
        }

    }
}
