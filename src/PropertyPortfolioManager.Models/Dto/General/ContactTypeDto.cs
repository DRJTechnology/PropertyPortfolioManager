namespace PropertyPortfolioManager.Models.Dto.General
{
    public class ContactTypeDto : BaseDto
    {
        public int PortfolioId { get; set; }

        public string Type { get; set; } = string.Empty;

        public bool Active { get; set; }
    }
}
