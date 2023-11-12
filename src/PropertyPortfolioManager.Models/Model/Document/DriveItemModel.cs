namespace PropertyPortfolioManager.Models.Model.Document
{
    public class DriveItemModel
    {
        public DriveItemModel()
        {
            DriveItemList = new List<DriveItemModel>();
        }
        public string Name { get; set; } = string.Empty;
        public bool IsFolder { get; set; }
        public string FileMimeType { get; set; } = string.Empty;
        public string Id { get; set; } = string.Empty;
        public string WebUrl { get; set; } = string.Empty;
        public long Size { get; set; }
        public string LastModifiedByName { get; set; } = string.Empty;
        public DateTimeOffset LastModifiedDateTime { get; set; }
        public List<DriveItemModel> DriveItemList { get; set; }
    }
}
