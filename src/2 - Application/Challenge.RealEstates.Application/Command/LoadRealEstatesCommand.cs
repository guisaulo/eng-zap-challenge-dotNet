using System.ComponentModel.DataAnnotations;

namespace Challenge.RealEstates.API.Command
{
    public class LoadRealEstatesCommand
    {
        [Required]
        public string Url { get; set; }
    }
}
