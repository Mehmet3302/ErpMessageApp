using Microsoft.AspNetCore.Identity;

namespace Erp___Kurum_Ici_Haberlesme_2.Models
{
    public class AppRole : IdentityRole
    {
        public string? RolAd { get; set; }
    }
}
