namespace PropertyPortfolioManager.Models.Dto.Property
{
    public class UnitBasicDto
    {
        public int Id { get; set; }

        public int PortfolioId { get; set; }

        public string Code { get; set; } = string.Empty;

        public string UnitType { get; set; } = string.Empty;

        public string StreetAddress { get; set; } = string.Empty;

        public string TownCity { get; set; } = string.Empty;

        public bool Active { get; set; }

        public string MainPictureId { get; set; } = string.Empty;
    }
}
