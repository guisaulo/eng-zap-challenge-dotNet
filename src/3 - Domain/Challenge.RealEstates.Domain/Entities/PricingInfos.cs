namespace Challenge.RealEstates.Domain.Entities
{
    public class PricingInfos
    {
        public string BusinessType { get; set; }
        public long Price { get; set; }
        public long RentalTotalPrice { get; set; }
        public long MonthlyCondoFee { get; set; }
        public long YearlyIptu { get; set; }
    }
}