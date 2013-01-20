﻿using Galleria.Services.FileStorage;

namespace Galleria.ImageProcessing
{
    public class InformationExtractor
    {
        public string ImagePathBase { get; set;}
        public string ThumbnailPathBase { get; set; }
        public int ThumbnailMaxHeight { get; set; }

        public ExtractedImageInformation GetImageInformation(ISavedFile savedFileInformation)
        {
            ExtractedImageInformation info = new ExtractedImageInformation();
            info.FileName = savedFileInformation.StorageFilename;
            info = new TaglibExtractor().ExtractTags(ImagePathBase + info.FileName, info);
            if (string.IsNullOrWhiteSpace(info.Name))
            {
                info.Name = savedFileInformation.OriginalFileName;
            }

            //probably want to do initial rotation here

            ThumbnailGenerator thumbGen = new ThumbnailGenerator();
            thumbGen.GenerateThumbnail(ImagePathBase + info.FileName, ThumbnailPathBase + info.FileName, ThumbnailMaxHeight);
            info.ThumbnailPath = ThumbnailPathBase + info.FileName;
            return info;
        }
    }
}
