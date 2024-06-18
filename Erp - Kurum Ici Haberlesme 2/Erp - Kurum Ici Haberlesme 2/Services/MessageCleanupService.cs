using Erp___Kurum_Ici_Haberlesme_2.Data;

namespace Erp___Kurum_Ici_Haberlesme_2.Services
{
    public class MessageCleanupService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;

        public MessageCleanupService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                    var thresholdDate = DateTime.Now.AddHours(-168);
                    var outdatedMessages = context.Messages
                        .Where(m => m.Tarih <= thresholdDate && !m.MessageDurum)
                        .ToList();

                    foreach (var message in outdatedMessages)
                    {
                        message.MessageDurum = true;
                    }

                    if (outdatedMessages.Any())
                    {
                        await context.SaveChangesAsync();
                    }
                }

                await Task.Delay(TimeSpan.FromHours(1), stoppingToken); // Servisi her saat başı çalıştırır
            }
        }
    }
}
