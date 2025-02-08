using System.ComponentModel.DataAnnotations;

namespace Howest.MagicCards.WebAPI.Configuration
{
    public class ApiBehaviourConf
    {
        [Required]
        [Range(1, int.MaxValue)]
        public int MaxPageSize { get; set; }

        [Required]
        public string MessageToClient { get; set; }

    }
}
