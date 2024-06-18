using Erp___Kurum_Ici_Haberlesme_2.Data;
using Erp___Kurum_Ici_Haberlesme_2.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace Erp___Kurum_Ici_Haberlesme_2.Controllers
{
    [Authorize]
    public class MessageController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ApplicationDbContext _context;
        private readonly RoleManager<AppRole> _roleManager;

        public MessageController(ApplicationDbContext context, UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            // Sadece MessageDurum değeri false olan mesajları al
            var messages = await _context.Messages
                .Include(m => m.User)
                .Where(m => !m.MessageDurum)
                .OrderBy(m => m.Tarih)
                .ToListAsync();

            var adminUsers = await _userManager.GetUsersInRoleAsync("Admin");
            var adminUserIds = adminUsers.Select(u => u.Id).ToList();

            var nonAdminUsers = await _context.Users
                .Include(u => u.AltBirim)
                .Where(u => !adminUserIds.Contains(u.Id))
                .ToListAsync();

            ViewBag.Personeller = nonAdminUsers;

            var allUsers = await _context.Users
                .Include(u => u.AltBirim)
                .ToListAsync();

            ViewBag.AllUsers = allUsers;

            return View(messages);
        }

        [HttpPost]
        public async Task<IActionResult> SendMessage([FromBody] MessageDto messageDto)
        {
            if (string.IsNullOrWhiteSpace(messageDto.Content))
            {
                return BadRequest("Mesaj boş olamaz");
            }

            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return Unauthorized();
            }

            var newMessage = new Message
            {
                MessageIcerik = messageDto.Content,
                Tarih = DateTime.Now,
                UserId = user.Id
            };

            _context.Messages.Add(newMessage);
            await _context.SaveChangesAsync();

            return Ok();
        }

        public class MessageDto
        {
            public string Content { get; set; }
        }

        [HttpPost]
        [Authorize(Roles = "Bilişim Teknolojileri Genel Müdürü, Satın Alma Genel Müdürü, Muhasebe Genel Müdürü, İnsan Kaynakları Genel Müdürü")]
        public async Task<IActionResult> DeleteMessage([FromBody] DeleteMessageModel model)
        {
            try
            {
                var message = await _context.Messages.FirstOrDefaultAsync(m => m.MessageId == model.MessageId);

                if (message != null)
                {
                    message.MessageDurum = true;
                    _context.Messages.Update(message); // Değişikliği güncelleyerek kaydedin
                    await _context.SaveChangesAsync();
                    return Json(new { success = true });
                }
                else
                {
                    return Json(new { success = false, message = "Mesaj bulunamadı" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        public class DeleteMessageModel
        {
            public int MessageId { get; set; }
        }
    }
}
