﻿namespace PropertyPortfolioManager.Models.Model.General
{
    public class ContactTypeModel
    {
        public int Id { get; set; }

        public int PortfolioId { get; set; }

        public string Type { get; set; } = string.Empty;

        public bool Active { get; set; }
    }
}
