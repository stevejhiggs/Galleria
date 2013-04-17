using Galleria.Controllers;
using NUnit.Framework;
using System;

namespace Galleria.Tests.Unit.Controllers
{
    [TestFixture]
    public class ImageDetailsControllerTests : RavenControllerTests
    {
        [Test]
        public void Edit_With_Invalid_Id_Should_Return_404()
        {
            ExecuteAction(new ImageDetailsController(null), controller =>
            {
                ((ImageDetailsController)controller).Edit(new Guid().ToString());
            });
        }

    }
}
