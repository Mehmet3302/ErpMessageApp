using Erp___Kurum_Ici_Haberlesme_2.Models;

namespace Erp___Kurum_Ici_Haberlesme_2.ViewModel
{
    public class RoleUserViewModel
    {
        public string? RoleId { get; set; }
        public string? RoleName { get; set; }
        public List<AppUser>? Users { get; set; }
    }
}
