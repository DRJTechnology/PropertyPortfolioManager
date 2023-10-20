using System.ComponentModel.DataAnnotations;

namespace PropertyPortfolioManager.Models.Model.Property
{
    public class PortfolioModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        public bool Active { get; set; }
    }
}
