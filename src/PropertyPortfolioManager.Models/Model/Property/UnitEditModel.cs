﻿using PropertyPortfolioManager.Models.Model.General;
using System.Data.SqlTypes;

namespace PropertyPortfolioManager.Models.Model.Property
{
    public class UnitEditModel
    {
        public UnitEditModel()
        {
            this.Address = new AddressEditModel();
        }

        public int Id { get; set; }
        public string Code { get; set; } = string.Empty;
        public int AddressId { get; set; }
        public Int16  UnitTypeId { get; set; }
        public DateTime PurchaseDate { get; set; }
        public SqlMoney PurchasePrice { get; set; }
        public DateTime SaleDate { get; set; }
        public SqlMoney SalePrice { get; set; }
        public bool Deleted { get; set; }

        public AddressEditModel Address { get; set; }
    }
}