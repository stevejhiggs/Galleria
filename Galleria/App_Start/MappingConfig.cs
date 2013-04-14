﻿using AutoMapper;
using Galleria.Core.ImageProcessing;
using Galleria.Core.Models;
using Galleria.Core.Services.FileStorage;
using Galleria.ViewModels;
using Microsoft.Practices.ServiceLocation;
using System;

namespace Galleria
{
    public static class MappingConfig
    {
        public static void SetupMappings()
        {
            Mapper.CreateMap<StoredImage, ProcessedImageViewModel>()
                    .ForMember(m => m.Url, v => v.MapFrom(s => (ServiceLocator.Current.GetInstance<IFileStorageService>().GetFileStorageUri(s.FileName, FileType.File))))
                    .ForMember(m => m.PreviewUrl, v => v.MapFrom(s => (ServiceLocator.Current.GetInstance<IFileStorageService>().GetFileStorageUri(s.PreviewFileName, FileType.Preview))));

            Mapper.CreateMap<ExtractedImageInformation, StoredImage>()
                    .ForMember(m => m.Id, v => v.MapFrom(s => Guid.NewGuid().ToString()));

            Mapper.CreateMap<StoredImage, EditImageDetailsViewModel>();
        }
    }
}