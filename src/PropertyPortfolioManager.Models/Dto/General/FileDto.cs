namespace PropertyPortfolioManager.Models.Dto.General
{
    public class FileDto : BaseDto
    {
        public string ItemId { get; set; } = string.Empty;
        public string FileName { get; set; } = string.Empty;
        public long Size { get; set; }
    }
}
