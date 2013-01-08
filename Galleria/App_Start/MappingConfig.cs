using AutoMapper;
using Galleria.ImageProcessing;
using Galleria.Models;
using Galleria.ViewModels;
using System;
using System.Configuration;
using System.Web;

namespace Galleria
{
    public static class MappingConfig
    {
        public static void SetupMappings()
        {
            Mapper.CreateMap<StoredImage, ProcessedImageViewModel>()
                    .ForMember(m => m.Url, v => v.MapFrom(s => VirtualPathUtility.ToAbsolute(ConfigurationManager.AppSettings["MediaPath"] + s.Filename)))
                    .ForMember(m => m.ThumbnailUrl, v => v.MapFrom(s => VirtualPathUtility.ToAbsolute(ConfigurationManager.AppSettings["ThumbnailPath"] + s.Filename)));

            Mapper.CreateMap<ExtractedImageInformation, StoredImage>()
                    .ForMember(m => m.Id, v => v.MapFrom(s => Guid.NewGuid().ToString()));
        }
    }
}