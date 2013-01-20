using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace Galleria.Services.FileStorage.Implementations
{
    public class ChunkedLocalFileSystemStorageService : IChunkedFileStorageService
    {
        private HttpContextBase StoredHttpContext;
        private string BlockStorageUri;
        private string FileStorageUri;
        private string BlockStoragePath;
        private string FileStoragePath;


        public ChunkedLocalFileSystemStorageService(HttpContextBase httpContext, string blockStorageUri, string fileStorageUri)
        {
            this.StoredHttpContext = httpContext;
            BlockStoragePath = httpContext.Server.MapPath(blockStorageUri);
            FileStoragePath = httpContext.Server.MapPath(fileStorageUri);
            BlockStorageUri = VirtualPathUtility.ToAbsolute(blockStorageUri);
            FileStorageUri = VirtualPathUtility.ToAbsolute(fileStorageUri);
        }

        public async Task<IEnumerable<ISavedBlock>> SaveAsBlocksAsync(HttpRequestMessage httpRequest)
        {
            if (httpRequest.Content.IsMimeMultipartContent())
            {
                var streamProvider = new MultipartFormDataStreamProvider(BlockStoragePath);

                await httpRequest.Content.ReadAsMultipartAsync(streamProvider);
                var fileInfo = streamProvider.FileData.Select(i =>
                {
                    //spit the info given to us by the client
                    string[] clientoptions = i.Headers.ContentDisposition.Name.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    string clientFilenumString = Regex.Replace(clientoptions[0], "[^\\d]", "");
                    string clientBlocknumString = Regex.Replace(clientoptions[1], "[^\\d]", "");

                    var info = new FileInfo(i.LocalFileName);
                    return new SavedBlock(int.Parse(clientFilenumString), int.Parse(clientBlocknumString), info.Name, BlockStorageUri + "/" + info.Name, info.Length / 1024);
                });
                return fileInfo;
            }
            else
            {
                throw new ArgumentException("improper request format");
            }
        }

        public async Task<ISavedFile> AssembleFileFromBlocksAsync(IFileAssemblyRequest assemblyRequest)
        {
            string storageFileName = string.Format("{0}-{1}", Guid.NewGuid(), assemblyRequest.Filename);
            List<string> sourcefiles = new List<string>();

            //build up the blockstrings
            foreach (string block in assemblyRequest.Blocks)
            {
                sourcefiles.Add(BlockStoragePath + block);
            }

            using (Stream destStream = File.OpenWrite(FileStoragePath + storageFileName))
            {
                foreach (string srcFileName in sourcefiles)
                {
                    using (Stream srcStream = File.OpenRead(srcFileName))
                    {
                        await srcStream.CopyToAsync(destStream);
                    }
                }
            }

            //tidy up the old blocks
            foreach (string srcFileName in sourcefiles)
            {
                File.Delete(srcFileName);
            }

            return new SavedFile() { OriginalFileName = assemblyRequest.Filename, StorageFilename = storageFileName, StorageUri = FileStorageUri + storageFileName };
        }

        public async Task<IEnumerable<ISavedFile>> SaveFilesWithoutChunkingAsync(HttpRequestMessage httpRequest)
        {
            if (!httpRequest.Content.IsMimeMultipartContent())
            {
                throw new ArgumentException("improper request format");
            }

            var streamProvider = new MultipartFormDataStreamProvider(FileStoragePath);

            await httpRequest.Content.ReadAsMultipartAsync(streamProvider);
            var fileInfo = streamProvider.FileData.Select(i =>
            {
                var info = new FileInfo(i.LocalFileName);
                string storageFileName = string.Format("{0}-{1}", Guid.NewGuid(), info.Name);

                //figure out async move
                File.Move(FileStoragePath + info.Name, FileStoragePath + storageFileName);
                return new SavedFile() { OriginalFileName = info.Name, StorageFilename = storageFileName, StorageUri = FileStorageUri + storageFileName };
            });

            return fileInfo;
        }
    }
}
