namespace Challenge.RealEstates.Application.Command
{
    public class LoadRealEstatesCommandResponse
    {
        public string LoadDate { get; set; }
        public RealEstatesCommandResponse Input { get; set; }
        public RealEstatesCommandResponse Zap { get; set; }
        public RealEstatesCommandResponse VivaReal { get; set; }
    }
}