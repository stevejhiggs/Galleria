using Galleria.ViewModels;
using Microsoft.AspNet.SignalR.Hubs;

namespace Galleria.Hubs
{
    public class PictureProcessHub : Hub
    {
        public void PictureProcessComplete(ProcessedImageViewModel imageInfo)
        {
            Clients.All.pictureprocessed(imageInfo);
        }
    }
}