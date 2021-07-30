using System.ComponentModel.DataAnnotations;

namespace Challenge.RealEstates.Application.Command
{
    public class LoadRealEstatesCommand
    {
        [Required]
        public string Url { get; set; }
    }
}
