using Microsoft.AspNetCore.Mvc;
using PropertyPortfolioManager.Models.InternalObjects;
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
        [Route("GetAll")]
        public async Task<List<UnitBasicResponseModel>> GetAll()
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
        public async Task<ApiCreateResponse> Create(UnitEditModel unit)
        {
            try
            {
                var currentUser = this.UserService.GetCurrent(User);
                var newUnitId = await this.unitService.Create(currentUser.Id, unit);
                return new ApiCreateResponse()
                {
                    CreatedId = newUnitId,
                    Success = true,
                };
            }
            catch (Exception ex)
            {
                return new ApiCreateResponse()
                {
                    Success = false,
                    ErrorMessage = ex.Message,
                };
            }

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
