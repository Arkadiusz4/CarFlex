namespace CarFlex.Models
{
    public class CarStatsViewModel
    {
        public int CarId { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public int RentalCount { get; set; }
        public decimal TotalEarnings { get; set; }
    }
}
