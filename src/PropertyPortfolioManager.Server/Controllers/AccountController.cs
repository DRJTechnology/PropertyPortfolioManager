using Microsoft.AspNetCore.Mvc;
using PropertyPortfolioManager.Models.InternalObjects;
using PropertyPortfolioManager.Models.Model.Finance;
using PropertyPortfolioManager.Models.Model.General;
using PropertyPortfolioManager.Server.Services.Interfaces;

namespace PropertyPortfolioManager.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : BaseController
    {
        private readonly IAccountService accountService;

        public AccountController(IUserService userService, IAccountService accountService)
            : base(userService)
        {
            this.accountService = accountService;
        }

        [HttpGet]
        [Route("GetAll/{activeOnly}")]
        public async Task<List<AccountResponseModel>> GetAll(bool activeOnly)
        {
            var portfolioId = (await this.GetCurrentUser()).SelectedPortfolioId;
            if (portfolioId == null)
            {
                return new List<AccountResponseModel>();
            }
            else
            {
                return await this.accountService.GetAll((int)portfolioId, activeOnly);
            }
        }

        [HttpGet]
        [Route("GetById/{accountId}")]
        public async Task<AccountResponseModel> GetById(int accountId)
        {
            var portfolioId = (await this.GetCurrentUser()).SelectedPortfolioId;
            if (portfolioId == null)
            {
                return new AccountResponseModel();
            }
            else
            {
                return await this.accountService.GetById(accountId, (int)portfolioId);
            }
        }

        [HttpPost]
        [Route("Create")]
        public async Task<PpmApiResponse> Create(AccountEditModel account)
        {
            try
            {
                var newAccountId = 0;
                var portfolioId = (await this.GetCurrentUser()).SelectedPortfolioId;
                if (portfolioId == null)
                {
                    return new PpmApiResponse()
                    {
                        Success = false,
                        ErrorMessage = "Account_Create: User has no Selected Portfolio Id set."
                    };
                }
                else
                {
                    newAccountId = await this.accountService.Create((await this.GetCurrentUser()).Id, (int)portfolioId, account);
                }
                return new PpmApiResponse()
                {
                    CreatedId = newAccountId,
                    Success = true,
                };
            }
            catch (Exception ex)
            {
                return new PpmApiResponse()
                {
                    Success = false,
                    ErrorMessage = ex.Message,
                };
            }
        }

        [HttpPost]
        [Route("Update")]
        public async Task<PpmApiResponse> Update(AccountEditModel account)
        {
            try
            {
                var portfolioId = (await this.GetCurrentUser()).SelectedPortfolioId;
                if (portfolioId == null)
                {
                    return new PpmApiResponse()
                    {
                        Success = false,
                        ErrorMessage = "Account_Update: User has no Selected Portfolio Id set."
                    };
                }
                else
                {
                    if (await this.accountService.Update((await this.GetCurrentUser()).Id, (int)portfolioId, account))
                    {
                        return new PpmApiResponse()
                        {
                            CreatedId = account.Id,
                            Success = true,
                        };
                    }
                    else
                    {
                        return new PpmApiResponse()
                        {
                            Success = false,
                            ErrorMessage = $"AccountController: Failed to update accountId {account.Id}"
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                return new PpmApiResponse()
                {
                    Success = false,
                    ErrorMessage = ex.Message,
                };
            }
        }

        [HttpDelete]
        [Route("Delete/{accountId}")]
        public async Task<PpmApiResponse> Delete(int accountId)
        {
            try
            {
                var portfolioId = (await this.GetCurrentUser()).SelectedPortfolioId;
                if (portfolioId == null)
                {
                    return new PpmApiResponse()
                    {
                        Success = false,
                        ErrorMessage = "Account_Delete: User has no Selected Portfolio Id set."
                    };
                }
                else
                {
                    if (await this.accountService.Delete((await this.GetCurrentUser()).Id, (int)portfolioId, accountId))
                    {
                        return new PpmApiResponse()
                        {
                            Success = true,
                        };
                    }
                    else
                    {
                        return new PpmApiResponse()
                        {
                            Success = false,
                            ErrorMessage = $"AccountController: Failed to delete accountId {accountId}"
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                return new PpmApiResponse()
                {
                    Success = false,
                    ErrorMessage = ex.Message,
                };
            }
        }

    }
}
