using Microsoft.AspNetCore.Mvc;
using PropertyPortfolioManager.Models.Model.Property;
using PropertyPortfolioManager.WebAPI.Services.Interfaces;

namespace PropertyPortfolioManager.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UnitController : BaseController
    {
        private readonly IUnitService unitService;

        public UnitController(IUserService userService, IUnitService unitService)
            : base(userService)
        {
            this.unitService = unitService;
        }

        [HttpGet]
        [Route("GetByAll")]
        public async Task<List<UnitResponseModel>> GetAll()
        {
            return await this.unitService.GetAll();
        }

        [HttpGet]
        [Route("GetById/{unitId}")]
        public async Task<UnitResponseModel> GetById(int unitId)
        {
            return await this.unitService.GetById(unitId);
        }

        [HttpPost]
        [Route("Create")]
        public async Task<int> Create(UnitEditModel unit)
        {
            var currentUser = this.UserService.GetCurrent(User);
            return await this.unitService.Create(currentUser.Id, unit);
        }

        [HttpPost]
        [Route("Update")]
        public async Task<bool> Update(UnitEditModel unit)
        {
            var currentUser = this.UserService.GetCurrent(User);
            return await this.unitService.Update(currentUser.Id, unit);
        }

    }
}
