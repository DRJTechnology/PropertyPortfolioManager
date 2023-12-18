using Microsoft.AspNetCore.Mvc;
using PropertyPortfolioManager.Models.Enums;
using PropertyPortfolioManager.Models.InternalObjects;
using PropertyPortfolioManager.Models.Model.Finance;
using PropertyPortfolioManager.Server.Services.Interfaces;

namespace PropertyPortfolioManager.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : BaseController
    {
        private readonly ILogger<AccountController> logger;
        private readonly IAccountService accountService;

        public AccountController(ILogger<AccountController> logger, IUserService userService, IAccountService accountService)
            : base(userService)
        {
            this.logger = logger;
            this.accountService = accountService;
        }

        [HttpGet]
        [Route("GetAll/{activeOnly}")]
        public async Task<IActionResult> GetAll(bool activeOnly)
        {
            try
            {
                var portfolioId = (await this.GetCurrentUser()).SelectedPortfolioId;
                if (portfolioId == null)
                {
                    return this.Ok(new List<AccountResponseModel>());
                }
                else
                {
                    var accounts =  await this.accountService.GetAll((int)portfolioId, activeOnly);
                    return this.Ok(accounts);
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"GetAll");
                return this.BadRequest();
            }
        }

        [HttpGet]
        [Route("GetByType/{accountType}/{activeOnly}")]
        public async Task<IActionResult> GetByType(AccountType accountType, bool activeOnly)
        {
            try
            {
                var portfolioId = (await this.GetCurrentUser()).SelectedPortfolioId;
                if (portfolioId == null)
                {
                    return this.Ok(new List<AccountResponseModel>());
                }
                else
                {
                    var accounts = await this.accountService.GetByType((int)portfolioId, accountType, activeOnly);
                    return this.Ok(accounts);
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"GeByType/{accountType}/{activeOnly}");
                return this.BadRequest();
            }
        }

        [HttpGet]
        [Route("GetById/{accountId}")]
        public async Task<IActionResult> GetById(int accountId)
        {
            try
            {
                var portfolioId = (await this.GetCurrentUser()).SelectedPortfolioId;
                if (portfolioId == null)
                {
                    return this.Ok(new AccountResponseModel());
                }
                else
                {
                    var account = await this.accountService.GetById(accountId, (int)portfolioId);
                    return this.Ok(account);
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"GetById/{accountId}");
                return this.BadRequest();
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
                logger.LogError(ex, $"Create");
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
                logger.LogError(ex, $"Update");
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
                logger.LogError(ex, $"Delete/{accountId}");
                return new PpmApiResponse()
                {
                    Success = false,
                    ErrorMessage = ex.Message,
                };
            }
        }
    }
}
