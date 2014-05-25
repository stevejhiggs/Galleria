using Galleria.Controllers.Api;
using Galleria.ViewModels;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;

namespace Galleria.Tests.Unit.Controllers.Api
{
    [TestFixture]
    public class ImageCollectionControllerTests : RavenControllerTests
    {
        [Test]
        public async void TestMethod1()
        {
            IEnumerable<ProcessedImageViewModel> results = null;

            await ExecuteApiAction<ImageCollectionController>(async controller =>
            {
                results = await controller.SearchByName();
            });
        }
    }
}
