using PropertyPortfolioManager.Models.Dto.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PropertyPortfolioManager.Server.Services.Interfaces
{
    public interface IUserService
    {
        Task<UserDto> GetByObjectIdentifier(Guid userObjectIdentifier);
        Task<UserDto> GetCurrent(ClaimsPrincipal user);
    }
}
