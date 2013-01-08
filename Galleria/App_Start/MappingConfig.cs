using AutoMapper;
using Galleria.Models;
using Galleria.ViewModels;
using System.Configuration;
using System.Web;

namespace Galleria
{
    public static class MappingConfig
    {
        public static void SetupMappings()
        {
            Mapper.CreateMap<UploadInformation, ProcessedImageViewModel>()
                    .ForMember(m => m.Url, v => v.MapFrom(s => VirtualPathUtility.ToAbsolute(ConfigurationManager.AppSettings["MediaPath"] + s.Filename)))
                    .ForMember(m => m.ThumbnailUrl, v => v.MapFrom(s => VirtualPathUtility.ToAbsolute(ConfigurationManager.AppSettings["ThumbnailPath"] + s.Filename)));
        }
    }
}