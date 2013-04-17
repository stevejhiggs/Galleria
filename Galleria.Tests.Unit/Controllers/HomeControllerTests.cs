using Galleria.Controllers;
using NUnit.Framework;

namespace Galleria.Tests.Unit.Controllers
{
    [TestFixture]
    public class HomeControllerTests : RavenControllerTests
    {
        [Test]
        public void Call_Index_Should_Not_Throw_Exception()
        {
            ExecuteAction<HomeController>(controller =>
            {
                controller.Index();
            });
        }

    }
}
