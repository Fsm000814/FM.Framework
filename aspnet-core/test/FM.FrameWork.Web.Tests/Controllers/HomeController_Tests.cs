using System.Threading.Tasks;
using FM.FrameWork.Models.TokenAuth;
using FM.FrameWork.Web.Controllers;
using Shouldly;
using Xunit;

namespace FM.FrameWork.Web.Tests.Controllers
{
    public class HomeController_Tests: FrameWorkWebTestBase
    {
        [Fact]
        public async Task Index_Test()
        {
            await AuthenticateAsync(null, new AuthenticateModel
            {
                UserNameOrEmailAddress = "admin",
                Password = "123qwe"
            });

            //Act
            var response = await GetResponseAsStringAsync(
                GetUrl<HomeController>(nameof(HomeController.Index))
            );

            //Assert
            response.ShouldNotBeNullOrEmpty();
        }
    }
}