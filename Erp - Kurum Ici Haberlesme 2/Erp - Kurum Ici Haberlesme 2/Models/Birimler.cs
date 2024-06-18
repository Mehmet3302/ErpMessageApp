using System.ComponentModel.DataAnnotations;

namespace Erp___Kurum_Ici_Haberlesme_2.Models
{
    public class Birimler
    {
        [Key]
        public int BirimId { get; set; }
        public string? BirimAd { get; set; }
    }
}
