using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.Configuration.Attributes;
using DRJTechnology.Cache;
using Microsoft.Extensions.Options;
using PropertyPortfolioManager.Models.Model.Finance;
using PropertyPortfolioManager.Server.Repositories.Interfaces;
using PropertyPortfolioManager.Server.Services.Helpers;
using PropertyPortfolioManager.Server.Services.Interfaces;
using PropertyPortfolioManager.Server.Shared.Configuration;
using System.Globalization;

namespace PropertyPortfolioManager.Server.Services
{
    public class BankStatementService : IBankStatementService
    {
        private Settings settings;
        private readonly ICacheService cacheService;
        private readonly IBankStatementRepository bankStatementRepository;

        public BankStatementService(IOptions<Settings> settings, ICacheService cacheService, IBankStatementRepository bankStatementRepository)
        {
            this.settings = settings.Value;
            this.cacheService = cacheService;
            this.bankStatementRepository = bankStatementRepository;
        }

        public async Task<string> UploadBankStatement(int currentUserId, int portfolioId, Stream stream)
        {
            var config = new CsvConfiguration(new CultureInfo("en-GB"));
            config.MissingFieldFound = null;
            config.IgnoreBlankLines = true;

            var recordList = new List<BankStatementModel>();
            using (var reader = new StreamReader(stream))
            using (var csv = new CsvReader(reader, config))
            {
                csv.Context.RegisterClassMap<BankStatementMap>();
                var records = csv.GetRecords<BankStatementModel>();
                recordList = records.ToList();
            }
            var dataTable = DataHelpers.ConvertToDataTable<BankStatementModel>(recordList);

            var uploadResults = await this.bankStatementRepository.AddBankStatementRecords(currentUserId, portfolioId, dataTable);

            return $"Upload file contains {uploadResults.TotalRowCount} rows." + Environment.NewLine
                    + $"{uploadResults.InsertedRowCount} rows were inserted." + Environment.NewLine
                    + $"{uploadResults.TotalRowCount - uploadResults.InsertedRowCount} duplicate rows were found.";
        }
    }
}
