namespace PropertyPortfolioManager.Models.Dto.Profile
{
    public class UserDto
    {
        public int Id { get; set; }
        public int? SelectedPortfilioId { get; set; }
        public Guid ObjectIdentifier { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
