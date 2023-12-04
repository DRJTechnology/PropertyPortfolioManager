using Microsoft.AspNetCore.Components;
using PropertyPortfolioManager.Client.Interfaces;
using PropertyPortfolioManager.Models.Model.General;

namespace PropertyPortfolioManager.Client.Components
{
    public partial class ContactSelector
    {
        private IEnumerable<ContactBasicResponseModel>? contacts;
        private bool Initialising = true;
        private int selectedContactId = 0;

        [Inject]
        public IContactDataService contactDataService { get; set; }

        [Parameter]
        public bool ActiveOnly { get; set; } = true;

        [Parameter]
        public string ElementId { get; set; } = "contact";

        [Parameter]
        public string ElementClass { get; set; } = string.Empty;

        [Parameter]
        public string SelectPrompt { get; set; } = "Select";

        [Parameter]
        public int ContactId { get; set; } = 0;

        [Parameter]
        public EventCallback<int> ContactIdChanged { get; set; }

        protected override async Task OnInitializedAsync()
        {
            selectedContactId = ContactId;
            await PopulateContactListAsync();
            Initialising = false;
        }

        private async Task PopulateContactListAsync()
        {
            try
            {
                contacts = await this.contactDataService.GetAllAsync<ContactBasicResponseModel>(ActiveOnly);
                Initialising = false;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private int SelectedContactId
        {
            get
            {
                return selectedContactId;
            }
            set
            {
                if (selectedContactId != value)
                {
                    selectedContactId = value;
                    ContactIdChanged.InvokeAsync(selectedContactId);
                }
            }
        }
    }
}
