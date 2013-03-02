using AutoMapper;
using Galleria.ImageProcessing;
using Galleria.Models;
using Galleria.Services.FileStorage;
using Galleria.ViewModels;
using Microsoft.Practices.ServiceLocation;
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
                    .ForMember(m => m.Url, v => v.MapFrom(s => (ServiceLocator.Current.GetInstance<IFileStorageService>().GetFileStorageUri(s, FileType.File))))
                    .ForMember(m => m.PreviewUrl, v => v.MapFrom(s => (ServiceLocator.Current.GetInstance<IFileStorageService>().GetFileStorageUri(s, FileType.Preview))));

            Mapper.CreateMap<ExtractedImageInformation, StoredImage>()
                    .ForMember(m => m.Id, v => v.MapFrom(s => Guid.NewGuid().ToString()));

            Mapper.CreateMap<StoredImage, EditImageDetailsViewModel>();
        }
    }
}