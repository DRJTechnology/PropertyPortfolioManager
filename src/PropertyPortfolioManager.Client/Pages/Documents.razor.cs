using Microsoft.AspNetCore.Components;
using PropertyPortfolioManager.Client.Interfaces;
using PropertyPortfolioManager.Client.Models;
using PropertyPortfolioManager.Models.Model.Document;

namespace PropertyPortfolioManager.Client.Pages
{
    public partial class Documents
    {
        private DriveItemModel CurrentFolder = new DriveItemModel();
        private List<string> knownExtensions = new List<string> { "aac", "ai", "bmp", "cs", "css", "csv", "doc", "docx", "exe", "gif", "heic", "html", "java", "jpg", "js", "json", "jsx", "key", "m4p", "md", "mdx", "mov", "mp3", "mp4", "otf", "pdf", "php", "png", "ppt", "pptx", "psd", "py", "raw", "rb", "sass", "scss", "sh", "sql", "svg", "tiff", "tsx", "ttf", "txt", "wav", "woff", "xls", "xlsx", "xml", "yml" };
        private bool DataLoading = true;
        private List<BreadcrumbItem> Breadcrumb = new List<BreadcrumbItem>();

        [Inject]
        public IDocumentService documentService { get; set; }

        [Parameter]
        public bool SelectFileMode { get; set; }

        [Parameter]
        public EventCallback<string> FileIdSelected { get; set; }

        protected async override Task OnInitializedAsync()
        {
            try
            {
                CurrentFolder = await this.documentService.GetFolderAsync();
                Breadcrumb.Add(new BreadcrumbItem() { Id = CurrentFolder.Id, Name = "Documents", WebUrl = CurrentFolder.WebUrl });
                DataLoading = false;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        protected async void SelectFolder(string driveId)
        {
            CurrentFolder = await this.documentService.GetFolderAsync(driveId);
            this.UpdateBreadcrunb();
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
        private void UpdateBreadcrunb()
        {
            if (Breadcrumb.Exists(b => b.Id == CurrentFolder.Id))
            {
                var existingItem = Breadcrumb.Where(b => b.Id == CurrentFolder.Id).FirstOrDefault();
                int index = Breadcrumb.IndexOf(existingItem);
                if (index != -1)
                {
                    Breadcrumb.RemoveRange(index + 1, Breadcrumb.Count - index - 1);
                }
            }
            else
            {
                Breadcrumb.Add(new BreadcrumbItem() { Id = CurrentFolder.Id, Name = CurrentFolder.Name, WebUrl = CurrentFolder.WebUrl });
            }
        }

        private async Task SelectFile(string driveItemId)
        {
            await FileIdSelected.InvokeAsync(driveItemId);
        }
    }
}
