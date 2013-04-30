using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using Galleria.Controllers;
using Galleria.Tests.Unit.Controllers;
using System.Collections;
using Galleria.ViewModels;
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
                results = await controller.Get();
            });
        }
    }
}
