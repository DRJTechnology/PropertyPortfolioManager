using PropertyPortfolioManager.Models.Dto.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PropertyPortfolioManager.WebAPI.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<UserDto> GetByObjectIdentifier(string userObjectIdentifier);
        Task<UserDto> GetByObjectIdentifier(Guid userObjectIdentifier);
        Task<int> Create(UserDto user);
    }
}
