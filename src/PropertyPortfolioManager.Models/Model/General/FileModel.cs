namespace PropertyPortfolioManager.Models.Model.General
{
    public class FileModel
    {
        public int Id { get; set; }

        public string ItemId { get; set; } = string.Empty;

        public string FileName { get; set; } = string.Empty;

        public long Size { get; set; }

        public bool Deleted { get; set; }
    }
}
