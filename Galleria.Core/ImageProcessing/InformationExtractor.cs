using Galleria.Core.FileStorage;
using System.Text.RegularExpressions;

namespace Galleria.Core.ImageProcessing
{
    public class InformationExtractor
    {
		public ExtractedImageInformation GetImageInformation(SavedFile savedFileInformation, byte[] fileContents, ExtractedImageInformation info)
        {

            //TODO, I Should be a parallel async call

			info = new TaglibExtractor().ExtractTags(savedFileInformation.StorageFileName, fileContents, info);
            if (string.IsNullOrWhiteSpace(info.Title))
            {
                info.Title = MakeTitleFromFileName(savedFileInformation.UploadedFileName);
            }

			return info;
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
