using Application.Common.Mapping;
using AutoMapper;
using Domain.Aggregates.CatalogAggregate;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Common.Dtos
{
    public class ImageDto : IMapFrom<Image>
    {
        public int Id { get; set; }
        public string Url { get; set; }

        private void Mapping(Profile profile)
        {
            profile.CreateMap<Image, ImageDto>();
        }

    }
}
