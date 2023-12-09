﻿using Microsoft.AspNetCore.Mvc;
using PropertyPortfolioManager.Models.Model.General;
using PropertyPortfolioManager.Server.Services.Interfaces;

namespace PropertyPortfolioManager.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountTypeController : BaseController
    {
        private readonly IAccountTypeService accountTypeService;

        public AccountTypeController(IUserService userService, IAccountTypeService accountTypeService)
            : base(userService)
        {
            this.accountTypeService = accountTypeService;
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<List<EntityTypeBasicModel>> GetAll()
        {
            return await this.accountTypeService.GetAll();
        }
    }
}
