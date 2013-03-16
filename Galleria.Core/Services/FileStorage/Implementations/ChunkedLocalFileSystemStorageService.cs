﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;

namespace Galleria.Core.Services.FileStorage.Implementations
{
    public class ChunkedLocalFileSystemStorageService : IChunkedFileStorageService
    {
        private string BlockStorageUri;
        private string FileStorageUri;
        private string PreviewStorageUri;
        private string BlockStoragePath;
        private string FileStoragePath;
        private string PreviewStoragePath;


        public ChunkedLocalFileSystemStorageService(string blockStorageUri, string fileStorageUri, string previewStorageUri)
        {
            //TODO Hosting environment difficult to mock, replace with custom wrapper
            BlockStoragePath = HostingEnvironment.MapPath(blockStorageUri);
            FileStoragePath = HostingEnvironment.MapPath(fileStorageUri);
            PreviewStoragePath = HostingEnvironment.MapPath(previewStorageUri);
            BlockStorageUri = VirtualPathUtility.ToAbsolute(blockStorageUri);
            FileStorageUri = VirtualPathUtility.ToAbsolute(fileStorageUri);
            PreviewStorageUri = VirtualPathUtility.ToAbsolute(previewStorageUri);
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

            return new SavedFile() { Name = assemblyRequest.Filename, Filename = storageFileName };
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

                //TODO figure out async move
                File.Move(FileStoragePath + info.Name, FileStoragePath + storageFileName);
                return new SavedFile() { Name = info.Name, Filename = storageFileName };
            });

            return fileInfo;
        }


        public string GetFileStorageUri(ISavedFile file, FileType fileType)
        {
            switch (fileType)
            {
                case FileType.File:
                    return FileStorageUri + file.Filename;
                case FileType.Preview:
                    return PreviewStorageUri + file.Filename;
            }

            return null;
        }
    }
}