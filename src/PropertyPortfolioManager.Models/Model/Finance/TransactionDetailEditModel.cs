﻿namespace PropertyPortfolioManager.Models.Model.Finance
{
    public class TransactionDetailEditModel
    {
        public int Id { get; set; }

        public int TransactionId { get; set; }

        public int AccountId { get; set; }

        public DateTime Date { get; set; }

        public string Reference { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public decimal Credit { get; set; }

        public decimal Debit { get; set; }
    }
}
