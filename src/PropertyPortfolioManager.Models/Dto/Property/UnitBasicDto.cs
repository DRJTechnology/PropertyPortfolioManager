﻿namespace PropertyPortfolioManager.Models.Dto.Property
{
    public class UnitBasicDto
    {
        public int Id { get; set; }

        public int PortfolioId { get; set; }

        public string Code { get; set; } = string.Empty;

        public string UnitType { get; set; } = string.Empty;

        public string StreetAddress { get; set; } = string.Empty;
    }
}
