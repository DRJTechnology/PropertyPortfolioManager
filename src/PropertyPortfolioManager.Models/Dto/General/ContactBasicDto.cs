namespace PropertyPortfolioManager.Models.Dto.General
{
    public class ContactBasicDto
    {
        public int Id { get; set; }

        public int PortfolioId { get; set; }

        public string Name { get; set; } = string.Empty;

        public string ContactType { get; set; } = string.Empty;

        public string StreetAddress { get; set; } = string.Empty;

        public bool Active { get; set; }
    }
}
