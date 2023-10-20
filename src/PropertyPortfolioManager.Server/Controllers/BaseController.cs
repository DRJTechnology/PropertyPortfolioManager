using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web;
using PropertyPortfolioManager.Models.Dto.Profile;
using PropertyPortfolioManager.WebAPI.Services.Interfaces;

namespace PropertyPortfolioManager.Server.Controllers
{
	[ApiController]
	public class BaseController : ControllerBase
	{
		private readonly IUserService userService;
		private UserDto currentUser;

		public BaseController(IUserService userService)
		{
			this.userService = userService;
			this.currentUser = new UserDto();
		}

		protected IUserService UserService { get { return this.userService; } }


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
				this.currentUser = await userService.GetCurrent(User);
			}
			return this.currentUser;
		}
	}
}
