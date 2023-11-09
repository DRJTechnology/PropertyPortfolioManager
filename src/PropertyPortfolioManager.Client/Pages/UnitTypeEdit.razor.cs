﻿using Microsoft.AspNetCore.Components;
using PropertyPortfolioManager.Client.Interfaces;
using PropertyPortfolioManager.Models.Model.Property;


namespace PropertyPortfolioManager.Client.Pages
{
    public partial class UnitTypeEdit
    {
        [Inject]
        public IUnitTypeDataService unitTypeDataService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Parameter]
        public string? UnitTypeId { get; set; }

        private UnitTypeModel UnitType { get; set; } = new UnitTypeModel();

        private bool Saved;
        private string Message = string.Empty;
        private string StatusClass = string.Empty;

        protected async override Task OnInitializedAsync()
        {
            Saved = false;

            int.TryParse(UnitTypeId, out var unitTypeId);

            if (unitTypeId == 0) //new unit type is being created
            {
                UnitType = new UnitTypeModel() { Active = true };
            }
            else
            {
                UnitType = await this.unitTypeDataService.GetByIdAsync<UnitTypeModel>(unitTypeId);
            }
        }

        protected async Task HandleValidSubmit()
        {
            Saved = false;

            if (UnitType.Id == 0) //new
            {
                var addedUnitType = await this.unitTypeDataService.Create<UnitTypeModel>(UnitType);
                if (addedUnitType != null)
                {
                    StatusClass = "alert-success";
                    Message = "New unit type added successfully.";
                    Saved = true;
                }
                else
                {
                    StatusClass = "alert-danger";
                    Message = "Something went wrong adding the new unit type. Please try again.";
                    Saved = false;
                }
            }
            else
            {
                await this.unitTypeDataService.Update<UnitTypeModel>(UnitType);
                StatusClass = "alert-success";
                Message = "Unit type updated successfully.";
                Saved = true;
            }
        }

        protected void HandleInvalidSubmit()
        {
            StatusClass = "alert-danger";
            Message = "There are some validation errors. Please try again.";
        }

        protected async Task DeleteUnitType()
        {
            try
            {
                await this.unitTypeDataService.DeleteAsync(UnitType.Id);
            }
            catch (Exception ex)
            {
                throw;
            }

            StatusClass = "alert-success";
            Message = "Deleted successfully";

            Saved = true;
        }

        protected void NavigateToList()
        {
            NavigationManager.NavigateTo("/unittype");
        }

    }
}
