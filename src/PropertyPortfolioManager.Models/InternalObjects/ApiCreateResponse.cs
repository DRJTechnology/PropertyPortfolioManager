namespace PropertyPortfolioManager.Models.InternalObjects
{
    public class ApiCreateResponse
    {
        public bool Success { get; set; }
        public int CreatedId { get; set; }
        public string ErrorMessage { get; set; }
    }
}
