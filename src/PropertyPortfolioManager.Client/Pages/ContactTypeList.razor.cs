using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using PropertyPortfolioManager.Models.Model.General;
using System.Net.Http.Json;

namespace PropertyPortfolioManager.Client.Pages
{
    [Authorize]
	public partial class ContactTypeList
    {
		private ContactTypeModel[]? contactypes;

		[Inject]
        public HttpClient Http { get; set; }

		public bool ActiveOnly { get; set; } = true;

        protected override async Task OnInitializedAsync()
		{
			await PopulateContactTypeListAsync();
		}

		public async Task IncludeInactive(bool includeInactive)
		{
            ActiveOnly = !includeInactive;
            await PopulateContactTypeListAsync();
        }

		private async Task PopulateContactTypeListAsync()
		{
			try
			{
                contactypes = await Http.GetFromJsonAsync<ContactTypeModel[]>($"api/ContactType/GetAll/{ActiveOnly}");
            }
            catch (Exception ex)
			{
				throw;
			}
        }
    }
}
