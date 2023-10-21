﻿using DRJTechnology.Cache;
using Microsoft.Identity.Web;
using PropertyPortfolioManager.Models.CacheKeys;
using PropertyPortfolioManager.Models.Dto.Profile;
using PropertyPortfolioManager.WebAPI.Repositories.Interfaces;
using PropertyPortfolioManager.WebAPI.Services.Interfaces;
using System.Security.Claims;


namespace PropertyPortfolioManager.WebAPI.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;
        private readonly ICacheService cacheService;

        public UserService(IUserRepository userRepository, ICacheService cacheService)
        {
            this.userRepository = userRepository;
            this.cacheService = cacheService;
        }

        public async Task<UserDto> GetByObjectIdentifier(Guid userObjectIdentifier)
        {
            var user = await this.userRepository.GetByObjectIdentifier(userObjectIdentifier);
            return user;
        }

        public async Task<UserDto> GetCurrent(ClaimsPrincipal user)
        {
            try
            {
                var cacheKey = $"{CacheKeys.KeyUserPrefix}{user.GetObjectId()}";
                var currentUser = await this.cacheService.GetAsync<UserDto>(cacheKey);

                if (currentUser != null)
                {
                    return currentUser;
                }

                var userObjectIdentifier = new Guid(user.GetObjectId()!);
                var userDto = await this.userRepository.GetByObjectIdentifier(userObjectIdentifier);

                if (userDto == null)
                {
                    userDto = new UserDto()
                    {
                        ObjectIdentifier = userObjectIdentifier,
                        Name = user.FindFirstValue("name") ?? "No name",
                    };
                    userDto.Id = await this.userRepository.Create(userDto);
                }

                await this.cacheService.SetAsync(cacheKey, userDto);

                return userDto;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
