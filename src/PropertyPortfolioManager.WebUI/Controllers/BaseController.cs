using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web;
using PropertyPortfolioManager.Models.Dto.Profile;
using PropertyPortfolioManager.Models.Model.Property;
using PropertyPortfolioManager.WebUI.Interfaces;

namespace PropertyPortfolioManager.WebUI.Controllers
{
    public abstract class BaseController : Controller
    {
        private readonly IUserService userService;
        private readonly IPortfolioService portfolioService;
        private UserDto currentUser;
        private PortfolioModel currentPortfolio;

        public BaseController(IUserService userService, IPortfolioService portfolioService)
        {
            this.userService = userService;
            this.portfolioService = portfolioService;
            this.currentUser = new UserDto();
            this.currentPortfolio = new PortfolioModel();
        }

        protected IUserService UserService { get { return this.userService; } }
        protected IPortfolioService PortfolioService { get { return this.portfolioService; } }

        protected Guid UserOi
        {
            get
            {
                var objectIdentifier = new Guid(User.GetObjectId());
                return objectIdentifier;
            }
        }

        protected async Task<UserDto> GetCurrentUser()
        {
            if (this.currentUser == null || this.currentUser.Id == 0)
            {
                this.currentUser = await userService.GetCurrent();
            }
            return this.currentUser;
        }

        protected async Task<PortfolioModel> GetCurrentPortfolio()
        {
            if (this.currentPortfolio == null || this.currentPortfolio.Id == 0)
            {
                this.currentPortfolio = await portfolioService.GetCurrent();
            }
            return this.currentPortfolio;
        }

    }
}
