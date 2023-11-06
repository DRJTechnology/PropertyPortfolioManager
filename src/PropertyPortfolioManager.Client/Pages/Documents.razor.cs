using Microsoft.AspNetCore.Components;
using PropertyPortfolioManager.Client.Interfaces;
using PropertyPortfolioManager.Models.Model.Document;

namespace PropertyPortfolioManager.Client.Pages
{
    public partial class Documents
    {
        private List<DriveItemModel> DriveItemList = new List<DriveItemModel>();
        private List<string> knownExtensions = new List<string> { "aac", "ai", "bmp", "cs", "css", "csv", "doc", "docx", "exe", "gif", "heic", "html", "java", "jpg", "js", "json", "jsx", "key", "m4p", "md", "mdx", "mov", "mp3", "mp4", "otf", "pdf", "php", "png", "ppt", "pptx", "psd", "py", "raw", "rb", "sass", "scss", "sh", "sql", "svg", "tiff", "tsx", "ttf", "txt", "wav", "woff", "xls", "xlsx", "xml", "yml" };
        private bool DataLoading = true;

        [Inject]
        public IDocumentService documentService { get; set; }

        protected async override Task OnInitializedAsync()
        {
            try
            {
                DriveItemList = await this.documentService.GetCurrentFolderContentsAsync();
                DataLoading = false;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        protected async void SelectFolder(string driveId)
        {
            DriveItemList = await this.documentService.GetCurrentFolderContentsAsync(driveId);
            await InvokeAsync(() => StateHasChanged()).ConfigureAwait(false);
        }

        protected string GetFileTypeClass(string FileName)
        {
            var extension = Path.GetExtension(FileName).TrimStart('.');
            if (knownExtensions.Contains(extension))
            {
                return $"bi-filetype-{extension}";
            }
            else if (extension == "zip")
            {
                return "bi-file-earmark-zip";
            }
            else
            {
                return "bi-file-earmark";
            }
        }
    }
}
