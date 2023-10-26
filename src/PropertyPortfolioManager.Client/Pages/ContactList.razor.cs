using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using PropertyPortfolioManager.Client.Interfaces;
using PropertyPortfolioManager.Models.Model.General;

namespace PropertyPortfolioManager.Client.Pages
{
    [Authorize]
	public partial class ContactList
    {
		private IEnumerable<ContactBasicResponseModel>? contacts;

        [Inject]
        public IContactDataService contactDataService { get; set; }

        public bool ActiveOnly { get; set; } = true;

        protected override async Task OnInitializedAsync()
		{
			await PopulateContactListAsync();
		}

		public async Task IncludeInactive(bool includeInactive)
		{
            ActiveOnly = !includeInactive;
            await PopulateContactListAsync();
        }

		private async Task PopulateContactListAsync()
		{
			try
			{
                contacts = await this.contactDataService.GetAllAsync<ContactBasicResponseModel>(ActiveOnly);
            }
            catch (Exception ex)
			{
				throw;
			}
        }
    }
}
