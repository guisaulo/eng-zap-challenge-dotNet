namespace Challenge.RealEstates.Domain.DomainResponse
{
    public class AddRangeResponse
    {
        public RealEstatesResponse Input { get; set; }
        public RealEstatesResponse Zap { get; set; }
        public RealEstatesResponse VivaReal { get; set; }

        public AddRangeResponse()
        {
            this.Input = new RealEstatesResponse();
            this.Zap = new RealEstatesResponse();
            this.VivaReal = new RealEstatesResponse();
        }
    }
}
