using Galleria.Hubs;
using Galleria.ViewModels;
using Microsoft.AspNet.SignalR;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Http;

namespace Galleria.Controllers
{
    public class FileAssemblyController : ApiController
    {
        private const string blockpath = "~/uploads/blocks/";
        private const string imagepath = "~/uploads/";

        // GET api/fileassembly
        public void Post(AssembleRequest item)
        {
            string mappedBlockPath = HttpContext.Current.Server.MapPath(blockpath);
            string mappedFilePath = HttpContext.Current.Server.MapPath(imagepath);
            string destinationFile = mappedFilePath + item.filename;

            List<string> sourcefiles = new List<string>();

            //build up the blockstrings
            foreach(string block in item.blocks)
            {
                sourcefiles.Add(mappedBlockPath + block);
            }

            using (Stream destStream = File.OpenWrite(destinationFile))
            {
                foreach (string srcFileName in sourcefiles)
                {
                    using (Stream srcStream = File.OpenRead(srcFileName))
                    {
                        srcStream.CopyTo(destStream);
                    }
                }
            }

            //tidy up the old blocks
            foreach (string srcFileName in sourcefiles)
            {
                File.Delete(srcFileName);
            }

            //signal the hub that we are done
            var context = GlobalHost.ConnectionManager.GetHubContext<PictureProcessHub>();
            var model = new ImageProcessingCompleteViewModel();
            model.Url = VirtualPathUtility.ToAbsolute(imagepath + item.filename);
            context.Clients.All.pictureprocessed(model);
        }
    }

    public class AssembleRequest
    {
        public string filename { get; set; }
        public string[] blocks { get; set; }
    }
}
