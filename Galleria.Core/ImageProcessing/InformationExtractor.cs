using Galleria.Core.Services.FileStorage;
using System.Text.RegularExpressions;

namespace Galleria.Core.ImageProcessing
{
    public class InformationExtractor
    {
        public string ImagePathBase { get; set;}

        public void GetImageInformation(SavedFile savedFileInformation, byte[] fileContents, string fileStorageUri, ref ExtractedImageInformation info)
        {

            //TODO, I Should be a parallel async call
            
			info = new TaglibExtractor().ExtractTags(fileStorageUri, fileContents, info);
            if (string.IsNullOrWhiteSpace(info.Title))
            {
                info.Title = MakeTitleFromFileName(savedFileInformation.UploadedFileName);
            }
        }

        private string MakeTitleFromFileName(string fileName)
        {
            int dotIndex = fileName.LastIndexOf('.');
            if (dotIndex > -1)
            {
                fileName = fileName.Substring(0, dotIndex);
            }

            //todo: handle international characters
            fileName = Regex.Replace(fileName, "[^A-Za-z0-9 ]", " ");

            return fileName;
        }
    }
}
