using System.ComponentModel.DataAnnotations;

namespace Erp___Kurum_Ici_Haberlesme_2.Models
{
    public class Message
    {
        [Key]
        public int MessageId { get; set; }
        public string? MessageIcerik { get; set; }
        public bool MessageDurum { get; set; }
        public DateTime Tarih { get; set; }
        public string? UserId { get; set; }
        public virtual AppUser? User { get; set; }
    }
}
