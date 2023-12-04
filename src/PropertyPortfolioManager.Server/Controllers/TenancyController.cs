using Microsoft.AspNetCore.Mvc;
using Microsoft.Graph.Models;
using PropertyPortfolioManager.Models.InternalObjects;
using PropertyPortfolioManager.Models.Model.General;
using PropertyPortfolioManager.Models.Model.Property;
using PropertyPortfolioManager.Server.Services.Interfaces;

namespace PropertyPortfolioManager.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TenancyController : BaseController
    {
        private readonly ITenancyService tenancyService;
        private readonly IContactService contactService;

        public TenancyController(IUserService userService, ITenancyService tenancyService, IContactService contactService)
            : base(userService)
        {
            this.tenancyService = tenancyService;
            this.contactService = contactService;
        }


        [HttpGet]
        [Route("GetAll")]
        public async Task<List<TenancyResponseModel>> GetAll()
        {
            return await this.GetAll(true);
        }

        [HttpGet]
        [Route("GetAll/{activeOnly}")]
        public async Task<List<TenancyResponseModel>> GetAll(bool activeOnly)
        {
            var portfolioId = (await this.GetCurrentUser()).SelectedPortfolioId;
            if (portfolioId == null)
            {
                return new List<TenancyResponseModel>();
            }
            else
            {
                return await this.tenancyService.GetAll((int)portfolioId, activeOnly);
            }
        }

        [HttpGet]
        [Route("GetById/{tenancyId}")]
        public async Task<TenancyResponseModel> GetById(int tenancyId)
        {
            var portfolioId = (await this.GetCurrentUser()).SelectedPortfolioId;
            if (portfolioId == null)
            {
                return new TenancyResponseModel();
            }
            else
            {
                return await this.tenancyService.GetById(tenancyId, (int)portfolioId);
            }
        }

        [HttpPost]
        [Route("Create")]
        public async Task<PpmApiResponse> Create(TenancyEditModel tenancy)
        {
            try
            {
                var newTenancyId = 0;
                var portfolioId = (await this.GetCurrentUser()).SelectedPortfolioId;
                if (portfolioId == null)
                {
                    return new PpmApiResponse()
                    {
                        Success = false,
                        ErrorMessage = "Tenancy_Create: User has no Selected Portfolio Id set."
                    };
                }
                else
                {
                    newTenancyId = await this.tenancyService.Create((await this.GetCurrentUser()).Id, (int)portfolioId, tenancy);
                }
                return new PpmApiResponse()
                {
                    CreatedId = newTenancyId,
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
        public async Task<PpmApiResponse> Update(TenancyEditModel tenancy)
        {
            try
            {
                var portfolioId = (await this.GetCurrentUser()).SelectedPortfolioId;
                if (portfolioId == null)
                {
                    return new PpmApiResponse()
                    {
                        Success = false,
                        ErrorMessage = "Tenancy_Update: User has no Selected Portfolio Id set."
                    };
                }
                else
                {
                    if (await this.tenancyService.Update((await this.GetCurrentUser()).Id, (int)portfolioId, tenancy))
                    {
                        return new PpmApiResponse()
                        {
                            CreatedId = tenancy.Id,
                            Success = true,
                        };
                    }
                    else
                    {
                        return new PpmApiResponse()
                        {
                            Success = false,
                            ErrorMessage = $"TenancyController: Failed to update tenancyId {tenancy.Id}"
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
        [Route("Delete/{tenancyId}")]
        public async Task<PpmApiResponse> Delete(int tenancyId)
        {
            try
            {
                var portfolioId = (await this.GetCurrentUser()).SelectedPortfolioId;
                if (portfolioId == null)
                {
                    return new PpmApiResponse()
                    {
                        Success = false,
                        ErrorMessage = "TenancyType_Delete: User has no Selected Portfolio Id set."
                    };
                }
                else
                {
                    if (await this.tenancyService.Delete((await this.GetCurrentUser()).Id, (int)portfolioId, tenancyId))
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
                            ErrorMessage = $"TenancyTypeController: Failed to delete tenancyTypeId {tenancyId}"
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

        [HttpPost]
        [Route("AddContact")]
        public async Task<ContactResponseModel> AddContact(TenancyContactModel tenancyContact)
        {
            try
            {
                var newTenancyContactId = 0;
                var portfolioId = (await this.GetCurrentUser()).SelectedPortfolioId;
                if (portfolioId == null)
                {
                    throw new Exception("Tenancy_AddContact: User has no Selected Portfolio Id set.");
                }
                else
                {
                    newTenancyContactId = await this.tenancyService.AddContact((await this.GetCurrentUser()).Id, (int)portfolioId, tenancyContact);
                }

                return await this.contactService.GetById(tenancyContact.ContactId, (int)portfolioId);
            }
            catch (Exception ex)
            {
                throw new Exception("Tenancy_AddContact: Internal error.");
            }
        }

        [HttpPost]
        [Route("RemoveContact")]
        public async Task<PpmApiResponse> RemoveContact(TenancyContactModel tenancyContact)
        {
            try
            {
                var newTenancyContactId = 0;
                var portfolioId = (await this.GetCurrentUser()).SelectedPortfolioId;
                if (portfolioId == null)
                {
                    return new PpmApiResponse()
                    {
                        Success = false,
                        ErrorMessage = "Tenancy_AddContact: User has no Selected Portfolio Id set."
                    };
                }
                else
                {
                    if (await this.tenancyService.RemoveContact((await this.GetCurrentUser()).Id, (int)portfolioId, tenancyContact))
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
                            ErrorMessage = $"TenancyController: Failed to remove contact ({tenancyContact.ContactId}) from tenancy ({tenancyContact.TenancyId})"
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
