namespace PropertyPortfolioManager.Client.Helpers
{
    public static class StringHelpers
    {
        public static string ToFileSizeString(this long fileSizeInBytes)
        {
            string[] sizes = { "B", "KB", "MB", "GB", "TB" };
            decimal decimalFileSize = (decimal)fileSizeInBytes;
            int order = 0;
            while (decimalFileSize >= 1024 && order < sizes.Length - 1)
            {
                order++;
                decimalFileSize /= 1024;
            }
            return string.Format("{0:0.##} {1}", decimalFileSize, sizes[order]);
        }
    }
}
