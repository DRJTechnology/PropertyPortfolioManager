﻿namespace PropertyPortfolioManager.Models.Dto.Property
{
    public class UnitTypeDto : BaseDto
    {
        public int PortfolioId { get; set; }

        public string Type { get; set; } = string.Empty;

        public bool Active { get; set; }
    }
}
