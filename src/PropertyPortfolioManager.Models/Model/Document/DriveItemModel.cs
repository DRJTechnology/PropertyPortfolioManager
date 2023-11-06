namespace PropertyPortfolioManager.Models.Model.Document
{
    public class DriveItemModel
    {
        public string Name { get; set; } = string.Empty;
        public bool IsFolder { get; set; }
        public string Id { get; set; } = string.Empty;
        public string WebUrl { get; set; } = string.Empty;
        public long Size { get; set; }
        public string LastModifiedByName { get; set; } = string.Empty;
        public DateTimeOffset LastModifiedDateTime { get; set; }
    }
}
