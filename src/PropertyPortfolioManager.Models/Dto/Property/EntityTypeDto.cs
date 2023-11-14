namespace PropertyPortfolioManager.Models.Dto.Property
{
    public class EntityTypeDto : BaseDto
    {
        public int PortfolioId { get; set; }

        public string Type { get; set; } = string.Empty;

        public bool Active { get; set; }
    }
}
